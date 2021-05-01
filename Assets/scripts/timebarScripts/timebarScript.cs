using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timebarScript : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    bool isTicking = false;
    float temp,temp2;
    public AudioSource time;
    // Start is called before the first frame update
    void Start()
    {
        temp = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        //fill.color = Color.Lerp(fill.color, new Color(256, 0, 0), (Time.realtimeSinceStartup - temp) / slider.maxValue * 0.005f);
        if (isTicking)
        {
            temp2 = ((slider.maxValue - (Time.realtimeSinceStartup - temp)) / slider.maxValue);
            fill.color = (Color.white * temp2 + Color.red * (1 - temp2));
            slider.value = slider.maxValue - (Time.realtimeSinceStartup - temp);
            if (slider.value == 0)
            {
                Stop();
            }
        }
    }

    
    public float GetTime()
    {
        return slider.value;
    }

    public void Begin()
    {
        time.Play();
        temp = Time.realtimeSinceStartup;
        isTicking = true;
    }
    public void Stop()
    {
        time.Stop();
        isTicking = false;
    }
    public void SetMax(int x)
    {
        slider.maxValue = x;
        slider.value = x;
    }
    public void SetMax(float x)
    {
        slider.maxValue = x;
        slider.value = x;
    }
    public float GetMax()
    {
        return slider.maxValue;
    }
}
