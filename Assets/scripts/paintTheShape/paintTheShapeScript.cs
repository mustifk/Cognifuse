using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sağda solda iki şekil oluşuyor fakat yanmıyor
///
/// onların birleşimini ortada yapmalı- bu fonksiyon eksik
/// randomizer dene
/// sağ sol cisim yerleri değişcek
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
        Randomizer();
        for (int x = 0; x < arrLength; x++)
        {
            for (int y = 0; y < arrLength; y++)
            {
                blockList[x,y] = Instantiate(currentBlock, new Vector3(x * gap, y * gap,1), Quaternion.identity, this.transform);
                tempGO = Instantiate(currentBlock, new Vector3(x * gap, y * gap, 1), Quaternion.identity, BlocksLeft.transform) as GameObject;
                tempGO.GetComponent<blockScript>().ChangeState(blocks1[x, y]);
                tempGO = Instantiate(currentBlock, new Vector3(x * gap, y * gap, 1), Quaternion.identity, BlocksRight.transform) as GameObject;
                tempGO.GetComponent<blockScript>().ChangeState(blocks2[x, y]);
            }
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
                    Debug.Log("GAMEOVER");
                }
                else if (blockList[x,y].GetComponent<blockScript>().CurrentState() == validBlocks[x,y])
                {
                    complete++;
                }
            }
        }
        if (complete == arrLength * arrLength)
        {
            Debug.Log("YOU WONN");
        }
    }

    void Randomizer()
    {
        int temp = Random.Range(0, 3);
        int[] states;
        states = new int[3];
        switch (difficulty)
        {
            case 2:
                states[0] = 5;
                states[1] = 5;
                states[2] = 6;
                break;
            case 3:
                states[0] = 8;
                states[1] = 8;
                states[2] = 9;
                break;
            default:
                states[0] = 3;
                states[1] = 3;
                states[2] = 3;
                break;
        }
        for (int x = 0; x < arrLength; x++)
        {
            for (int y = 0; y < arrLength; y++)
            {
                while (states[temp] == 0)
                {
                    temp = Random.Range(0, 3);
                }
                states[temp]--;
                blocks1[x, y] = temp;
            }
        }

        switch (difficulty)
        {
            case 2:
                states[0] = 5;
                states[1] = 5;
                states[2] = 6;
                break;
            case 3:
                states[0] = 8;
                states[1] = 8;
                states[2] = 9;
                break;
            default:
                states[0] = 3;
                states[1] = 3;
                states[2] = 3;
                break;
        }
        for (int x = 0; x < arrLength; x++)
        {
            for (int y = 0; y < arrLength; y++)
            {
                while (states[temp] == 0)
                {
                    temp = Random.Range(0, 3);
                }
                states[temp]--;
                blocks2[x, y] = temp;
            }
        }
    }
}
