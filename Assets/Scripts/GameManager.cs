using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string gameState ; //静的メンバ変数

    public static int totalScore; //ゲーム全般を通してのスコア
    public static int stageScore; //ステージで獲得したスコア


    //startより先に実行
    void Awake()
    {
        //ゲームの初期状態をplaying
        gameState = "playing";
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameState);
    }
}
