using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDBallsEngine : MonoBehaviour
{
    public GameObject droplet;
    public int difficulty = 3;
    int dropletCount;
    /// <summary>
    /// -2 / -5 / 2 / 5 ** 7 drop noktaları
    /// -6 kaybolma noktası
    /// belirli hızda insin çıksın
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

        StartCoroutine(Begin());
    }

    IEnumerator Begin()
    {
        GameObject temp;
        while (dropletCount != 0)
        {
            yield return new WaitForSeconds(2.3f - difficulty * 0.3f);
            for (int i = 0; i < 2; i++)
            {
                temp = Instantiate(droplet, new Vector3(0, 0), Quaternion.identity, transform);
                if (i == 1)
                {
                    temp.GetComponent<dropletScript>().Blue();
                }
                if (Random.Range(0, 2) == 1)
                {
                    temp.GetComponent<dropletScript>().Wrong();
                }
                temp.GetComponent<dropletScript>().Position();
            }
            dropletCount--;
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
}
