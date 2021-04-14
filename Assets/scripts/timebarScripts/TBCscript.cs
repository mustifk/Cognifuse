using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBCscript : MonoBehaviour
{
    public GameObject TB;
    public timebarScript timebar()
    {
        return TB.GetComponent<timebarScript>();
    }
}
