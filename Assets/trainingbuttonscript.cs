using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainingbuttonscript : MonoBehaviour
{
    // Start is called before the first frame update
    public void BeginTraining(int x)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().startTraining(x, GameObject.FindGameObjectWithTag("trainingEditor").GetComponent<TrainingSceneScript>().getDif());
    }
}
