using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject mainImage; //アナウンスをする画像
    public GameObject buttonPanel; //ボタンをグループ化しているパネル

    public GameObject retryButton; //リトライボタン
    public GameObject nextButton; //ネクストボタン

    public Sprite gameClearSprite; //ゲームクリアの絵
    public Sprite gameOverSprite; //ゲームオーバーの絵

    TimeController timeCnt; //TimeController.cs　参照
    public GameObject timeText; //Object TimeText を参照

    public GameObject scoreText; //スコアテキスト

    AudioSource audio;
    SoundController soundController; //自作したスクリプト


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        timeCnt = GetComponent<TimeController>();

        buttonPanel.SetActive(false); //存在を非表示(名前の横のチェックボックス)
       
        //時間差でメソッドを発動
        Invoke("InactiveImage",1.0f);

        UpdateScore();  //トータルスコアが出るように更新

        //AudioSourceとSoundController の取得
        audio = GetComponent<AudioSource>();
        soundController = GetComponent<SoundController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState == "gameclear")
        {
            buttonPanel.SetActive(true);
            mainImage.SetActive(true);
            //メイン画像オブジェクトの変数spriteにステージクリアの画像を代入
            mainImage.GetComponent<Image>().sprite = gameClearSprite;
            //リトライボタンオブジェクトのButtonコンポーネントのinterractableを無効化
            retryButton.GetComponent<Button>().interactable = false;

            //ステージスコアをトータルスコアに加算
            GameManager.totalScore += GameManager.stageScore;
            GameManager.stageScore = 0;

            timeCnt.isTimeOver = true;  //タイムカウント停止
            float times = timeCnt.displayTime;
            if (timeCnt.isCountDown)
            {
                //残時間をそのままタイムボーナスとして加算
                GameManager.totalScore += (int)times * 10 ;
            }
            else
            {
                float gameTime = timeCnt.gameTime; //基準時間の取得
                GameManager.totalScore += (int)(gameTime - times) * 10;
            }

            UpdateScore(); //UIに表示

            audio.Stop(); //ステージのBGMを止める
            //SoundControllerの変数に指名した音を選択して鳴らす
            audio.PlayOneShot(soundController.bgm_GameClear); 

            GameManager.gameState = "gameend";  //重複加算しないようにフラグをゲームオーバーに

        }
        else if (GameManager.gameState == "gameover")
        {
            buttonPanel.SetActive(true); //ボタンパネルの復活
            mainImage.SetActive(true); //メイン画像の復活
            //メイン画像オブジェクトの変数spriteにゲームオーバーの画像を代入
            mainImage.GetComponent<Image>().sprite = gameOverSprite;
            //ネクストボタンオブジェクトのButtonコンポーネントのinterractableを無効化
            nextButton.GetComponent<Button>().interactable = false;

            timeCnt.isTimeOver = true ; //カウントを止める

            audio.Stop(); //ステージのBGMを止める
            //SoundControllerの変数に指名した音を選択して鳴らす
            audio.PlayOneShot(soundController.bgm_GameOver);

            GameManager.gameState = "gameend";
        }
        else if (GameManager.gameState == "playing")
        {
            float times = timeCnt.displayTime;
            timeText.GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(times).ToString();

            if(timeCnt.isCountDown)
            {
                if (timeCnt.displayTime <= 0)
                {
                    //プレイヤーを見つけてきて、そのPlayerControllerコンポーネントのGameOverメソッドをやらせている
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GameOver();
                    GameManager.gameState = "gameover";
                }
            }
            else
            {
                if (timeCnt.displayTime >= timeCnt.gameTime)
                {
                    //プレイヤーを見つけてきて、そのPlayerControllerコンポーネントのGameOverメソッドをやらせている
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GameOver();
                    GameManager.gameState = "gameover";
                }
            }

            UpdateScore();  //スコア　リアルタイムに更新

        }

    }

    //メイン画像を非表示するためだけのメソッド
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    void UpdateScore()
    {
        int score = GameManager.stageScore + GameManager.totalScore;
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
        //Debug.Log(score);
    }
}
