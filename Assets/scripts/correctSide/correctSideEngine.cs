using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class correctSideEngine : MonoBehaviour
{
    //1 -> Easy
    //2 -> Normal
    //3 -> Hard
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
    float[] scaleX = { 2, 1.5f, 1 };
    float[] scaleY = { 2, 1.5f, 1 };

    // float[] posButtonsX = { -5.95f, -3.68f };
    //  float[] posButtonsY = { 5.95f, -3.68f };
    //float[] posButtonsX = { -5.92f, 6.01f };
    //float[] posButtonsY = { -1.88f, -1.88f };

    int indexOfShape = 0;

    [SerializeField]
    GameObject leftButton;

    [SerializeField]
    GameObject rightButton;

    GameObject leftShape, rightShape;

    GameObject[] objects;

    void Start()
    {
        switch(diffLevel)
        {
            case 1:
                numberOfShape = 5;
                break;
            case 2:
                numberOfShape = 10;
                break;
            case 3:
                numberOfShape = 15;
                break;
        }
        objects = new GameObject[numberOfShape+1];

        assignShapes();

        Instantiate(leftButton, new Vector3(-5.95f, -3.68f), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(rightButton, new Vector3(5.95f, -3.68f), Quaternion.identity).transform.parent = gameObject.transform;

        createShapes();
    }
    void assignShapes()
    {
        GameObject[] shapesArray = { triangle, circle, rectangle, star };

        int number, index = 0;
        for (int i = 0; i < numberOfShape+1; i++)
        {
            number = Random.Range(0, 4);
            objects[index++] = shapesArray[number];
        }
    }

    void createShapes()
    {
        int number = Random.Range(0, 2);
        if (number == 0)
        {
            leftShape = objects[indexOfShape];
            number = Random.Range(indexOfShape+1, objects.Length);
            rightShape = objects[number];
        }
        else
        {
            rightShape = objects[indexOfShape];
            number = Random.Range(indexOfShape+1, objects.Length);
            leftShape = objects[number];
        }

        Instantiate(leftShape, new Vector3(-5.92f, -1.88f), Quaternion.identity).transform.parent = gameObject.transform;
        Instantiate(rightShape, new Vector3(6.01f, -1.88f), Quaternion.identity).transform.parent = gameObject.transform;

        GameObject shape;
        int j = 0;
        for(int i=indexOfShape;i<(indexOfShape+3);i++, j++)
        {
            shape = Instantiate(objects[i], new Vector3(posX[j], posY[j]), Quaternion.identity);
            shape.transform.localScale = new Vector3(scaleX[j], scaleY[j]);
            shape.transform.parent = gameObject.transform;
            if ((indexOfShape + 1) == objects.Length)
                break;
        }
    }

    public void checkShape(CSshape gameObject)
    {
        if(gameObject.str == "left")
        {
            if (leftShape.CompareTag(objects[indexOfShape].transform.tag))
            {
                Debug.Log(objects[indexOfShape].transform.tag);
                //GameObject.Destroy(transform.GetChild(0).gameObject);
                //Destroy(gameObject.transform.GetChild(0));
                //indexOfShape++;
                arrangeButtons();
            }
            else
            {
                Debug.Log("GAME OVER");
                gameOver();
            }
        }
        else if(gameObject.str == "right")
        {
            if (rightShape.CompareTag(objects[indexOfShape].transform.tag))
            {
                Debug.Log(objects[indexOfShape].transform.tag);
                //Destroy(gameObject.transform.GetChild(0));
                //GameObject.Destroy(transform.GetChild(0).gameObject);
                //indexOfShape++;
                arrangeButtons();
            }
            else
            {
                Debug.Log("GAME OVER");
                gameOver();
            }
        }
        
    }

    public void arrangeButtons()
    {
        indexOfShape++;
        
        int counter = 0;
        for (int i = transform.childCount - 1; counter <= 4; i--, counter++)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }

        createShapes();

        transform.GetChild(transform.childCount - 1).transform.localPosition = new Vector3(posX[2], posY[2]);
        transform.GetChild(transform.childCount - 2).transform.localPosition = new Vector3(posX[1], posY[1]);
        transform.GetChild(transform.childCount - 3).transform.localPosition = new Vector3(posX[0], posY[0]);
/*
        int random = Random.Range(0, 2);
        if(random == 0)
        {
            //left
            leftShape = transform.GetChild(transform.childCount - 3).gameObject;
            rightShape = objects[Random.Range(indexOfShape, objects.Length)];
        }
        else
        {
            //right
            rightShape = transform.GetChild(transform.childCount - 3).gameObject;
            leftShape = objects[Random.Range(indexOfShape, objects.Length)];
        }*/
    }

    void gameOver()
    {
        int childs = transform.childCount;
        for (int i = childs - 1; i > 0; i--)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }

        GameObject.Destroy(transform.GetChild(0).gameObject);
        //SceneManager.LoadScene("cardMatch");
    }

    /*  void createShapes()
      {
          int number = Random.Range(0,4);

         // int selectedShape = 0;
          CSshape temp;
       /*   switch(number)
          {
              case 0:
                  //triangle
                  temp = Instantiate(triangle);
                  break;
              case 1:
                  //circle
                  //selectedShape = 1;
                  temp = Instantiate(circle);
                  break;
              case 2:
                  //rectangle
                  //selectedShape = 2;
                  temp = Instantiate(rectangle);
                  break;
              default:
                  //star
                  //selectedShape = 3;
                  temp = Instantiate(star);
                  break;
          }

          int sortNumber = 1;

          temp.transform.position = new Vector3(0f, startPosY);
          temp.GetComponent<SpriteRenderer>().sortingOrder = sortNumber--;

          temp.transform.parent = gameObject.transform;

          int index = 0;
          objects[index++] = temp;

          CSshape[] shapesArray = { triangle, circle, rectangle, star };

          for (int i=0;i<numberOfShape-1;i++)
          {
              number = Random.Range(0, 4);
              temp = Instantiate(shapesArray[number]);
              temp.GetComponent<SpriteRenderer>().sortingOrder = sortNumber--;

              startPosY += 0.95f;

              temp.transform.parent = gameObject.transform;
              temp.transform.position = new Vector3(0f, startPosY);
              objects[index++] = temp;
          }

          // createOtherShape(selectedShape);


      }*/


    /*void createOtherShape(int selectedShape)
    {
        int number;
        CSshape[] shapesArray = new CSshape[3];
        switch(selectedShape)
        {
            case 0:
                //triangle
                shapesArray[0] = circle;
                shapesArray[1] = rectangle;
                shapesArray[2] = star;
                break;
            case 1:
                //circle
                shapesArray[0] = triangle;
                shapesArray[1] = rectangle;
                shapesArray[2] = star;
                break;
            case 2:
                //rectangle
                shapesArray[0] = circle;
                shapesArray[1] = triangle;
                shapesArray[2] = star;
                break;
            default:
                //star
                shapesArray[0] = circle;
                shapesArray[1] = rectangle;
                shapesArray[2] = triangle;
                break;
        }
        number = Random.Range(0, 3);
        Instantiate(shapesArray[number]);
    }*/
}
