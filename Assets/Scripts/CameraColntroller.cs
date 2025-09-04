using UnityEngine;

public class CameraColntroller : MonoBehaviour
{
    GameObject player;
    float x, y, z; //カメラの座標を決める変数

    [Header("カメラの限界値")]
    public float leftLimit;
    public float rightLimit;
    public float bottomLimit;
    public float topLimit;

    [Header("カメラのスクロール")]
    public bool isScrollX; //横方向に強制スクロール
    public float scrollSpeedX = 0.5f;
    public bool isScrollY; //横方向に強制スクロール
    public float scrollSpeedY = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Playerタグを持ったゲームオブジェクトを探して、変数playerに代入
        player = GameObject.FindGameObjectWithTag("Player");
        //カメラのZ座標は初期値のままを維持したい
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //いったんプレイヤーのx座標、y座標の位置を変数に取得
        x = player.transform.position.x;
        y = player.transform.position.y;

        //X方向の強制スクロール
        if (isScrollX )
        {
            x = transform.position.x + (scrollSpeedX * Time.deltaTime) ;
        }

        //左右のスクロールリミット
        if (x < leftLimit)
        {
            x = leftLimit;
        }
        else if (x > rightLimit)
        {
            x = rightLimit;
        }

        //Y方向の強制スクロール
        if (isScrollY)
        {
            y = transform.position.y + (scrollSpeedY * Time.deltaTime) ;
        }

        //上下のスクロールリミット
        if (y < bottomLimit)
        {
            y = bottomLimit;
        }
        else if (y > topLimit)
        {
            y = topLimit;
        }

        //取り決めた各変数の値をカメラのポジションとする
        transform.position = new Vector3(x, y, z);

    }
}
