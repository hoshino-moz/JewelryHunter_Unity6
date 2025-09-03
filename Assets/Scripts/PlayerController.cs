using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("playerの能力値")]
    public float speed = 3.0f; //playerのスピードを調整
    public float jumpPower = 9.0f; //ジャンプ力

    [Header("地面判定の対象レイヤー")]
    public LayerMask groundLayer;  //地面レイヤーを指名するための変数


    Rigidbody2D rbody; //PlayerについているRigidBody2Dを扱うための変数
    // Transform tr; //本来なら　transform特別

    float axisH; //入力の方向を記憶するための変数

    bool goJump = false; //ジャンプフラグ　(True:真 false: 偽)
    bool onGrand = false; //地面にイルカどうか？

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>(); //Playerについているコンポーネント情報を取得
        // tr = GetComponent<Transform>();　//本来なら　transform特別
    }

    // Update is called once per frame
    void Update()
    {
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
    private void FixedUpdate()
    {
        //　地面判定をサークルキャスト
        onGrand = Physics2D.CircleCast (
            transform.position, //発射位置=playerの位置
            0.2f ,              //Circleの半径
            new Vector2(0, 1.0f) , //発射方向
            0,                  //発射距離
            groundLayer        //対象となるレイヤー情報(変数)
            );

        //Velocity 代入
        rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);

        //ジャンプ
        if (goJump)
        {
            //ジャンプさせる
            rbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            goJump = false; //FlagをOffに戻す
        }
    }


    //ジャンプボタンが押されたら
    void Jump()
    {
        if (onGrand)
        {
            goJump = true;  //JumpフラグをON
        }
    }
}
