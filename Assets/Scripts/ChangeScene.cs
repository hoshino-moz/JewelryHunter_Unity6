using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; //�؂�ւ������V�[�������w��
    public bool toTitle; //�^�C�g���؂�ւ��̃t���O

    public void Load()
    {
        // 
        GameManager.stageScore = 0;

        //�^�C�g����ʂɖ߂鎞�̓X�R�A�[���Z�b�g
        if (toTitle) GameManager.totalScore = 0;

        //�V�[���؂�ւ��̃��\�b�h
        SceneManager.LoadScene(sceneName);

    }
}
