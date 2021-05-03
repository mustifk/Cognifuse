using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class endScript : MonoBehaviour
{
    GameObject main;
    public TextMeshProUGUI score,bestScore,Cogni,mainmenu,again,details;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("Player");
        score.text = main.GetComponent<mainScript>().getScore();
        if (main.GetComponent<mainScript>().Language() == 0)
        {
            mainmenu.text = "Ana Menu";
            again.text = "Tekrar Oyna";
            details.text = "Detayli Skor";
            Cogni.text = "CogniFuselendin";
            bestScore.text = "En yuksek skor " + main.GetComponent<mainScript>().getBestScore();
        }
        else
        {
            mainmenu.text = "Main Menu";
            again.text = "Play Again";
            details.text = "Detailed Score";
            Cogni.text = "CogniFused";
            bestScore.text = "Your best score is " + main.GetComponent<mainScript>().getBestScore();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().tag = "OldScript";
        SceneManager.LoadScene("Main");
    }

    public void PlayAgain()
    {
        main.GetComponent<mainScript>().PlayAgain();
    }

    public void Details()
    {
       
    }
}
