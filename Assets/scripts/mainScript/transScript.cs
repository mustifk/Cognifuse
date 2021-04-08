using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class transScript : MonoBehaviour
{
    public TextMeshProUGUI text,title;
    public float transitionDuration = 8f;
    public int sceneIndex = 0;
    private string[][] instructions;
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
        instructions[1][0] = "Simon Says";
        instructions[1][1] = "Wait for the buttons to blink, then push them in the correct order!";
        instructions[2][0] = "Tap The Dot";
        instructions[2][1] = "Only press the blinking button if it is circle and yellow!";
        instructions[3][0] = "Match The Cards";
        instructions[3][1] = "Try to find the other twin of all the cards on the screen!";
        instructions[4][0] = "Color Confusion";
        instructions[4][1] = "Result is correct iff text in left part is the color of the right part!";
        instructions[5][0] = "Double Dribble";
        instructions[5][1] = "Catch the circles with the same color and avoid vice versa!";
        instructions[6][0] = "Have You Seen This";
        instructions[6][1] = "Follow the objects in the screen and then, have you seen this?";
        instructions[7][0] = "Paint The Shape";
        instructions[7][1] = "Try to figure out the final shape when the shapes on left and right become one!";
        instructions[8][0] = "Shape Match";
        instructions[8][1] = "Find the correct shapes among all and select them!";
        instructions[9][0] = "Which One";
        instructions[9][1] = "Watch carefully to catch the one which was't on the previous screen!";
        instructions[10][0] = "Word Quest";
        instructions[10][1] = "Try to find the given words on the word puzzle!";
        CurrentSceneSelector();
        StartCoroutine(NextScene());
    }

    // Update is called once per frame
    void Update()
    {
        
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
