using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class transScript : MonoBehaviour
{
    public TextMeshProUGUI text,title;
    public float transitionDuration = 8f;
    public int sceneIndex = 0;
    private string[][] instructions;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    GameObject brain;

    GameObject[] brains;

    bool changed2 = false, changed1 = false;
    float startTime, speed = 1.0f;

    static int number_of_brains;

    // Start is called before the first frame update
    void Start()
    {
        instructions = new string[20][];
        for (int i = 0; i < 20; i++)
        {
            instructions[i] = new string[2];
        }
        instructions[0][0] = "This Is Title";
        instructions[0][1] = "Here we explain the minigame!";
        instructions[1][0] = "Guess The Item";
        instructions[1][1] = "Find the correct item that is under the magnifying glass!";
        instructions[2][0] = "Correct Side";
        instructions[2][1] = "Place the item to the correct size but be careful, sides are changing!";
        instructions[3][0] = "Tap The Dot";
        instructions[3][1] = "Only press the blinking button if it is circle and yellow!";
        instructions[4][0] = "Quick Maths";
        instructions[4][1] = "Answer the given basic math questions,be quick!";
        instructions[5][0] = "Have You Seen This";
        instructions[5][1] = "Follow the objects in the screen and then, have you seen this?";
        instructions[6][0] = "Camel Dwarf";
        instructions[6][1] = "Press the screen to do a camel or dwarf, be cautious about the current status!";
        instructions[7][0] = "Shape Match";
        instructions[7][1] = "Find the correct shapes among all and select them!";
        instructions[8][0] = "Double Dribble";
        instructions[8][1] = "Catch the circles with the same color and avoid vice versa!";
        instructions[9][0] = "Color Confusion";
        instructions[9][1] = "Result is correct iff text in left part is the color of the right part!";
        instructions[10][0] = "Match The Cards";
        instructions[10][1] = "Try to find the other twin of all the cards on the screen!";
        instructions[11][0] = "Word Quest";
        instructions[11][1] = "Try to find the hidden words written on the left before the time is up!";
        instructions[12][0] = "Which One";
        instructions[12][1] = "Watch carefully to catch the one which was't on the previous screen!";
        instructions[13][0] = "Number Rush";
        instructions[13][1] = "Try to collect all numbers in order, avoid the enemies!";
        instructions[14][0] = "Paint The Shape";
        instructions[14][1] = "Try to figure out the final shape when the shapes on left and right become one!";
        instructions[15][0] = "Simon Says";
        instructions[15][1] = "Wait for the buttons to blink, then push them in the correct order!";
        createBrains();
        startTime = Time.time;

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().GetHP() == number_of_brains && number_of_brains == 2)
            changed2 = true;
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().GetHP() == number_of_brains && number_of_brains == 1)
        {
            changed2 = true;
            changed1 = true;
        }

        number_of_brains = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().GetHP();
  
        CurrentSceneSelector();
        StartCoroutine(NextScene());
    }

    // Update is called once per frame
    void Update()
    {
        if(number_of_brains == 2 && !changed2)
        {
            float t = (Time.time - startTime) * speed;
            brains[2].transform.GetComponent<Image>().color = Color.Lerp(Color.white, new Color(0.3568628f, 0.3490196f, 0.3490196f, 255), t);
        }
        if (number_of_brains == 1 && !changed1)
        {
            float t = (Time.time - startTime) * speed;
            brains[1].transform.GetComponent<Image>().color = Color.Lerp(Color.white, new Color(0.3568628f, 0.3490196f, 0.3490196f, 255), t);
            brains[2].GetComponent<Image>().color = new Color32(91, 89, 89, 255);
        }
        else
        {
            if(changed1 && changed2)
            {
                brains[1].GetComponent<Image>().color = new Color32(91, 89, 89, 255);
                brains[2].GetComponent<Image>().color = new Color32(91, 89, 89, 255);
            }
            else if(changed1)
                brains[1].GetComponent<Image>().color = new Color32(91, 89, 89, 255);
            else if(changed2)
                brains[2].GetComponent<Image>().color = new Color32(91, 89, 89, 255);
        }

    }

    void createBrains()
    {
        brains = new GameObject[3];
        brains[0] = Instantiate(brain, new Vector3(-4, 3.9f), Quaternion.identity, canvas.transform);
        brains[1] = Instantiate(brain, new Vector3(0, 3.9f), Quaternion.identity, canvas.transform);
        brains[2] = Instantiate(brain, new Vector3(4, 3.9f), Quaternion.identity, canvas.transform);
    }

    IEnumerator NextScene()
    {
        
        yield return new WaitForSeconds(transitionDuration);
        GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().NextScene();
    }

    public void NextSceneImmediate()
    {
        StopAllCoroutines();
        GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().NextScene();

    }

    void CurrentSceneSelector()
    {
        title.text = instructions[GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().CurrentSceneIndex()][0];
        text.text = instructions[GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().CurrentSceneIndex()][1];
    }

}
