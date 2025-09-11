using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("playerの能力値")]
    public float speed = 3.0f; //playerのスピードを調整
    public float jumpPower = 9.0f; //ジャンプ力

    [Header("地面判定の対象レイヤー")]
    public LayerMask groundLayer;  //地面レイヤーを指名するための変数


    Rigidbody2D rbody; //PlayerについているRigidBody2Dを扱うための変数
    Animator animator; //Animator
    // Transform tr; //本来なら　transform特別

    float axisH; //入力の方向を記憶するための変数

    bool goJump = false; //ジャンプフラグ　(True:真 false: 偽)
    bool onGround = false; //地面にイルカどうか？



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>(); //Playerについているコンポーネント情報を取得
        // tr = GetComponent<Transform>();　//本来なら　transform特別
        animator = GetComponent<Animator>();  //
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームのステータスがplayingでないなら
        if (GameManager.gameState != "playing")
        {
            return; // その1フレームを強制終了
        }

        //もしも水平方向のキーが押されたら
        //if (Input.GetAxisRaw ("Horizontal") != 0 )

        //Velocityの元となる値の取得 (右なら1.0f、左なら-1.0f、何もなければ0 )
        axisH = Input.GetAxisRaw("Horizontal");

        if (axisH > 0)
        {
            //右を向く
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (axisH < 0)
        {
            //左を向く
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //GetButtonDownメソッド-> 引数に　　Jumpボタンはスペースキー
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }

    //1秒間に50回(50fps)繰り返すように制御しながら行う繰り返しメソッド
    void FixedUpdate()
    {
        //ゲームのステータスがplayingでないなら
        if (GameManager.gameState != "playing")
        {
            return; // その1フレームを強制終了
        }

        //　地面判定をサークルキャスト
        onGround = Physics2D.CircleCast(
            transform.position, //発射位置=playerの位置
            0.2f,              //Circleの半径
            new Vector2(0, 1.0f), //発射方向
            0,                  //発射距離
            groundLayer        //対象となるレイヤー情報(変数)
            );

        //Velocity 代入
        rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);

        //ジャンプフラグが立ったら
        if (goJump)
        {
            //ジャンプさせる
            rbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            goJump = false; //FlagをOffに戻す
        }


        //if (onGround)　//地面の上にいるとき
        //{
            if (axisH == 0)
            {
                animator.SetBool("Run", false); //アイドルアニメに切り替え
            }
            else
            {
                animator.SetBool("Run", true); //Runアニメに切り替え
            }
        //}
    }

    //ジャンプボタンが押されたら
    void Jump()
    {
        if (onGround)
        {
            goJump = true;  //JumpフラグをON
            animator.SetTrigger("Jump");
        }
    }

    //isTrigger特性を持っているColliderとぶつかったら処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Goal")
        //ゴールに接触したらGameClear
        if (collision.gameObject.CompareTag("Goal"))
        {
            GameManager.gameState = "gameclear";
            Debug.Log("ゴールに接触した！");
            Goal();
        }

        //  穴に落ちたら(Dead)　ゲームオーバー
        if (collision.gameObject.CompareTag("Dead"))
        {
            GameManager.gameState = "gameover";
            Debug.Log("ゲームオーバー！");
            GameOver();
        }

        //アイテムに触れたらスコア加算
        if (collision.gameObject.CompareTag("ScoreItem"))
        {
            GameManager.stageScore += collision.gameObject.GetComponent<ItemData>().value;
            Destroy(collision.gameObject);
        }

    }

    //ゴールした時のメソッド
    public void Goal()
    {
        animator.SetBool("Clear", true); //クリアアニメに切り替え
        GameStop(); //プレイヤーのVelocityを止めるメソッド
    }

    public void GameOver()
    {
        animator.SetBool("Dead", true); //デッドアニメに切り替え
        GameStop();

        //当たり判定を無効にする
        GetComponent<CapsuleCollider2D>().enabled = false;

        //少し上に飛び跳ねる
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

        //プレイヤーを時間差(3秒)で抹消
        Destroy(gameObject,3.0f);
    }

    void GameStop()
    {
        //速度を0にリセット
        //rbody.linearVelocity = new Vector2(0, 0);
        rbody.linearVelocity = Vector2.zero;
    }
}
