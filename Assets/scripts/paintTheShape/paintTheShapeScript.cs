using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sağda solda iki şekil oluşuyor fakat yanmıyor
///
/// onların birleşimini ortada yapmalı- bu fonksiyon eksik
/// randomizer çalışıyor sanırım ama yanmıyor
/// 
/// kontrol mekanizmasına bak
/// 
/// 
/// </summary>

public class paintTheShapeScript : MonoBehaviour
{
    public GameObject block, blockM,BlocksRight,BlocksLeft;
    GameObject currentBlock,tempGO;
    GameObject[,] blockList;
    public int[,] blocks1, blocks2, validBlocks;
    public int difficulty;
    int gap = 1,arrLength;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Difficulty();
        blockList = new GameObject[5,5];
        blocks1 = new int[5,5];
        blocks2 = new int[5,5];
        validBlocks = new int[5,5];
       
        switch (difficulty)
        {
            case 2:
                Camera.main.transform.position = new Vector3(1.5f, 1.5f, 0);
                Camera.main.orthographicSize = 3f;
                //this.gameObject.transform.position += new Vector3(-2.25f, -2.25f, 0);
                currentBlock = blockM;
                arrLength = 4;
                break;
            case 3:
                Camera.main.transform.position = new Vector3(2f, 2f, 0);
                Camera.main.orthographicSize = 4f;
                //this.gameObject.transform.position += new Vector3(-2.5f, -2.5f, 1f);
                currentBlock = blockM;
                arrLength = 5;
                break;
            default:
                gap = 2;
                Camera.main.transform.position = new Vector3(2f, 2f, 0);
                //this.gameObject.transform.position += new Vector3(-2f,-2f,1f);
                currentBlock = block;
                arrLength = 3;
                break;
        }

        do
        {
            Randomizer(blocks1);
            Randomizer(blocks2);
        } 
        while (!Validator());
        
        for (int x = 0; x < arrLength; x++)
        {
            for (int y = 0; y < arrLength; y++)
            {
                blockList[x, y] = Instantiate(currentBlock, new Vector3(x * gap, y * gap, 1), Quaternion.identity, this.transform);
                tempGO = Instantiate(currentBlock, new Vector3(x * gap, y * gap, 1), Quaternion.identity, BlocksLeft.transform) as GameObject;
                tempGO.GetComponent<blockScript>().ChangeState(blocks1[x, y]);
                tempGO = Instantiate(currentBlock, new Vector3(x * gap, y * gap, 1), Quaternion.identity, BlocksRight.transform) as GameObject;
                tempGO.GetComponent<blockScript>().ChangeState(blocks2[x, y]);
            }
        }
        BlocksRight.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        BlocksLeft.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        if (difficulty == 2)
        {
            BlocksRight.transform.position = new Vector3(Camera.main.transform.position.x + 3f, Camera.main.transform.position.y / 2, 3);
            BlocksLeft.transform.position = new Vector3(Camera.main.transform.position.x - 4.5f, Camera.main.transform.position.y / 2, 3);
        }
        else
        {
            BlocksRight.transform.position = new Vector3(Camera.main.transform.position.x + 4f, Camera.main.transform.position.y / 2, 3);
            BlocksLeft.transform.position = new Vector3(Camera.main.transform.position.x - 6f, Camera.main.transform.position.y / 2, 3);
        }
       
        
    }

    void Update()
    {
        
    }

    public void Press()
    {
        int complete = 0;
        for (int x = 0; x < arrLength; x++)
        {
            for (int y = 0; y < arrLength; y++)
            {
                if (blockList[x,y].GetComponent<blockScript>().CurrentState() < validBlocks[x,y])
                {
                    Terminate();
                    GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame(10, false);
                }
                else if (blockList[x,y].GetComponent<blockScript>().CurrentState() == validBlocks[x,y])
                {
                    complete++;
                }
            }
        }
        if (complete == arrLength * arrLength)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame(10, true);
        }
    }

    bool Validator()
    {
        int count = arrLength * arrLength;
        for (int x = 0; x < arrLength; x++)
        {
            for (int y = 0; y < arrLength; y++)
            {
                if (blocks1[x,y] >= blocks2[x,y])
                {
                    validBlocks[x, y] = blocks1[x, y];
                }
                else
                {
                    validBlocks[x, y] = blocks2[x, y];
                }
                if (validBlocks[x, y] == 1)
                {
                    count--;
                }
            }
        }
        return !(count == 0);
    }

    void Randomizer(int[,] blocks)
    {
        int temp = Random.Range(0, 3);
        int[] states;
        states = new int[3];
        switch (difficulty)
        {
            case 2:
                states[0] = 9;
                states[1] = 7;
                //states[2] = 6;
                break;
            case 3:
                states[0] = 13;
                states[1] = 12;
                //states[2] = 9;
                break;
            default:
                states[0] = 5;
                states[1] = 4;
                //states[2] = 3;
                break;
        }
        for (int x = 0; x < arrLength; x++)
        {
            for (int y = 0; y < arrLength; y++)
            {
                temp = Random.Range(0, 2);
                while (states[temp] == 0)
                {
                    temp = Random.Range(0, 2);
                }
                states[temp]--;
                blocks[x, y] = temp;
            }
        }
    }

    void Terminate()
    {
        for (int i = 0; i < arrLength; i++)
        {
            for (int k = 0; k < arrLength; k++)
            {
                blockList[i, k].GetComponent<blockScript>().ColliderOff();
            }
        }
    }
}
