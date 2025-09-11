using Unity.VisualScripting;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    [Header("�������m����")]
    public float length = 0.0f; //�����������m����

    [Header("���������")]
    public bool isDelete = false; //������ɍ폜

    [Header("�����蔻��I�u�W�F�N�g")]
    public GameObject deadObj;  //���S������

    bool isFell = false;   //�����t���O
    float fadeTime = 0.5f;  //�t�F�[�h�A�E�g����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Rigidbody2D�̕����������~
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
        deadObj.SetActive(false);  //���S��������\��
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //Player��T��
        if (player != null )
        {
            //player�Ƃ̋����v��
            float d = Vector2.Distance(transform.position, player.transform.position);
            if (length >= d)
            {
                Rigidbody2D rbody = GetComponent<Rigidbody2D>();
                if (rbody.bodyType == RigidbodyType2D.Static)
                {
                    //Rigidbody2D�̕����������J�n
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                    deadObj.SetActive(true);   //���S�������\��
                }
            }
        }
        if (isFell)
        {
            //��������
            //�����l��ύX���ăt�F�[�h�A�E�g������
            fadeTime -= Time.deltaTime; //
            Color col = GetComponent<SpriteRenderer>().color; //�J���[�����o��
            col.a = fadeTime;  //�����l��ύX
            GetComponent<SpriteRenderer>().color = col; //�J���[���Đݒ�
            if (fadeTime <= 0.0f)
            {
                //�����ɂȂ��������
                Destroy(gameObject);
            }
        }
    }

    //�ڐG�J�n
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDelete)
        {
            isFell = true; //�L�����N�^�[���������ǂ����̃t���O
        }
    }
    //�͈͕\��
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, length);
    }


}
