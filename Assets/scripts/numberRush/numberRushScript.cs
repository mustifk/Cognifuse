using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numberRushScript : MonoBehaviour
{
    public int Demo = 0;
    public GameObject collectible,enemy,TBC;
    GameObject[] collectibles,enemies;
    timebarScript timebar;
    int[,] coord;
    int difficulty;
    int collectibleCount,enemyCount,nextToCount = 1;
    bool isGameOver = false;
    public AudioSource collect ,win, lose;
    // Start is called before the first frame update
    void Start()
    {
        //timebar
        GameObject temp = Instantiate(TBC);
        timebar = temp.GetComponent<TBCscript>().timebar();
        if (Demo == 0)
        {
            difficulty = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Difficulty();
        }
        else
        {
            difficulty = Demo;
        }

        coord = new int[15,9];
        for (int i = 0; i < 15; i++)
        {
            for (int y = 0; y < 9; y++)
            {
                coord[i, y] = 0;
            }
        }
        collectibles = new GameObject[10];
        enemies = new GameObject[4];
        switch (difficulty)
        {
            case 2:
                enemyCount = 3;
                collectibleCount = 6;
                timebar.SetMax(8);
                break;
            case 3:
                timebar.SetMax(10);
                collectibleCount = 8;
                enemyCount = 4;
                break;
            default:
                timebar.SetMax(5);
                collectibleCount = 4;
                enemyCount = 2;
                break;
        }
        CollectibleGenerator();
        EnemyGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            GetComponentInChildren<pointerScript>().Disable();
        }
        if (timebar.GetTime() == 0 && !isGameOver)
        {
            GameOver(0);
        }
    }

    void CollectibleGenerator()
    {
        Vector2 temp;
        for (int i = 0; i < collectibleCount; i++)
        {
            do
            {
                temp = new Vector2(Random.Range(-7, 7), Random.Range(-4, 4));
            } while (coord[(int)temp.x + 7,(int)temp.y + 4] == 1);
            collectibles[i] = Instantiate(collectible,temp, Quaternion.identity, this.transform);
            collectibles[i].GetComponent<collectableScript>().Number(i + 1);
            coord[(int)temp.x + 7, (int)temp.y + 4] = 1;
        }
    }

    void EnemyGenerator()
    {
        Vector2 temp;
        for (int i = 0; i < enemyCount; i++)
        {
            do
            {
                temp = new Vector2(Random.Range(-7, 7), Random.Range(-4, 4));
            } while (coord[(int)temp.x + 7, (int)temp.y + 4] == 1);
            enemies[i] = Instantiate(enemy, temp, Quaternion.identity, this.transform);
            if (i != 1)
            {
                enemies[i].GetComponent<numberRushEnemyScript>().Patrol(difficulty);
            }
            else
            {
                enemies[i].GetComponent<numberRushEnemyScript>().Follower();
            }
            coord[(int)temp.x + 7, (int)temp.y + 4] = 1;

        }
    }

    public void Begin()
    {
        timebar.Begin();
        if (!isGameOver)
        {
            for (int i = 0; i < collectibleCount; i++)
            {
                collectibles[i].GetComponent<CircleCollider2D>().enabled = true;
            }
            for (int i = 0; i < enemyCount; i++)
            {
                enemies[i].GetComponent<CircleCollider2D>().enabled = true;
            }
        }
    }

    public void Collision(int x)
    {
        if (nextToCount == x)
        {
            collect.Play();
            nextToCount++;
            if (nextToCount == collectibleCount + 1)
            {
                GameOver(1);
            }
        }
        else
        {
            lose.Play();
            GameOver(0);
        }
    }

    void GameOver(int x)
    {
        timebar.Stop();
        isGameOver = true;
        if (x == 1)
        {
            StartCoroutine(End(true));
        }
        else
        {
            StartCoroutine(End(false));
        }
    }

    IEnumerator End(bool win)
    {
        yield return new WaitForSeconds(0.8f);
        if (Demo == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame((timebar.GetTime() / timebar.GetMax()), win);
        }
    }

    public int GetDifficulty()
    {
        return difficulty;
    }
    public void CheckDone()
    {
        if (nextToCount < collectibleCount + 1)
        {
            GameOver(0);
        }
    }
}
