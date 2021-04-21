using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tbcmini : MonoBehaviour
{
    public GameObject TB;
    public minibarScript timebar()
    {
        return TB.GetComponent<minibarScript>();
    }
}
