using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject mainImage;
    public GameObject buttonPanel;

    public GameObject retryButton;
    public GameObject nextButton;

    public Sprite gameClearSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       buttonPanel.SetActive(false); //存在を非表示(名前の横のチェックボックス)
       
        //時間差でメソッドを発動
        Invoke("InactiveImage",1.0f);
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
        }
    }

    //メイン画像を非表示するためだけのメソッド
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

}
