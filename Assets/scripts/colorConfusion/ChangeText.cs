using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeText : MonoBehaviour
{

    [SerializeField] private Text myText=GameObject.Find("Canvas/Text").GetComponent<Text>();
    [SerializeField] private Text myText2;

    string[] colorNames = {"Red","Blue","Black","Green"}; 
    private Color newColor;
    private string newString;
   
    
    void Start()
    {
        myText. = colorNames[2];
       
        
    }
}
