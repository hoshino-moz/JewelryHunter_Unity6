using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string gameState ; //�ÓI�����o�ϐ�

    public static int totalScore; //�Q�[���S�ʂ�ʂ��ẴX�R�A
    public static int stageScore; //�X�e�[�W�Ŋl�������X�R�A


    //start����Ɏ��s
    void Awake()
    {
        //�Q�[���̏�����Ԃ�playing
        gameState = "playing";
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameState);
    }
}
