using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDMainLineScript : MonoBehaviour
{
    float dif;
    public GameObject Main,line;
    // Start is called before the first frame update
    void Start()
    {
        dif = Main.GetComponent<DDBallsEngine>().GetDifficulty();
        StartCoroutine(Begin());
    }

    IEnumerator Begin()
    {
        GameObject temp;
        for (int i = 0; i < 1000; i++)
        {
            temp = Instantiate(line, new Vector3(0, 0), Quaternion.identity, this.transform);
            temp.transform.position = new Vector3(-3.5f, 8);
            temp = Instantiate(line, new Vector3(0, 0), Quaternion.identity, this.transform);
            temp.transform.position = new Vector3(3.5f, 8);
            yield return new WaitForSeconds(2f - dif * 0.4f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 Velocity()
    {
        return new Vector2(0, -4 + -1f * dif);
    }
}
