using Unity.VisualScripting;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    [Header("¶‘¶ŠÔ")]
    public float deleteTime = 3.0f; //íœ‚·‚éŠÔ
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); //‰½‚©‚ÉÚG‚µ‚½‚çÁ‚·
    }
}
