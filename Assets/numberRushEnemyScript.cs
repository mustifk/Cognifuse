using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numberRushEnemyScript : MonoBehaviour
{
    GameObject sprite,limbs;
    bool RL, start = false, isPatrolling = false, patrolOver = false;
    float moveSpeed,patrolSpeed;
    Transform player;
    Rigidbody2D rb, rbOrg;
    Vector2 movement,temp;
    // Start is called before the first frame update
    void Start()
    {

        switch (this.GetComponentInParent<numberRushScript>().GetDifficulty())
        {
            case 2:
                moveSpeed = 3f;
                break;
            case 3:
                moveSpeed = 4f;
                break;
            default:
                moveSpeed = 2f;
                break;
        }
        limbs = this.transform.GetChild(1).gameObject;
        sprite = this.transform.GetChild(0).gameObject;
        RL = Random.Range(0, 2) == 1;
        rbOrg = this.GetComponent<Rigidbody2D>();
        rb = limbs.GetComponent<Rigidbody2D>();
        temp = new Vector2(Random.Range(-7, 7), Random.Range(-4, 4));
    }

    // Update is called once per frame
    void Update()
    {
        //rotate effect
        if (RL)
        {
            sprite.transform.Rotate(new Vector3(0,0,1f),-0.5f);
        }
        else
        {
            sprite.transform.Rotate(new Vector3(0,0,1f),0.5f);
        }
        player = this.transform.parent.Find("Pointer").GetComponent<Transform>();
        Vector3 direction = player.position - rbOrg.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        if (player.position != new Vector3(0, -4.5f, 10)) 
        {
            start = true;
        }
    }

    private void FixedUpdate()
    {
        
        if (start)
        {
            if (isPatrolling && !patrolOver)
            {
                transform.position = Vector2.MoveTowards(transform.position, temp , Time.deltaTime * patrolSpeed);
                if (transform.position == new Vector3(temp.x,temp.y,transform.position.z))
                {
                    patrolOver = true;
                    StartCoroutine(Wait());
                }
            }
            else if(!isPatrolling)
            {
                moveChar();
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f * patrolSpeed);
        temp = new Vector2(Random.Range(-7, 7), Random.Range(-4, 4));
        patrolOver = false;
    }

    void moveChar()
    {
        rbOrg.transform.position = Vector2.MoveTowards(rbOrg.transform.position,player.position,(moveSpeed * Time.deltaTime));
    }
    public void Follower()
    {
        this.GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    public void Patrol(int x)
    {
        isPatrolling = true;
        patrolSpeed = 1.5f + x;
    }
}
