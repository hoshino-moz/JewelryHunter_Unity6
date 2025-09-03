using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("player�̔\�͒l")]
    public float speed = 3.0f; //player�̃X�s�[�h�𒲐�
    public float jumpPower = 9.0f; //�W�����v��

    [Header("�n�ʔ���̑Ώۃ��C���[")]
    public LayerMask groundLayer;  //�n�ʃ��C���[���w�����邽�߂̕ϐ�


    Rigidbody2D rbody; //Player�ɂ��Ă���RigidBody2D���������߂̕ϐ�
    // Transform tr; //�{���Ȃ�@transform����

    float axisH; //���͂̕������L�����邽�߂̕ϐ�

    bool goJump = false; //�W�����v�t���O�@(True:�^ false: �U)
    bool onGrand = false; //�n�ʂɃC���J�ǂ����H

    

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

        //GetButtonDown���\�b�h-> �����Ɂ@�@Jump�{�^���̓X�y�[�X�L�[
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }

    //1�b�Ԃ�50��(50fps)�J��Ԃ��悤�ɐ��䂵�Ȃ���s���J��Ԃ����\�b�h
    private void FixedUpdate()
    {
        //�@�n�ʔ�����T�[�N���L���X�g
        onGrand = Physics2D.CircleCast (
            transform.position, //���ˈʒu=player�̈ʒu
            0.2f ,              //Circle�̔��a
            new Vector2(0, 1.0f) , //���˕���
            0,                  //���ˋ���
            groundLayer        //�ΏۂƂȂ郌�C���[���(�ϐ�)
            );

        //Velocity ���
        rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);

        //�W�����v
        if (goJump)
        {
            //�W�����v������
            rbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            goJump = false; //Flag��Off�ɖ߂�
        }
    }


    //�W�����v�{�^���������ꂽ��
    void Jump()
    {
        if (onGrand)
        {
            goJump = true;  //Jump�t���O��ON
        }
    }
}
