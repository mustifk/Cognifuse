using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDLineScript : MonoBehaviour
{
    public GameObject Main;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody2D>().velocity = this.transform.parent.GetComponent<DDMainLineScript>().Velocity();
    }

    private void FixedUpdate()
    {
        if (this.transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }
}
