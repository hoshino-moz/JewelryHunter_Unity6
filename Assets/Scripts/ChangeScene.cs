using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; //切り替えたいシーン名を指定

    public void Load()
    {
        //シーン切り替えのメソッド
        SceneManager.LoadScene(sceneName);

    }
}
