using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endCanvasScript : MonoBehaviour
{
    static int counter = 0;
    // Start is called before the first frame update
    public GameObject End, Detail;

    public void ChangeCanvas()
    {
        End.SetActive(counter % 2 == 1);
        Detail.SetActive(counter % 2 == 0);
        counter++;
    }
}
