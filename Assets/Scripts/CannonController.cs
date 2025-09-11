using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("�����v���n�u/����/���x/�͈�")]
    public GameObject objPrefab; //���˂�����v���n�u�f�[�^
    public float delayTime = 3.0f;//�C���^�[�o������
    public float fireSpeed = 4.0f;//���ˑ��x
    public float length = 8.0f; //�͈�

    [Header("���ˌ�")]
    public Transform gateTransfome;

    GameObject player;  //�v���C���[
    float passedTime = 0; //�o�ߎ���

    //�����`�F�b�N
    bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        float d = Vector2.Distance(transform.position, targetPos);
        if (length >= d)
        {
            ret = true;
        }
        return ret;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //player���擾
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

            //�ҋ@���ԉ��Z
            passedTime += Time.deltaTime;
            //Player�Ƃ̋����`�F�b�N
            if (CheckLength(player.transform.position))
            {
                //�ҋ@���Ԍo��
                if (passedTime > delayTime)
                {
                    {
                        passedTime = 0;
                        //�C�e���v���n�u������
                        Vector2 pos = new Vector2(gateTransfome.position.x, gateTransfome.position.y);
                        GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
                        //�C�g�������Ă���ق��ɔ��˂���
                        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
                        float angleZ = transform.localEulerAngles.z;
                        float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
                        float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);
                        Vector2 v = new Vector2(x, y) * fireSpeed;
                        rbody.AddForce(v, ForceMode2D.Impulse);


                    }
                }
            }

        }

    }


    //�͈͕\��
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, length);
    }
}
