using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class wordController : MonoBehaviour
{
    public int Demo = 0;
    //timebar
    public GameObject TBC;
    timebarScript timebar;
    public AudioSource correct;
    bool isGameover = false;

    //1 -> Easy     1 word
    //2 -> Normal   2 word
    //3 -> Hard     3 word
    public int difficulty;

    //string[] words = { "GENE", "BODY", "MOOD", "NEURO", "BRAIN", "CONE", "DEEP", "LEARN", "RNA", "IONS", "TERM", "OPTIC", "PAIN", "PRION", "EYE", "ROD", "SENSE", "STEM", "ULTRA", "RAY"};
    string[] words = { "RNA", "EYE", "ROD", "RAY", "BAR", "WIT"};
    string[] four = { "BODY", "CONE", "DEEP", "TERM", "PAIN", "STEM", "IONS", "MIND", "PONS", "NOUS", "HEAD"};
    string[] five = { "NERVE", "BRAIN", "LEARN", "OPTIC", "GYRUS", "SENSE", "SMART" };


    public Text text;

    [SerializeField]
    button button;

    public Canvas canva;

    button[,] buttons;

    [SerializeField] Canvas canvas;

    List<GameObject> selected_buttons;      //green buttons

    string word = null;     //buttons make a word

    public bool selected = false;

    int find_number_of_word = 0;

    int startPosX, startPosY;
    int row, column;
    int indexRow, indexCol;
    int tempRow, tempCol;
    int[] positionsRow;
    int[] positionsCol;

    int maxWord;
    int distance;
    int indexWord;

    string tempWord;

    [SerializeField]
    TextMeshProUGUI[] texts;
    int textIndex = 0;

    int[,] bitArray;
    int[] bitWord;
    int[] bitPath;

    void Start()
    {
        //timebar
        GameObject temp = Instantiate(TBC);
        timebar = temp.GetComponent<TBCscript>().timebar();

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Language() == 0)
        {
            words[0] = "LOB";
            words[1] = "KAN";
            words[2] = "GOZ";
            words[3] = "RAY"; 
            words[4] = "HIS"; 
            words[5] = "ACI";
            four[0] = "ALGI"; 
            four[1] = "ISIK";
            four[2] = "DEHA";
            four[3] = "KARA"; 
            four[4] = "YENI";
            four[5] = "SERI";
            four[6] = "ZEKA";
            four[7] = "AKIL";
            four[8] = "KAFA";
            four[9] = "SIKI"; 
            four[10] = "OYUN";
            five[0] = "BEDEN"; 
            five[1] = "VUCUT";
            five[2] = "MOTOR";
            five[3] = "BEYIN";
            five[4] = "NORON";
            five[5] = "HEVES";
            five[6] = "SINIR";
        }
        selected_buttons = new List<GameObject>();

        if (Demo == 0)
        {
            difficulty = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Difficulty();
        }
        else
        {
            difficulty = Demo;
        }
        
        maxWord = difficulty;
        startPosX = -130;
        startPosY = 70;
        
        RectTransform rt = button.GetComponent<RectTransform>();
        switch(difficulty)
        {
            case 1:
                row = 3;
                column = 4;
                startPosX = -126;
                startPosY = 80;
                distance = 100;
                timebar.SetMax(5);
                rt.sizeDelta = new Vector2(distance, distance);
                break;
            case 2:
                row = 4;
                column = 6;
                startPosX = -178;
                startPosY = 105;
                distance = 75;
                timebar.SetMax(9);
                rt.sizeDelta = new Vector2(distance, distance);
                break;
            case 3:
                row = 5;
                column = 7;
                startPosX = -162;
                startPosY = 93;
                distance = 55;
                timebar.SetMax(14);
                rt.sizeDelta = new Vector2(distance, distance);
                break;
        }

        fillWords();
        
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

        bitPath = new int[4];
        for (int i = 0; i < 4; i++)
        {
            bitPath[i] = 0;
        }

        for (int i=0;i<difficulty;i++)
        {
            indexWord = Random.Range(0, words.Length);
            while (bitWord[indexWord] == 1)
            {
                indexWord = Random.Range(0, words.Length);
            }

            texts[textIndex++].text = words[indexWord];
            bitWord[indexWord] = 1;
        }

        buttons = new button[row,column];
        createButtons();
        assignWord();
        timebar.Begin();
    }

    void fillWords()
    {
        switch(difficulty)
        {
            case 1:
                break;
            case 2:
                words = words.Concat(four).ToArray();
                break;
            default:
                words = words.Concat(four).ToArray();
                words = words.Concat(five).ToArray();
                break;
        }
    }

    private void Update()
    {
        if (timebar.GetTime() == 0 && !isGameover)
        {
            StartCoroutine(EndOfMinigame(false));
            isGameover = true;
        }
    }

    void createButtons()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXZ";
        char startChar = 'X';
        int tempPosY = startPosY;
        for(int i=0;i<row;i++)
        {
            int tempPosX = startPosX;
            for (int j = 0; j < column; j++)
            {
                button temp = Instantiate(button, new Vector3(tempPosX, tempPosY, 0), Quaternion.identity) as button;
                temp.transform.SetParent(canvas.transform, false);
                
                startChar = chars[Random.Range(0, chars.Length)];

                temp.GetComponentInChildren<Text>().text = startChar.ToString();
                
                tempPosX += distance;
                buttons[i,j] = temp;
            }
            tempPosY -= distance;
        }
    }

    void assignWord()
    {
        for(int t = 0;t<maxWord;t++)
        {
            indexRow = Random.Range(0, row);
            indexCol = Random.Range(0, column);

            tempWord = texts[t].text;

            checkFilled();

            placeWord(tempWord);
        }
    }

    void checkFilled()
    {
        //int max = 3;
       // if (difficulty == 3)
        int max = 4;
        int counter = Random.Range(0, max);
        while (bitPath[counter] == 1)
        {
            counter = (counter + 1) % max;
        }
        switch (counter)
        {
            case 0:
                indexRow = 0;
                indexCol = 0;
                break;
            case 1:
                indexRow = (row / 2);
                indexCol = (column / 2);
                break;
            case 2:
                indexRow = row - 2;
                indexCol = column - 2;
                break;
            case 3:
                indexRow = row - 1;
                indexCol = 1;
                break;
            default:
                indexRow = 1;
                indexCol = column - 2;
                break;
        }
        bitPath[counter] = 1;
    }

    void placeWord(string text)
    {
        tempRow = indexRow;
        tempCol = indexCol;

        positionsRow = new int[text.Length];
        positionsCol = new int[text.Length];

        for (int i = 0; i < positionsRow.Length; i++)
        {
            positionsRow[i] = -1;
        }
        for (int i = 0; i < positionsCol.Length; i++)
        {
            positionsCol[i] = -1;
        }

        for (int i = 0; i < text.Length; i++)
        {
            positionsRow[i] = tempRow;
            positionsCol[i] = tempCol;
            if(i != text.Length-1)
                searchEmpty(i, text);
        }

        for(int i=0;i<text.Length;i++)
        {
            indexRow = positionsRow[i];
            indexCol = positionsCol[i];
            buttons[indexRow, indexCol].GetComponentInChildren<Text>().text = text[i].ToString();
            bitArray[indexRow, indexCol] = 1;
        }
    }

    void searchEmpty(int index, string text)
    {
        int[] bit = new int[4];     //{up, down, left, right}
        for (int i = 0; i < 4; i++)
        {
            bit[i] = 0;
        }
        //Debug.Log(text + " --> ROW: " + tempRow + " COL: " + tempCol);
        if (tempRow != 0 && bitArray[(tempRow - 1), tempCol] != 1)
        {
            //up();
            bool check = false;
            if(index!=0)
            {
                for (int i = 0; i < index; i++)
                {
                    if ((tempRow - 1) == positionsRow[i] && tempCol == positionsCol[i])
                        check = true;
                }
                if (!check && (tempRow -1) > 0)
                    bit[0] = 1;
            }
            else
            {
                bit[0] = 1;
            }
        }
        if (tempRow != (row - 1) && bitArray[(tempRow + 1), tempCol] != 1)
        {
            //down();
            bool check = false;
            if (index != 0)
            {
                for (int i = 0; i < index; i++)
                {
                    if ((tempRow + 1) == positionsRow[i] && tempCol == positionsCol[i])
                        check = true;
                }
                if (!check && (tempRow+1) < row)
                    bit[1] = 1;
            }
            else
            {
                bit[1] = 1;
            }
        }
        if (tempCol != 0 && bitArray[tempRow, (tempCol - 1)] != 1)
        {
            //left();
            bool check = false;
            if (index != 0)
            {
                for (int i = 0; i < index; i++)
                {
                    if (tempRow == positionsRow[i] && (tempCol - 1) == positionsCol[i])
                        check = true;
                }
                if (!check && (tempCol -1) > 0)
                    bit[2] = 1;
            }
            else
            {
                bit[2] = 1;
            }
        }
        if (tempCol != (column - 1) && bitArray[tempRow, (tempCol + 1)] != 1)
        {
            //right();
            bool check = false;
            if (index != 0)
            {
                for (int i = 0; i < index; i++)
                {
                    if (tempRow == positionsRow[i] && (tempCol + 1) == positionsCol[i])
                        check = true;
                }
                if (!check && (tempCol + 1) < column)
                    bit[3] = 1;
            }
            else
            {
                bit[3] = 1;
            }
        }

        int number = Random.Range(0, 4);

        for (int i = 0; i < 4; i++)
        {
            if (bit[number] == 1)
                break;
            number = (number + 1) % 4;
        }

        switch (number)
        {
            case 0:
                tempRow--;
                break;
            case 1:
                tempRow++;
                break;
            case 2:
                tempCol--;
                break;
            default:
                tempCol++;
                break;
        }
    }

   /* void findPath()
    {
        int[] bit = new int[4];     //{up, down, left, right}
        for(int i=0;i<4;i++)
        {
            bit[i] = 0;
        }

        if(indexRow != 0 && bitArray[(indexRow - 1),indexCol] != 1)
        {
            //up();
            bit[0] = 1;
        }
        if(indexRow != (row - 1) && bitArray[(indexRow + 1), indexCol] != 1)
        {
            //down();
            bit[1] = 1;
        }
        if(indexCol != 0 && bitArray[indexRow, (indexCol - 1)] != 1)
        {
            //left();
            bit[2] = 1;
        }
        if (indexCol != (column - 1) && bitArray[indexRow, (indexCol + 1)] != 1)
        {
            //right();
            bit[3] = 1;
        }

        int number = Random.Range(0, 4);
      
        for(int i=0;i<4;i++)
        {
            if (bit[number] == 1)
                break;
            number = (number + 1) % 4;
        }

        switch (number)
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
            default:
                indexCol++;
                break;
        }
    }*/

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

    public void makeWord()
    {
        foreach(string temp in words)
        {
            if(temp == word)
            {
                correct.Play();
                
                for(int i=0;i<texts.Length;i++)
                {
                    if (word == texts[i].text)
                        texts[i].color = new Color(0.586f, 0.962f, 0f, 1);
                }

                for (int i = 0; i < words.Length; i++)
                    if (words[i] == temp)
                        deleteWord(words, i);
                find_number_of_word++;

                foreach (GameObject temp2 in selected_buttons)
                    temp2.GetComponent<button>().destroy_button = true;
            }
        }

        selected_buttons.Clear();
        word = null;

        if(find_number_of_word == maxWord)
        {
            StartCoroutine(EndOfMinigame(true));
        }
    }

    IEnumerator EndOfMinigame(bool win)
    {
        timebar.Stop();
        isGameover = true;
        yield return new WaitForSeconds(0.8f);
        Finish();
        if (Demo == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame((timebar.GetTime() / timebar.GetMax()), win);
        }

    }

    void deleteWord(string[] array, int index)
    {
        if (index == (array.Length - 1))
            array[index] = "";
        else
        {
            for (int i = index; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }
        }
    }

    void Finish()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in canva.transform)
        {
            if (child.name != "Image")
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
