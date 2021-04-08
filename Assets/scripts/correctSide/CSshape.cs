using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSshape : MonoBehaviour
{
    correctSideEngine engine;

    public string str;

    private void Start()
    {
        engine = GameObject.FindGameObjectWithTag("GameController").GetComponent<correctSideEngine>();
    }

    private void OnMouseDown()
    {
        engine.checkShape(gameObject.GetComponent<CSshape>());
    }
}
