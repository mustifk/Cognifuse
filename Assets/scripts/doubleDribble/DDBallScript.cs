using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDBallScript : MonoBehaviour
{
    public AudioSource correct, incorrect;
    bool left;
    static bool lose = false;
    // Start is called before the first frame update
    void Start()
    {
        left = true;
        if (this.transform.position.x > 0)
        {
            left = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "DropletX")
        {
            
            if (!lose)
            {
                incorrect.Play();
                lose = true;
            }
            Destroy(collision.gameObject);
            this.transform.parent.GetComponent<DDBallsEngine>().GameOver();
        }
        else
        {
            if (!lose)
            {
                correct.Play();
            }
            collision.transform.parent.GetComponent<DDBallsEngine>().DropletGone();
            Destroy(collision.gameObject);
        }
    }

    public void Move()
    {
        left = !left;
        if (left)
        {
            this.transform.position = new Vector2(this.transform.position.x + 3, this.transform.position.y);
        }
        else
        {
            this.transform.position = new Vector2(this.transform.position.x - 3, this.transform.position.y);
        }
    }


}
