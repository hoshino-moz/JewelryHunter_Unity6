using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [Header("�ړ�����")]
    public float moveX = 0.0f;
    public float moveY = 0.0f;
    public float times = 0.0f;
    public float wait = 0.0f;

    [Header("����Ă���")]
    public bool isMoveWhenOn = false;

    bool isCanMove = true;
    Vector3 startPos;
    Vector3 endPos;
    bool isReverse = false;
    float movep = 0.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector2(startPos.x + moveX, startPos.y + moveY);
        if (isMoveWhenOn)
        {
            isCanMove = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCanMove)
        {
            float distance = Vector2.Distance(startPos, endPos);
            float ds = distance / times;
            float df = ds * Time.deltaTime;
            movep += df / distance;
            if (isReverse)
            {
                transform.position = Vector2.Lerp(endPos, startPos, movep); //�t�ړ�
            }
            else
            {
                transform.position = Vector2.Lerp(startPos, endPos, movep);
            }
            if (movep >= 1.0f)
            {
                movep = 0.0f;
                isReverse = !isReverse;
                isCanMove = false;
                if (isMoveWhenOn == false)
                {
                    Invoke("Move", wait);
                }
            }
        }
    }

    //�ړ��t���O�𗧂Ă�
    public void Move()
    {
        isCanMove = true;
    }

    //�ړ��t���O�����낷
    public void Stop()
    {
        isCanMove = false;
    }

    //�ڐG�J�n
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //�ڐG�����̂��v���C���[�Ȃ�ړ����̎q�ɂ���
            collision.transform.SetParent(transform);
            if (isMoveWhenOn)
            {
                //��������ɓ����t���OON
                isCanMove = true;   //�ړ��t���O�𗧂Ă�
            }
        }
    }
    //�ڐG�I��
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //�ڐG�����̂��v���C���[�Ȃ�ړ����̎q����O��
            collision.transform.SetParent(null);
        }
    }

    //�ړ��͈͕\��
    void OnDrawGizmosSelected()
    {
        Vector2 fromPos;
        if (startPos == Vector3.zero)
        {
            fromPos = transform.position;
        }
        else
        {
            fromPos = startPos;
        }
        //�ړ���
        Gizmos.DrawLine(fromPos, new Vector2(fromPos.x + moveX, fromPos.y + moveY));
        //�X�v���C�g�̃T�C�Y
        Vector2 size = GetComponent<SpriteRenderer>().size;
        //�����ʒu
        Gizmos.DrawWireCube(fromPos, new Vector2(size.x, size.y));
        //�ړ��ʒu
        Vector2 toPos = new Vector3(fromPos.x + moveX, fromPos.y + moveY);
        Gizmos.DrawWireCube(toPos, new Vector2(size.x, size.y));
    }
}
