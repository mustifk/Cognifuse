using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allCamScript : MonoBehaviour
{
    private void Start()
    {
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Listening())
        {
            AudioListener.volume = 0;
        }
    }
}
