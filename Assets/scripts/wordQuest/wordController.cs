using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class wordController : MonoBehaviour
{
    //1 -> Easy     1 word
    //2 -> Normal   2 word
    //3 -> Hard     3 word
    public int diffLevel;

    string[] words = { "APPLE", "PEAR", "ORANGE", "LIMES", "PEACH", "APRICOT", "MANGO", "AVOCADO", "BANANA", "MANDARIN"};

    public Text text;

    [SerializeField]
    button button;

    button[,] buttons;

    [SerializeField]
    Canvas canvas;

    List<GameObject> selected_buttons;      //green buttons

    string word = null;     //buttons make a word

    public bool selected = false;

    //public GameObject over_panel;

    int find_number_of_word = 0;

    int startPosX, startPosY;
    int row, column;
    int indexRow, indexCol;

    int maxWord;
    int distance;

    [SerializeField]
    string tempWord;

    int[,] bitArray;
    int[] bitWord;

    void Start()
    {
        selected_buttons = new List<GameObject>();
        //maxWord = diffLevel * 2;
        maxWord = diffLevel;
        startPosX = -130;
        startPosY = 70;
        
        RectTransform rt = button.GetComponent<RectTransform>();
        switch(diffLevel)
        {
            case 1:
                row = 3;
                column = 4;
                distance = 80;
                rt.sizeDelta = new Vector2(distance, distance);
                break;
            case 2:
                row = 4;
                column = 6;
                distance = 60;
                rt.sizeDelta = new Vector2(distance, distance);
                break;
            case 3:
                row = 5;
                column = 8;
                distance = 50;
                rt.sizeDelta = new Vector2(distance, distance);
                break;
        }

        bitArray = new int[row, column];
        for(int i=0;i<row;i++)
        {
            for(int j=0;j<column;j++)
            {
                bitArray[i, j] = 0;
            }
        }

        bitWord = new int[words.Length];
        for(int i=0;i<words.Length;i++)
        {
            bitWord[i] = 0;
        }

        buttons = new button[row,column];
        createButtons();
        assignWord();
    }

    void createButtons()
    {
        //char startChar = 'A';
        char startChar = 'X';
        for(int i=0;i<row;i++)
        {
            for (int j = 0; j < column; j++)
            {
                button temp = Instantiate(button, new Vector3(startPosX, startPosY, 0), Quaternion.identity) as button;
                //button temp = Instantiate(button) as button;
                temp.transform.SetParent(canvas.transform, false);
                temp.name = startChar.ToString();
                temp.GetComponentInChildren<Text>().text = startChar.ToString();
                //startChar++;
                startPosX += distance;
                buttons[i,j] = temp;
            }
            startPosY -= distance;
            startPosX = -130;
        }
    }

    void assignWord()
    {
        for(int t = 0;t<maxWord;t++)
        {
            indexRow = Random.Range(0, row);
            indexCol = Random.Range(0, column);

            int indexWord = Random.Range(0, words.Length);
            while(bitWord[indexWord] == 1)
            {
                indexWord = Random.Range(0, words.Length);
            }
            tempWord = words[indexWord];
            bitWord[indexWord] = 1;

            for (int i = 0; i < tempWord.Length; i++)
            {
                Debug.Log(indexRow + " --- " + indexCol + "    -----    " + tempWord.Length + "  ************************");
                buttons[indexRow, indexCol].GetComponentInChildren<Text>().text = tempWord[i].ToString();
                bitArray[indexRow, indexCol] = 1;
                findPath();
            }
        }
    }

    void findPath()
    {
        int[] bit = new int[4];     //{up, down, left, right}
        for(int i=0;i<4;i++)
        {
            bit[i] = 0;
        }

        if(indexRow != 0 && bitArray[(indexRow -1),indexCol] != 1)
        {
            //up();
            //indexRow--;
            bit[0] = 1;
        }
        else if(indexRow != (row -1) && bitArray[(indexRow + 1), indexCol] != 1)
        {
            //down();
            //indexRow++;
            bit[1] = 1;
        }
        else if(indexCol != 0 && bitArray[indexRow, (indexCol - 1)] != 1)
        {
            //left();
            //indexCol--;
            bit[2] = 1;
        }
        else
        {
            //right();
            //indexCol++;
            bit[3] = 1;
        }

        int number = Random.Range(0, 4);
        while (bit[number] == 0)
            number = Random.Range(0, 4);

        switch(number)
        {
            case 0:
                indexRow--;
                break;
            case 1:
                indexRow++;
                break;
            case 2:
                indexCol--;
                break;
            case 3:
                indexCol++;
                break;
        }

    }
    public void makePath(GameObject button)
    {
        selected_buttons.Add(button);

        word = null;

        foreach(GameObject temp in selected_buttons)
        {
            word = word + temp.GetComponentInChildren<Text>().text;
            text.text = word;
        }

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            selected = false;

            makeWord();

            text.text = null;
        }
    }

    void makeWord()
    {
        foreach(string temp in words)
        {
            if(temp == word)
            {
                find_number_of_word++;

                foreach (GameObject temp2 in selected_buttons)
                    temp2.GetComponent<button>().destroy_button = true;
            }
        }

        selected_buttons.Clear();
        word = null;

        if(find_number_of_word == maxWord)
        {
            Debug.Log("Finish");
        }
    }

}
