using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class correctSideEngine : MonoBehaviour
{
    public int Demo = 0;
    //timebar
    public GameObject TBC;
    timebarScript timebar;
    bool isGameover = false;

    //1 -> Easy
    //2 -> Normal
    //3 -> Hard
    public AudioSource correct, incorrect;
    public int diffLevel = 1;

    int numberOfShape;

    [SerializeField]
    GameObject triangle;

    [SerializeField]
    GameObject circle;

    [SerializeField]
    GameObject rectangle;

    [SerializeField]
    GameObject star;

    float[] posX = { 0, 0, -0.02f};
    float[] posY = { 0, 2.36f, 4.2f };
    float[] scaleX = { 0.45f, 0.37f, 0.28f };
    float[] scaleY = { 0.45f, 0.37f, 0.28f };

    int indexOfShape = 0;

    [SerializeField]
    GameObject leftButton;

    [SerializeField]
    GameObject rightButton;

    GameObject leftShape, rightShape;

    GameObject[] objects;

    int previousShape = -1;

    public GameObject shapeParticle;

    void Start()
    {
        //timebar
        GameObject temp = Instantiate(TBC);
        timebar = temp.GetComponent<TBCscript>().timebar();

        if (Demo == 0)
        {
            diffLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Difficulty();
        }
        else
        {
            diffLevel = Demo;
        }

        switch (diffLevel)
        {
            case 1:
                numberOfShape = 5;
                timebar.SetMax(6);
                break;
            case 2:
                numberOfShape = 8;
                timebar.SetMax(8);
                break;
            case 3:
                numberOfShape = 12;
                timebar.SetMax(10);
                break;
        }
        objects = new GameObject[numberOfShape];

        assignShapes();

        Instantiate(leftButton, new Vector3(-5.95f, -3.68f), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(rightButton, new Vector3(5.95f, -3.68f), Quaternion.identity).transform.parent = gameObject.transform;

        createShapes();

        timebar.Begin();
    }

    private void Update()
    {
        if (timebar.GetTime() == 0 && !isGameover)
        {
            isGameover = true;
            Finish(false);
        }
    }

    void assignShapes()
    {
        GameObject[] shapesArray = { triangle, circle, rectangle, star };

        int number, index = 0;
        for (int i = 0; i < numberOfShape; i++)
        {
            number = Random.Range(0, 4);
            if(previousShape == number) 
                if(number>=3)
                    number = Random.Range(0, number-1);
                else
                    number = Random.Range(number+1, 4);

            objects[index++] = shapesArray[number];
            previousShape = number;
        }
    }

    void createShapes()
    {
        int number = Random.Range(0, 2);
        if (number == 0)
        {
            leftShape = objects[indexOfShape];
            number = (indexOfShape + 1) % objects.Length;
            if (objects[number].name == leftShape.name)
                number = (indexOfShape + 1) % objects.Length;
            rightShape = objects[number];
            if (leftShape.name.Contains(rightShape.name))
                rightShape = objects[++number];

        }
        else
        {
            rightShape = objects[indexOfShape];
            number = (indexOfShape + 1) % objects.Length;
            if (objects[number].name == rightShape.name)
                number = (indexOfShape + 1) % objects.Length;
            leftShape = objects[number];
            if (rightShape.name.Contains(leftShape.name))
                leftShape = objects[++number];
        }

        Instantiate(leftShape, new Vector3(-5.92f, -1.88f), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(rightShape, new Vector3(6.01f, -1.88f), Quaternion.identity).transform.parent = gameObject.transform;

        GameObject shape;
        int j = 0;
        for(int i=indexOfShape, counter=0;counter<3 && i<objects.Length;counter++, j++)
        {
            shape = Instantiate(objects[i++], new Vector3(posX[j], posY[j]), Quaternion.identity);
            shape.transform.localScale = new Vector3(scaleX[j], scaleY[j]);
            shape.transform.parent = gameObject.transform;
            if (counter == 1)
            {
                shape.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.6f);
            }
            if (counter == 2)
            {
                shape.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
            }
        }
    }

    public void checkShape(CSshape gameObject)
    {

        if (gameObject.str == "left")
        {
            if (leftShape.CompareTag(objects[indexOfShape].transform.tag))
            {
                correct.Play();
                arrangeButtons();
            }
            else
            {
                incorrect.Play();
                Finish(false);
            }
        }
        else if(gameObject.str == "right")
        {
            if (rightShape.CompareTag(objects[indexOfShape].transform.tag))
            {
                correct.Play();
                arrangeButtons();
            }
            else
            {
                incorrect.Play();
                Finish(false);
            }
        }
        
    }

    public void arrangeButtons()
    {
        indexOfShape++;

        StartCoroutine(createShapeParticle());

        int counter = 0;
        for (int i = transform.childCount - 1; counter <= 4 && i > 1; i--, counter++)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }

        if (indexOfShape == objects.Length)
        {
            timebar.Stop();
            Finish(true);
        }
        else
        {
            createShapes();
        }
    }

    IEnumerator createShapeParticle()
    {
        GameObject temp = Instantiate(shapeParticle, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(temp);
    }

    void Finish(bool win)
    {
        timebar.Stop();
        int childs = transform.childCount;
        for (int i = childs - 1; i > 0; i--)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
        GameObject.Destroy(transform.GetChild(0).gameObject);

        StartCoroutine(End(win));
    }

    IEnumerator End(bool win)
    {
        yield return new WaitForSeconds(1);
        if (Demo == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame((timebar.GetTime() / timebar.GetMax()), win);
        }
    }
}
