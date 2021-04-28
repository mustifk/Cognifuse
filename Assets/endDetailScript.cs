using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class endDetailScript : MonoBehaviour
{
    // Start is called before the first frame update
    int[] CBS = new int[5], CCS = new int[5];

    public TextMeshProUGUI CurrentOrange, BestOrange, CurrentRed, BestRed, CurrentYellow, BestYellow, CurrentPurple, BestPurple, CurrentGreen, BestGreen;
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

    }

}
