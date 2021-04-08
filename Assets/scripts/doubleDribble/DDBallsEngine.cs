using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDBallsEngine : MonoBehaviour
{
    public GameObject droplet;
    int difficulty = 3;
    int dropletCount,dCtemp;
    bool isGameover;
    /// <summary>
    /// -2 / -5 / 2 / 5 ** 7 drop noktaları
    /// -6 kaybolma noktası
    /// doğma noktasını randomize yaparsın ikisi için de 
    /// doğma süreleri için courotine kullanıcaksın
    /// doğma x pozisyonlarına göre renklerinşi verebilirsin
    /// hızı difficultiye bağla
    /// aynı zamanda didfficulty obje sayısını da versin
    /// dropletin scriptini yenilemeyi unutma
    /// mainde fonksiyonun olsun
    /// </summary>
    /// 
    // Start is called before the first frame update
    void Start()
    {
        difficulty = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Difficulty();
        isGameover = false;
        switch (difficulty)
        {
            case 2:
                dropletCount = 8;
                break;
            case 3:
                dropletCount = 12;
                break;
            default:
                dropletCount = 4;
                break;
        }
        dCtemp = 2 * dropletCount;
        StartCoroutine(Begin());
    }

    IEnumerator Begin()
    {
        GameObject temp;
        while (dropletCount != 0)
        {
            for (int i = 0; i < 2; i++)
            {
                temp = Instantiate(droplet, new Vector3(0, 0), Quaternion.identity, transform);
                if (i == 1)
                {
                    temp.GetComponent<dropletScript>().Blue();
                }
                temp.GetComponent<dropletScript>().Position();
            }
            dropletCount--;
            yield return new WaitForSeconds(2.8f - difficulty * 0.6f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetDifficulty()
    {
        return difficulty;
    }

    public void GameOver()
    {
        if (!isGameover)
        {
            isGameover = true;
            StopAllCoroutines();
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame(10, false);
        }
    }

    public void DropletGone()
    {
        dCtemp--;
        if (dCtemp == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame(10, true);
        }
    }
}
