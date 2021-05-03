using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class endDetailScript : MonoBehaviour
{
    // Start is called before the first frame update
    int[] CBS = new int[5], CCS = new int[5];

    public TextMeshProUGUI CurrentOrange, BestOrange, CurrentRed, BestRed, CurrentYellow, BestYellow, CurrentPurple, BestPurple, CurrentGreen, BestGreen,mem,per,att,mot,rea;
    public TextMeshProUGUI b1,b2,b3,b4,b5,s1,s2,s3,s4,s5;
    void Start()
    {
        CBS = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().CategoricalBestScores();
        CCS = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().CategoricalCurrentScores();
        CurrentYellow.text = CCS[0].ToString();
        CurrentGreen.text = CCS[1].ToString();
        CurrentPurple.text = CCS[2].ToString();
        CurrentRed.text = CCS[3].ToString();
        CurrentOrange.text = CCS[4].ToString();
        BestYellow.text = CBS[0].ToString();
        BestGreen.text = CBS[1].ToString();
        BestPurple.text = CBS[2].ToString();
        BestRed.text = CBS[3].ToString();
        BestOrange.text = CBS[4].ToString();

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
            s1.text = "Senin Skorun";
            s2.text = s1.text;
            s3.text = s1.text;
            s4.text = s1.text;
            s5.text = s1.text;
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
            s1.text = "Your Score";
            s2.text = s1.text;
            s3.text = s1.text;
            s4.text = s1.text;
            s5.text = s1.text;

        }

    }

}
