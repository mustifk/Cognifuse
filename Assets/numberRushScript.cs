using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// ENEMYLERİN BAZISI YERİNDE SAYSIN ONU DENE
/// TAKİPTEYKEN YAKLAŞINDA YAVAŞLIYORLAR BAYA
/// 
/// /// </summary>

public class numberRushScript : MonoBehaviour
{
    public GameObject collectible,enemy;
    GameObject[] collectibles,enemies;
    int[,] coord;
    public int difficulty = 3;
    int collectibleCount,enemyCount,nextToCount = 1;
    bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
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
                collectibleCount = 7;
                break;
            case 3:
                collectibleCount = 10;
                enemyCount = 4;
                break;
            default:
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
            enemies[i].GetComponent<numberRushEnemyScript>().Move(difficulty);
            if (i != 1)
            {
                enemies[i].GetComponent<numberRushEnemyScript>().Patrol(difficulty);
            }
            coord[(int)temp.x + 7, (int)temp.y + 4] = 1;

        }
    }

    public void Begin()
    {
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
            nextToCount++;
            if (nextToCount == collectibleCount + 1)
            {
                isGameOver = true;
                Debug.Log("WON");
            }
        }
        else
        {
            isGameOver = true;
            Debug.Log("LOSE");
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
            isGameOver = true;
        }
    }
}
