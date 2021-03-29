using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropletScript : MonoBehaviour
{

    bool blue;
    float difficulty;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = this.transform.parent.GetComponent<DDBallsEngine>().GetDifficulty();
        blue = false;
        transform.tag = "DropletO";
        this.GetComponent<SpriteRenderer>().color = Color.red;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1.6f * difficulty);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (this.transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    public void Wrong()
    {
        this.transform.tag = "DropletX";
        if (blue)
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    public void Blue()
    {
        blue = true;
        this.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    public void Position()
    {
        if (!blue)
        {
            transform.position = new Vector3(-2, 7);
            if (Random.Range(0, 2) == 1)
            {
                transform.position = new Vector3(-5, 7);
            }
        }
        else
        {
            transform.position = new Vector3(2, 7);
            if (Random.Range(0, 2) == 1)
            {
                transform.position = new Vector3(5, 7);
            }
        }
    }
}


