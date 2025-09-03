using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody; //Player�ɂ��Ă���RigidBody2D���������߂̕ϐ�
                       // Transform tr; //�{���Ȃ�@transform����

    float axisH; //���͂̕������L�����邽�߂̕ϐ�
    public float speed = 3.0f; //player�̃X�s�[�h�𒲐�


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>(); //Player�ɂ��Ă���R���|�[�l���g�����擾
        // tr = GetComponent<Transform>();�@//�{���Ȃ�@transform����
    }

    // Update is called once per frame
    void Update()
    {
        //���������������̃L�[�������ꂽ��
        //if (Input.GetAxisRaw ("Horizontal") != 0 )

        //Velocity�̌��ƂȂ�l�̎擾 (�E�Ȃ�1.0f�A���Ȃ�-1.0f�A�����Ȃ����0 )
        axisH = Input.GetAxisRaw("Horizontal");

        if (axisH > 0)
        {
            //�E������
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (axisH < 0)
        {
            //��������
            transform.localScale = new Vector3(-1, 1, 1);
        }


    }

    //1�b�Ԃ�50��(50fps)�J��Ԃ��悤�ɐ��䂵�Ȃ���s���J��Ԃ����\�b�h
    private void FixedUpdate()
    {
        //Velocity ���
        rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);
    }
}
