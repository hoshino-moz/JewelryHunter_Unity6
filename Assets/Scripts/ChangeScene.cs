using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; //�؂�ւ������V�[�������w��

    public void Load()
    {
        //�V�[���؂�ւ��̃��\�b�h
        SceneManager.LoadScene(sceneName);

    }
}
