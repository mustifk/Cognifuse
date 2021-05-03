using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainDetailScript : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    int[] CBS = new int[5];

    public TextMeshProUGUI  BestOrange, BestRed, BestYellow, BestPurple, BestGreen, Best,mem,per,att,rea,mot;
    public TextMeshProUGUI  b1,b2,b3,b4,b5,b6;
    void Start()
    {
        CBS = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().CategoricalBestScores();
        BestYellow.text = CBS[0].ToString();
        BestGreen.text = CBS[1].ToString();
        BestPurple.text = CBS[2].ToString();
        BestRed.text = CBS[3].ToString();
        BestOrange.text = CBS[4].ToString();
        Best.text = PlayerPrefs.GetInt("highscore").ToString();
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Language() == 0)
        {
            mem.text = "Hafiza";
            per.text = "Algi";
            att.text = "Dikkat";
            rea.text = "Muhakeme";
            mot.text = "Motor Yetenekler";
            b1.text = "En Iyi Skor";
            b2.text = b1.text;
            b3.text = b1.text;
            b4.text = b1.text;
            b5.text = b1.text;
            b6.text = "En Iyi Skor";
        }
        else
        {
            mem.text = "Memory";
            per.text = "Perception";
            att.text = "Attention";
            rea.text = "Reasoning";
            mot.text = "Motor Skills";
            b1.text = "Best Score";
            b2.text = b1.text;
            b3.text = b1.text;
            b4.text = b1.text;
            b5.text = b1.text;
            b6.text = "Best Score";
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}