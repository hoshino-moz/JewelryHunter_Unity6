using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody; //Player�ɂ��Ă���RigidBody2D���������߂̕ϐ�

    float axisH ; //���͂̕������L�����邽�߂̕ϐ�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     �@�@rbody = GetComponent<Rigidbody2D>(); //Player�ɂ��Ă���R���|�[�l���g�����擾
    }

    // Update is called once per frame
    void Update()
    {
        //���������������̃L�[�������ꂽ��
        //if (Input.GetAxisRaw ("Horizontal") != 0 )
        
            //Velocity�̌��ƂȂ�l�̎擾 (�E�Ȃ�1.0f�A���Ȃ�-1.0f�A�����Ȃ����0 )
            axisH = Input.GetAxisRaw("Horizontal");

            //Velocity ���
            rbody.linearVelocity = new Vector2( axisH, 0)
        
    }
}
