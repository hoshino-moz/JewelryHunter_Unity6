using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("生成プレハブ/時間/速度/範囲")]
    public GameObject objPrefab; //発射させるプレハブデータ
    public float delayTime = 3.0f;//インターバル時間
    public float fireSpeed = 4.0f;//発射速度
    public float length = 8.0f; //範囲

    [Header("発射口")]
    public Transform gateTransfome;

    GameObject player;  //プレイヤー
    float passedTime = 0; //経過時間

    //距離チェック
    bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        float d = Vector2.Distance(transform.position, targetPos);
        if (length >= d)
        {
            ret = true;
        }
        return ret;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerを取得
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

            //待機時間加算
            passedTime += Time.deltaTime;
            //Playerとの距離チェック
            if (CheckLength(player.transform.position))
            {
                //待機時間経過
                if (passedTime > delayTime)
                {
                    {
                        passedTime = 0;
                        //砲弾をプレハブから作る
                        Vector2 pos = new Vector2(gateTransfome.position.x, gateTransfome.position.y);
                        GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
                        //砲身が向いているほうに発射する
                        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
                        float angleZ = transform.localEulerAngles.z;
                        float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
                        float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);
                        Vector2 v = new Vector2(x, y) * fireSpeed;
                        rbody.AddForce(v, ForceMode2D.Impulse);


                    }
                }
            }

        }

    }


    //範囲表示
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, length);
    }
}
