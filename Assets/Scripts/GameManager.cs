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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
