using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonscript : MonoBehaviour
{
    SpriteRenderer sprt;
    // Start is called before the first frame update
    void Start()
    {
        sprt = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnMouseDown()
    {
        Debug.Log("Pressed");
        this.GetComponentInParent<EngineScript>().Press(this.gameObject);
    }

    public void Blink()
    {
        sprt.color = Color.Lerp(Color.red,Color.yellow + Color.red, 0.6f);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.4f);
        sprt.color = Color.Lerp(Color.white, Color.red, 1f);
    }
}
