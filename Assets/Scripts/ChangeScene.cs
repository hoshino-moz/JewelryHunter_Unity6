using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; //切り替えたいシーン名を指定
    public bool toTitle; //タイトル切り替えのフラグ

    public void Load()
    {
        // 
        GameManager.stageScore = 0;

        //タイトル画面に戻る時はスコアーリセット
        if (toTitle) GameManager.totalScore = 0;

        //シーン切り替えのメソッド
        SceneManager.LoadScene(sceneName);

    }
}
