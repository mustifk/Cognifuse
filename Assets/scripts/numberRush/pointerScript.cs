using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointerScript : MonoBehaviour
{
    public GameObject trail;
    GameObject currentTrail;
    bool isCollecting = false,isDisabled = false;
    Rigidbody2D rb;
    Camera mainCam;
    void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if(!isDisabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCollecting();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopCollecting();
            }
            if (isCollecting)
            {
                UpdateCollector();
            }
        }
    }

    void UpdateCollector()
    {
        rb.position = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    void StartCollecting()
    {
        isCollecting = true;
        currentTrail = Instantiate(trail, new Vector3(transform.position.x,transform.position.y,-2),Quaternion.identity,this.transform);
        transform.parent.GetComponent<numberRushScript>().Begin();
        this.GetComponent<CircleCollider2D>().enabled = true;
    }

    void StopCollecting()
    {
        isCollecting = false;
        currentTrail.transform.SetParent(null);
        Destroy(currentTrail,1f);
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.transform.parent.GetComponent<numberRushScript>().CheckDone();
    }

    public void Disable()
    {
        this.GetComponent<CircleCollider2D>().enabled = false;
        isDisabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Collectible")
        {
            this.transform.parent.GetComponent<numberRushScript>().Collision(collision.collider.GetComponent<collectableScript>().GetNumber());
            Destroy(collision.gameObject);
        }
        else if (collision.collider.tag == "Enemy")
        {
            this.transform.parent.GetComponent<numberRushScript>().Collision(-1);
        }
    }
}
