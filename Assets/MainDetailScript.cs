using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainDetailScript : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    int[] CBS = new int[5];

    public TextMeshProUGUI  BestOrange, BestRed, BestYellow, BestPurple, BestGreen, Best;
    void Start()
    {
        CBS = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().CategoricalBestScores();
        BestYellow.text = CBS[0].ToString();
        BestGreen.text = CBS[1].ToString();
        BestPurple.text = CBS[2].ToString();
        BestRed.text = CBS[3].ToString();
        BestOrange.text = CBS[4].ToString();
        Best.text = PlayerPrefs.GetInt("highscore").ToString();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}