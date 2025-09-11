using Unity.VisualScripting;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    [Header("落下検知距離")]
    public float length = 0.0f; //自動落下検知距離

    [Header("落下後消滅")]
    public bool isDelete = false; //落下後に削除

    [Header("当たり判定オブジェクト")]
    public GameObject deadObj;  //死亡あたり

    bool isFell = false;   //落下フラグ
    float fadeTime = 0.5f;  //フェードアウト時間

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Rigidbody2Dの物理挙動を停止
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
        deadObj.SetActive(false);  //死亡あたりを非表示
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //Playerを探す
        if (player != null )
        {
            //playerとの距離計測
            float d = Vector2.Distance(transform.position, player.transform.position);
            if (length >= d)
            {
                Rigidbody2D rbody = GetComponent<Rigidbody2D>();
                if (rbody.bodyType == RigidbodyType2D.Static)
                {
                    //Rigidbody2Dの物理挙動を開始
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                    deadObj.SetActive(true);   //死亡あたりを表示
                }
            }
        }
        if (isFell)
        {
            //落下した
            //透明値を変更してフェードアウトさせる
            fadeTime -= Time.deltaTime; //
            Color col = GetComponent<SpriteRenderer>().color; //カラーを取り出す
            col.a = fadeTime;  //透明値を変更
            GetComponent<SpriteRenderer>().color = col; //カラーを再設定
            if (fadeTime <= 0.0f)
            {
                //透明になったら消す
                Destroy(gameObject);
            }
        }
    }

    //接触開始
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDelete)
        {
            isFell = true; //キャラクターを消すかどうかのフラグ
        }
    }
    //範囲表示
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, length);
    }


}
