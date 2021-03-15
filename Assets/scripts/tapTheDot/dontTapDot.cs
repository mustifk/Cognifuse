using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontTapDot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        gameObject.GetComponentInParent<tapTheDotEngine>().Press(true);
    }

    public void Exist(float lifespan)
    {
        StartCoroutine(Die(lifespan));
    }

    IEnumerator Die(float lifespan)
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
