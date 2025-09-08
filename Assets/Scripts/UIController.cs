using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject mainImage;
    public GameObject buttonPanel;

    public GameObject retryButton;
    public GameObject nextButton;

    public Sprite gameClearSprite;
    public Sprite gameOverSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    }

    //���C���摜���\�����邽�߂����̃��\�b�h
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

}
