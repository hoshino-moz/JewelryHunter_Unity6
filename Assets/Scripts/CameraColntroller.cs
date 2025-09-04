using UnityEngine;

public class CameraColntroller : MonoBehaviour
{
    GameObject player;
    float x, y, z; //�J�����̍��W�����߂�ϐ�

    [Header("�J�����̌��E�l")]
    public float leftLimit;
    public float rightLimit;
    public float bottomLimit;
    public float topLimit;

    [Header("�J�����̃X�N���[��")]
    public bool isScrollX; //�������ɋ����X�N���[��
    public float scrollSpeedX = 0.5f;
    public bool isScrollY; //�������ɋ����X�N���[��
    public float scrollSpeedY = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Player�^�O���������Q�[���I�u�W�F�N�g��T���āA�ϐ�player�ɑ��
        player = GameObject.FindGameObjectWithTag("Player");
        //�J������Z���W�͏����l�̂܂܂��ێ�������
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //��������v���C���[��x���W�Ay���W�̈ʒu��ϐ��Ɏ擾
        x = player.transform.position.x;
        y = player.transform.position.y;

        //X�����̋����X�N���[��
        if (isScrollX )
        {
            x = transform.position.x + (scrollSpeedX * Time.deltaTime) ;
        }

        //���E�̃X�N���[�����~�b�g
        if (x < leftLimit)
        {
            x = leftLimit;
        }
        else if (x > rightLimit)
        {
            x = rightLimit;
        }

        //Y�����̋����X�N���[��
        if (isScrollY)
        {
            y = transform.position.y + (scrollSpeedY * Time.deltaTime) ;
        }

        //�㉺�̃X�N���[�����~�b�g
        if (y < bottomLimit)
        {
            y = bottomLimit;
        }
        else if (y > topLimit)
        {
            y = topLimit;
        }

        //��茈�߂��e�ϐ��̒l���J�����̃|�W�V�����Ƃ���
        transform.position = new Vector3(x, y, z);

    }
}
