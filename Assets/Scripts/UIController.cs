using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject mainImage; //�A�i�E���X������摜
    public GameObject buttonPanel; //�{�^�����O���[�v�����Ă���p�l��

    public GameObject retryButton; //���g���C�{�^��
    public GameObject nextButton; //�l�N�X�g�{�^��

    public Sprite gameClearSprite; //�Q�[���N���A�̊G
    public Sprite gameOverSprite; //�Q�[���I�[�o�[�̊G

    TimeController timeCnt; //TimeController.cs�@�Q��
    public GameObject timeText; //Object TimeText ���Q��

    public GameObject scoreText; //�X�R�A�e�L�X�g

    AudioSource audio;
    SoundController soundController; //���삵���X�N���v�g


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        timeCnt = GetComponent<TimeController>();

        buttonPanel.SetActive(false); //���݂��\��(���O�̉��̃`�F�b�N�{�b�N�X)
       
        //���ԍ��Ń��\�b�h�𔭓�
        Invoke("InactiveImage",1.0f);

        UpdateScore();  //�g�[�^���X�R�A���o��悤�ɍX�V

        //AudioSource��SoundController �̎擾
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
            //���C���摜�I�u�W�F�N�g�̕ϐ�sprite�ɃX�e�[�W�N���A�̉摜����
            mainImage.GetComponent<Image>().sprite = gameClearSprite;
            //���g���C�{�^���I�u�W�F�N�g��Button�R���|�[�l���g��interractable�𖳌���
            retryButton.GetComponent<Button>().interactable = false;

            //�X�e�[�W�X�R�A���g�[�^���X�R�A�ɉ��Z
            GameManager.totalScore += GameManager.stageScore;
            GameManager.stageScore = 0;

            timeCnt.isTimeOver = true;  //�^�C���J�E���g��~
            float times = timeCnt.displayTime;
            if (timeCnt.isCountDown)
            {
                //�c���Ԃ����̂܂܃^�C���{�[�i�X�Ƃ��ĉ��Z
                GameManager.totalScore += (int)times * 10 ;
            }
            else
            {
                float gameTime = timeCnt.gameTime; //����Ԃ̎擾
                GameManager.totalScore += (int)(gameTime - times) * 10;
            }

            UpdateScore(); //UI�ɕ\��

            audio.Stop(); //�X�e�[�W��BGM���~�߂�
            //SoundController�̕ϐ��Ɏw����������I�����Ė炷
            audio.PlayOneShot(soundController.bgm_GameClear); 

            GameManager.gameState = "gameend";  //�d�����Z���Ȃ��悤�Ƀt���O���Q�[���I�[�o�[��

        }
        else if (GameManager.gameState == "gameover")
        {
            buttonPanel.SetActive(true); //�{�^���p�l���̕���
            mainImage.SetActive(true); //���C���摜�̕���
            //���C���摜�I�u�W�F�N�g�̕ϐ�sprite�ɃQ�[���I�[�o�[�̉摜����
            mainImage.GetComponent<Image>().sprite = gameOverSprite;
            //�l�N�X�g�{�^���I�u�W�F�N�g��Button�R���|�[�l���g��interractable�𖳌���
            nextButton.GetComponent<Button>().interactable = false;

            timeCnt.isTimeOver = true ; //�J�E���g���~�߂�

            audio.Stop(); //�X�e�[�W��BGM���~�߂�
            //SoundController�̕ϐ��Ɏw����������I�����Ė炷
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
                    //�v���C���[�������Ă��āA����PlayerController�R���|�[�l���g��GameOver���\�b�h����点�Ă���
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GameOver();
                    GameManager.gameState = "gameover";
                }
            }
            else
            {
                if (timeCnt.displayTime >= timeCnt.gameTime)
                {
                    //�v���C���[�������Ă��āA����PlayerController�R���|�[�l���g��GameOver���\�b�h����点�Ă���
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GameOver();
                    GameManager.gameState = "gameover";
                }
            }

            UpdateScore();  //�X�R�A�@���A���^�C���ɍX�V

        }

    }

    //���C���摜���\�����邽�߂����̃��\�b�h
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
