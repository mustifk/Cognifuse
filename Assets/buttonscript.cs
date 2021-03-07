using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonscript : MonoBehaviour
{
    SpriteRenderer sprt;
    // Start is called before the first frame update
    void Start()
    {
        sprt = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnMouseDown()
    {
        this.GetComponentInParent<EngineScript>().Press(this.gameObject);
    }

    public void Blink()
    {
        sprt.color = Color.red + Color.yellow;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.4f);
        sprt.color = Color.red;
    }
}
