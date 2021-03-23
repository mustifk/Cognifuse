using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class endScript : MonoBehaviour
{
    GameObject main;
    public TextMeshProUGUI score,bestScore;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("Player");
        score.text = main.GetComponent<mainScript>().getScore();
        bestScore.text = "Your best score is " + main.GetComponent<mainScript>().getBestScore();
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
        Debug.Log("Details and stuff!");
    }
}
