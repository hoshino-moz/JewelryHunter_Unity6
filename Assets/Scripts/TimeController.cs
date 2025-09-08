using UnityEngine;

public class TimeController : MonoBehaviour
{
    //カウント　ダウンor アップ
    public bool isCountDown = true;

    //ゲームの基準となる時間
    public float gameTime = 0;

    //カウントを止めるフラグ　Trueでカウント終了
    public bool isTimeOver = false;

    //ユーザに見せる時間
    public float displayTime = 0;

    //ゲームの経過時間
    float times = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //もしカウントダウンであれば基準時間をユーザーに見せる
        if (isCountDown)
        {
            displayTime = gameTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimeOver)
        {
            //停止フラグが立っていないので処理したいが
            //ゲームステータスがplayingでないなら止めたい
            if (GameManager.gameState != "playing")
            {
                isTimeOver = true; // 停止フラグをON
            }

            //カウントの処理をする

            //経過時間の蓄積
            times += Time.deltaTime; //デルタタイムの蓄積

            //カウントダウンだった場合
            if (isCountDown)
            {
                //ユーザーに見せたい時間
                displayTime = gameTime - times ;
                Debug.Log("経過時間");

                if (displayTime <= 0)
                {
                    displayTime = 0; //表記を0にする
                    isTimeOver = true ;
                    GameManager.gameState = "gameover";
                }
            }
            else
            {
                displayTime = times;
                if (displayTime <= gameTime)
                {
                    displayTime = gameTime ;
                    isTimeOver = true ;
                    GameManager.gameState = "gameover";
                }
            }

            Debug.Log(displayTime);
        }
    }
}
