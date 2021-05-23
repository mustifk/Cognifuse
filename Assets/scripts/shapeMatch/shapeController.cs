using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class shapeController : MonoBehaviour
{
    public Animator anim;
    public AudioSource bubble,wrong,winSound;
    public int Demo = 0;
    //timebar
    public GameObject TBC;
    timebarScript timebar;

    bool isGameover = false;
    //Easy -> 1
    //Normal -> 2
    //Hard -> 3
    int difficulty = 1;

    public shape circle;
    public shape hexagon;
    public shape rectangle;
    public shape star;
    public shape triangle;

    public TextMeshProUGUI text;

    public string message;
    private shape[] otherShapes;        //otherShapes is an array that does not contain the shape in the message

    private Sprite[] otherSprites;
    int[] otherSpriteArray;         //for different shape
    int[] sameSpriteArray;          //array filled depends on message

    public bool checkShape = true;
    public int counterShape = 0;

    private int numberOfObjects,numberOfScene;
    private int beforeTextIndex = -1;
    int lastSprite = 0;

    private string[] texts = { "circle", "hexagon", "rectangle", "star", "triangle" };

    int gameCounter;

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

        switch (difficulty)
        {
            case 1:
                numberOfObjects = 4;
                numberOfScene = 3;
                timebar.SetMax(5);
                break;
            case 2:
                numberOfObjects = 6;
                numberOfScene = 5;
                timebar.SetMax(10);
                break;
            case 3:
                numberOfObjects = 8;
                numberOfScene = 7;
                timebar.SetMax(15);
                break;
        }

        gameCounter = 0;

        otherSpriteArray = new int[numberOfObjects];
        sameSpriteArray = new int[difficulty];


        if (GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Language() == 0)
        {
            texts[0] = "daire";
            texts[1] = "altigen";
            texts[2] = "dortgen";
            texts[3] = "yildiz";
            texts[4] = "ucgen";
        }

        prepareGame();
        timebar.Begin();
    }

    private void Update()
    {
        if (!isGameover)
        {
            if (gameCounter < numberOfScene)
            {
                if (counterShape == difficulty)       //load other shapes
                {
                  
                        StartCoroutine(wait2());

                    gameCounter++;
                    counterShape = 0;
                    lastSprite = 0;
                    if (gameCounter < numberOfScene)
                    {
                        StartCoroutine(wait());
                    }
                }
            }
            else
            {
                GameOver(true);

            }
            if (timebar.GetTime() == 0 && !isGameover)
            {
                GameOver(false);
                isGameover = true;
            }
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.05f);
        prepareGame();


    }
    IEnumerator wait2()
    {
        yield return new WaitForSeconds(0.05f);
        int childs = transform.childCount;
        for (int i = childs - 1; i > 0; i--)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
        Debug.Log(transform.childCount);
        //GameObject.Destroy(transform.GetChild(0).gameObject);
    }
    IEnumerator wait3()
    {
        yield return new WaitForSeconds(0f);
        int childs = transform.childCount;
        for (int i = childs - 1; i > 0; i--)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
        if (transform.childCount > 0)
        {
            GameObject.Destroy(transform.GetChild(0).gameObject);
        }
        GameObject.Destroy(text);



    }
    void prepareGame()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            otherSpriteArray[i] = 0;
        }

        for (int i = 0; i < difficulty; i++)
        {
            sameSpriteArray[i] = 0;
        }

        int index = Random.Range(0, texts.Length);

        while(beforeTextIndex == index)     //prevent 2 same messages in successive sections
            index = Random.Range(0, texts.Length);
        beforeTextIndex = index;

        text.text = texts[index];

        message = texts[index];

        switch (message)
        {
            case "daire":
                message = "circle";
                otherShapes = new shape[] { hexagon, rectangle, star, triangle };
                break;
            case "circle":
                otherShapes = new shape[] { hexagon, rectangle, star, triangle };
                break;
            case "altigen":
                message = "hexagon";
                otherShapes = new shape[] { circle, rectangle, star, triangle };
                break;
            case "hexagon":
                otherShapes = new shape[] { circle, rectangle, star, triangle };
                break;
            case "dortgen":
                message = "rectangle";
                otherShapes = new shape[] { circle, hexagon, star, triangle };
                break;
            case "rectangle":
                otherShapes = new shape[] { circle, hexagon, star, triangle };
                break;
            case "yildiz":
                message = "star";
                otherShapes = new shape[] { circle, hexagon, rectangle, triangle };
                break;
            case "star":
                otherShapes = new shape[] { circle, hexagon, rectangle, triangle };
                break;
            case "ucgen":
                message = "ucgen";
                otherShapes = new shape[] { circle, hexagon, rectangle, star };
                break;
            case "triangle":
                otherShapes = new shape[] { circle, hexagon, rectangle, star };
                break;
        }

        Sprite[] sprites = new Sprite[numberOfObjects];

        createShapes(sprites);
    }

    private void createShapes(Sprite[] sprites)
    {
        float distanceX = 0, distanceY = 0;
        float startPosX = -6f;
        float startPosY = -4f;
        switch (difficulty)
        {
            case 1:
                startPosY = -3.5f;
                distanceX = Random.Range(3f, 4f);
                distanceY = Random.Range(1.0f, 1.5f);
                break;
            case 2:
                startPosY = -3.4f;
                distanceX = Random.Range(2.4f, 2.5f);
                distanceY = Random.Range(0.9f, 1f);
                break;
            case 3:
                startPosX = -8f;
                startPosY = -3.45f;
                distanceX = Random.Range(2.2f,2.3f);
                distanceY = Random.Range(0.6f, 0.7f);
                break;
        }

        float posx = startPosX;
        float posy = startPosY;


        ArrayList Xpos = new ArrayList(sprites.Length);
        ArrayList Ypos = new ArrayList(sprites.Length);

        for (int i = 0;i<sprites.Length;i++)
        {
            Xpos.Add(posx);
            Ypos.Add(posy);
            posx += distanceX;
            posy += distanceY;
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            shape shape;

            if (i < difficulty)
            {
                switch (message)
                {
                    case "daire":
                        message = "circle";
                        shape = circle;
                        break;
                    case "circle":
                        shape = circle;
                        break;
                    case "altigen":
                        message = "hexagon";
                        shape = hexagon;
                        break;
                    case "hexagon":
                        shape = hexagon;
                        break;
                    case "dortgen":
                        message = "rectangle";
                        shape = rectangle;
                        break;
                    case "rectangle":
                        shape = rectangle;
                        break;
                    case "yildiz":
                        message = "star";
                        shape = star;
                        break;
                    case "star":
                        shape = star;
                        break;
                    default:
                        message = "triangle";
                        shape = triangle;
                        break;
                }
                shape = Instantiate(shape) as shape;
                lastSprite = shape.changeSprite(lastSprite);
                shape.transform.localScale = new Vector2(0.3f, 0.3f);
            }
            else
            {
                int index = Random.Range(0, otherShapes.Length);
                shape = Instantiate(otherShapes[index]) as shape;
                shape.transform.localScale = new Vector2(0.3f, 0.3f);
                switch (shape.shapeName)
                {
                    case "daire":
                        shape.shapeName = "circle";
                        otherSprites = shape.circleSprite();
                        break;
                    case "circle":
                        otherSprites = shape.circleSprite();
                        break;
                    case "altigen":
                        shape.shapeName = "hexagon";
                        otherSprites = shape.hexagonSprite();
                        break;
                    case "hexagon":
                        otherSprites = shape.hexagonSprite();
                        break;
                    case "dortgen":
                        shape.shapeName = "rectangle";
                        otherSprites = shape.rectangleSprite();
                        break;
                    case "rectangle":
                        otherSprites = shape.rectangleSprite();
                        break;
                    case "yildiz":
                        shape.shapeName = "star";
                        otherSprites = shape.starSprite();
                        break;
                    case "star":
                        otherSprites = shape.starSprite();
                        break;
                    default:
                        shape.shapeName = "triangle";
                        otherSprites = shape.triangleSprite();
                        break;
                }

                while(otherSpriteArray[index] == 1)
                {
                    if(otherSpriteArray.Length > otherSprites.Length)
                        index = (index + 1) % otherSprites.Length;
                    else
                        index = (index + 1) % otherSpriteArray.Length;

                }

                shape.GetComponent<SpriteRenderer>().sprite = otherSprites[index];
                otherSpriteArray[index] = 1;
            }

            shape.transform.parent = gameObject.transform;

            int indexOfPos = Random.Range(0,Xpos.Count);
            posx = (float) Xpos[indexOfPos];
            Xpos.RemoveAt(indexOfPos);

            indexOfPos = Random.Range(0, Ypos.Count);
            posy = (float) Ypos[indexOfPos];
            Ypos.RemoveAt(indexOfPos);
            shape.transform.position = new Vector3(posx, posy, circle.transform.position.z);

        }
    }
    public void GameOver(bool win)
    {
        isGameover = true;
        StartCoroutine(wait3());

       
        StartCoroutine(End(win));
    }

    IEnumerator End(bool win)
    {
        timebar.Stop();
        if (win == true)
        {
            winSound.Play();
            anim.SetBool("true1", true);
        }
        else
        {
            anim.SetBool("false1", true);
        }
        yield return new WaitForSeconds(0.8f);
        if (Demo == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame((timebar.GetTime() / timebar.GetMax()), win);
        }
    }

    private Sprite[] shuffleImages(Sprite[] images)
    {
        Sprite[] temp = images.Clone() as Sprite[];
        for (int i = 0; i < temp.Length; i++)
        {
            Sprite tmp = temp[i];
            int random = Random.Range(i, temp.Length);
            temp[i] = temp[random];
            temp[random] = tmp;
        }
        return temp;
    }
    public void playBubble()
    {
        bubble.Play();
    }
    public void playWrong()
    {
        wrong.Play();
    }
}