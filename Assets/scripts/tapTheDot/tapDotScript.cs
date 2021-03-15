using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapDotScript : MonoBehaviour
{
    private bool wrong = false;
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
        gameObject.GetComponentInParent<tapTheDotEngine>().Press(wrong);
    }

    public void Wrong()
    {
        wrong = true;
    }

    public void Exist(float lifespan)
    {
        StartCoroutine(Die(lifespan));
    }

    IEnumerator Die(float lifespan)
    {
        yield return new WaitForSeconds(lifespan);
        if (!wrong)
        {
            gameObject.GetComponentInParent<tapTheDotEngine>().Press(true);
        }
        Destroy(gameObject);
    }
}
