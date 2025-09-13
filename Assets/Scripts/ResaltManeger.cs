using TMPro;
using UnityEngine;

public class ResaltManeger : MonoBehaviour
{


    public GameObject scoreText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = GameManager.totalScore.ToString();
    }

 
}
