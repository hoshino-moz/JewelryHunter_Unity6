using UnityEngine;

public class TimeController : MonoBehaviour
{
    //�J�E���g�@�_�E��or �A�b�v
    public bool isCountDown = true;

    //�Q�[���̊�ƂȂ鎞��
    public float gameTime = 0;

    //�J�E���g���~�߂�t���O�@True�ŃJ�E���g�I��
    public bool isTimeOver = false;

    //���[�U�Ɍ����鎞��
    public float displayTime = 0;

    //�Q�[���̌o�ߎ���
    float times = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�����J�E���g�_�E���ł���Ί���Ԃ����[�U�[�Ɍ�����
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
            //��~�t���O�������Ă��Ȃ��̂ŏ�����������
            //�Q�[���X�e�[�^�X��playing�łȂ��Ȃ�~�߂���
            if (GameManager.gameState != "playing")
            {
                isTimeOver = true; // ��~�t���O��ON
            }

            //�J�E���g�̏���������

            //�o�ߎ��Ԃ̒~��
            times += Time.deltaTime; //�f���^�^�C���̒~��

            //�J�E���g�_�E���������ꍇ
            if (isCountDown)
            {
                //���[�U�[�Ɍ�����������
                displayTime = gameTime - times ;
                Debug.Log("�o�ߎ���");

                if (displayTime <= 0)
                {
                    displayTime = 0; //�\�L��0�ɂ���
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
