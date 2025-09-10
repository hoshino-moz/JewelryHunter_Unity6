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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeCnt = GetComponent<TimeController>();

        buttonPanel.SetActive(false); //���݂��\��(���O�̉��̃`�F�b�N�{�b�N�X)
       
        //���ԍ��Ń��\�b�h�𔭓�
        Invoke("InactiveImage",1.0f);
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
        }
        else if (GameManager.gameState == "gameover")
        {
            buttonPanel.SetActive(true); //�{�^���p�l���̕���
            mainImage.SetActive(true); //���C���摜�̕���
            //���C���摜�I�u�W�F�N�g�̕ϐ�sprite�ɃQ�[���I�[�o�[�̉摜����
            mainImage.GetComponent<Image>().sprite = gameOverSprite;
            //�l�N�X�g�{�^���I�u�W�F�N�g��Button�R���|�[�l���g��interractable�𖳌���
            nextButton.GetComponent<Button>().interactable = false;
        }
        else if (GameManager.gameState == "playing")
        {
            float times = timeCnt.displayTime;
            timeText.GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(times).ToString();
        }

    }

    //���C���摜���\�����邽�߂����̃��\�b�h
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

}
