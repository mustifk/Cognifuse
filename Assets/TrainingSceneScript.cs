using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TrainingSceneScript : MonoBehaviour
{
    int trainingDiff = 1;
    public TextMeshProUGUI mem, per, rea, mot, att, swipe, diff, main, diffText;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Language() == 0)
        {
            mem.text = "Hafiza";
            att.text = "Dikkat";
            rea.text = "Muhakeme";
            mot.text = "Motor Yetenekler";
            per.text = "Algi";
            swipe.text = "Diger Turler Icin Kaydir";
            main.text = "Ana Menu";
            diffText.text = "Zorluk Seviyesi"; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTrainingScene()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().setTrainingMode(0);
        this.gameObject.SetActive(false);
    }

    public void setDif()
    {
        trainingDiff = ++trainingDiff > 3 ? 1:trainingDiff;
        diff.text = "" + trainingDiff;
    }
    public int getDif()
    {
        return trainingDiff;    
    }
}

