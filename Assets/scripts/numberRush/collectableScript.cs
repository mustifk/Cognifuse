using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class collectableScript : MonoBehaviour
{
    public GameObject textSTR;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void Respawn()
    {
        this.transform.position = new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(-4.5f, 4.5f));
    }

    public void Number(int x)
    {
        textSTR.GetComponent<TextMeshPro>().text = x.ToString();
    }

    public int GetNumber()
    {
        return int.Parse(textSTR.GetComponent<TextMeshPro>().text);
    }
}
