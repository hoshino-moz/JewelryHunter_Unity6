using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("player�̔\�͒l")]
    public float speed = 3.0f; //player�̃X�s�[�h�𒲐�
    public float jumpPower = 9.0f; //�W�����v��

    [Header("�n�ʔ���̑Ώۃ��C���[")]
    public LayerMask groundLayer;  //�n�ʃ��C���[���w�����邽�߂̕ϐ�


    Rigidbody2D rbody; //Player�ɂ��Ă���RigidBody2D���������߂̕ϐ�
    Animator animator; //Animator
    // Transform tr; //�{���Ȃ�@transform����

    float axisH; //���͂̕������L�����邽�߂̕ϐ�

    bool goJump = false; //�W�����v�t���O�@(True:�^ false: �U)
    bool onGround = false; //�n�ʂɃC���J�ǂ����H



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>(); //Player�ɂ��Ă���R���|�[�l���g�����擾
        // tr = GetComponent<Transform>();�@//�{���Ȃ�@transform����
        animator = GetComponent<Animator>();  //
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[���̃X�e�[�^�X��playing�łȂ��Ȃ�
        if (GameManager.gameState != "playing")
        {
            return; // ����1�t���[���������I��
        }

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
    void FixedUpdate()
    {
        //�Q�[���̃X�e�[�^�X��playing�łȂ��Ȃ�
        if (GameManager.gameState != "playing")
        {
            return; // ����1�t���[���������I��
        }

        //�@�n�ʔ�����T�[�N���L���X�g
        onGround = Physics2D.CircleCast(
            transform.position, //���ˈʒu=player�̈ʒu
            0.2f,              //Circle�̔��a
            new Vector2(0, 1.0f), //���˕���
            0,                  //���ˋ���
            groundLayer        //�ΏۂƂȂ郌�C���[���(�ϐ�)
            );

        //Velocity ���
        rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);

        //�W�����v�t���O����������
        if (goJump)
        {
            //�W�����v������
            rbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            goJump = false; //Flag��Off�ɖ߂�
        }


        //if (onGround)�@//�n�ʂ̏�ɂ���Ƃ�
        //{
            if (axisH == 0)
            {
                animator.SetBool("Run", false); //�A�C�h���A�j���ɐ؂�ւ�
            }
            else
            {
                animator.SetBool("Run", true); //Run�A�j���ɐ؂�ւ�
            }
        //}
    }

    //�W�����v�{�^���������ꂽ��
    void Jump()
    {
        if (onGround)
        {
            goJump = true;  //Jump�t���O��ON
            animator.SetTrigger("Jump");
        }
    }

    //isTrigger�����������Ă���Collider�ƂԂ������珈��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Goal")
        //�S�[���ɐڐG������GameClear
        if (collision.gameObject.CompareTag("Goal"))
        {
            GameManager.gameState = "gameclear";
            Debug.Log("�S�[���ɐڐG�����I");
            Goal();
        }

        //  ���ɗ�������(Dead)�@�Q�[���I�[�o�[
        if (collision.gameObject.CompareTag("Dead"))
        {
            GameManager.gameState = "gameover";
            Debug.Log("�Q�[���I�[�o�[�I");
            GameOver();
        }

        //�A�C�e���ɐG�ꂽ��X�R�A���Z
        if (collision.gameObject.CompareTag("ScoreItem"))
        {
            GameManager.stageScore += collision.gameObject.GetComponent<ItemData>().value;
            Destroy(collision.gameObject);
        }

    }

    //�S�[���������̃��\�b�h
    public void Goal()
    {
        animator.SetBool("Clear", true); //�N���A�A�j���ɐ؂�ւ�
        GameStop(); //�v���C���[��Velocity���~�߂郁�\�b�h
    }

    public void GameOver()
    {
        animator.SetBool("Dead", true); //�f�b�h�A�j���ɐ؂�ւ�
        GameStop();

        //�����蔻��𖳌��ɂ���
        GetComponent<CapsuleCollider2D>().enabled = false;

        //������ɔ�ђ��˂�
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

        //�v���C���[�����ԍ�(3�b)�Ŗ���
        Destroy(gameObject,3.0f);
    }

    void GameStop()
    {
        //���x��0�Ƀ��Z�b�g
        //rbody.linearVelocity = new Vector2(0, 0);
        rbody.linearVelocity = Vector2.zero;
    }
}
