using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropletScript : MonoBehaviour
{

    bool blue;
    static bool isGameOver;
    float difficulty;
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        difficulty = this.transform.parent.GetComponent<DDBallsEngine>().GetDifficulty();
        blue = false;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -4 + -1f * difficulty);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (this.transform.position.y < -6f)
        {
            this.transform.parent.GetComponent<DDBallsEngine>().DropletGone();            
            Destroy(this.gameObject);
        }
        else if (this.transform.position.y < -5f && this.transform.tag != "DropletX")
        {
            if (!isGameOver)
            {
                this.transform.parent.GetComponent<DDBallsEngine>().GameOver();
                isGameOver = true;
            }
        }
    }

    public void Wrong()
    {
        this.gameObject.tag = "DropletX";
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

    public void Red()
    {
        blue = false;
        this.GetComponent<SpriteRenderer>().color = Color.red;
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
            Red();
            if (Random.Range(0, 2) == 1)
            {
                Wrong();
            }
        }
        else
        {
            transform.position = new Vector3(2, 7);
            if (Random.Range(0, 2) == 1)
            {
                transform.position = new Vector3(5, 7);
            }
            Blue();
            if (Random.Range(0, 2) == 1)
            {
                Wrong();
            }
        }
    }
}


