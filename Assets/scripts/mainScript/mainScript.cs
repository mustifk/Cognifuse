using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SAHNE GEÇİŞLERİNDE SORUNLAR VAR
/// OYUNLAR DÜZENLENİP EKLENECEK
/// CAN VE FİNAL İÇİN SAHNELER OLUŞTURULACAK
/// SKOR TAKİBİ ÖNEMLİ
/// HALA ARKA ARKAYA AYNI OYUN GELEBİLİYOR
/// KATEGORİLER EKLENDİĞİNDE BU PROBLEMİ ÇÖZ /\
/// </summary>

public class mainScript : MonoBehaviour
{
    public float animSpeed;
    public Animator transition;
    private int nextSceneIndex,maxSceneIndex = 5,sceneQueueSize = 4,sceneCounter = 0;
    static int levelCount, HP, totalScore,bestScore,lastMinigame = 0;
    static int difficulty = new int();
    public int totalSceneCount = 14;
    Queue<int> sceneQueue;

    private bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("OldScript"));
        bestScore = PlayerPrefs.GetInt("highscore");
        difficulty = 1;
        sceneQueue = new Queue<int>();
        DontDestroyOnLoad(this.gameObject.GetComponent<mainScript>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginTheGame()
    {
        bestScore = PlayerPrefs.GetInt("highscore");
        HP = 3;
        sceneQueue = new Queue<int>();
        totalScore = 0;
        levelCount = 0;
        difficulty = 1;
        SceneRandomizer();
        transition.SetTrigger("End");
        Lagger();
    }   

    public void PlayAgain()
    {
        bestScore = PlayerPrefs.GetInt("highscore");
        HP = 3;
        sceneQueue = new Queue<int>();
        totalScore = 0;
        levelCount = 0;
        difficulty = 1;
        SceneRandomizer();
        Lagger();
    }

    public void Lagger()
    {
        StartCoroutine(Lag());
    }

    IEnumerator Lag()
    {
        yield return new WaitForSeconds(animSpeed);
        Transitioner();
    }

    public void NextScene()
    {
        difficulty = ((levelCount++ / 6) + 1);
        if (difficulty > 3)
        {
            difficulty = 3;
        }
        nextSceneIndex = sceneQueue.Dequeue();
        SceneManager.LoadScene(nextSceneIndex);
        if (sceneQueue.Count == 0)
        {
            SceneRandomizer();
        }
    }

    void EndScene()
    {
        SceneManager.LoadScene("End");
    }


    /// <summary>
    /// DİFFİCULTY SON OYUNLARDA HEP YÜKSEK
    /// OYUN SONDA İKEN DİFFİ RANDOMLA
    /// ARKA ARKAYA GELME SORUNU HENÜZ ÇÖZÜLMEMİŞ OLABİLİR
    /// 
    /// </summary>
    void SceneRandomizer()
    {
        sceneCounter++;
        if (sceneCounter == 1)
        {
            maxSceneIndex = 5;
        }
        else if (sceneCounter < 4)
        {
            maxSceneIndex = 10;
        }
        else
        {
            maxSceneIndex = totalSceneCount;
        }

        int temp = Random.Range(1, maxSceneIndex + 1);
        for (int i = 0; i < sceneQueueSize; i++)
        {
            if (i == sceneQueueSize - 1)
            {
                while (temp == lastMinigame)
                {
                    temp = Random.Range(1, maxSceneIndex + 1);
                }
                lastMinigame = temp;
                sceneQueue.Enqueue(temp);
            }
            else
            {
                if (i == 0)
                {
                    while (temp == lastMinigame || sceneQueue.Contains(temp))
                    {
                        temp = Random.Range(1, maxSceneIndex + 1);
                    }
                    sceneQueue.Enqueue(temp);
                }
                else
                {
                    while (sceneQueue.Contains(temp))
                    {
                        temp = Random.Range(1, maxSceneIndex + 1);
                    }
                    sceneQueue.Enqueue(temp);
                }
            }
            temp = Random.Range(1, maxSceneIndex + 1);
        }

        
    }

    public void EndOfMinigame(float scoreRate, bool won)
    {        
        //////
        ///Bu noktada skorların kategorik kaydını gelen skor ile tutabilirsin
        ///kategorik skor arrayı 0 1 2 3 4 kkategoriler
        ///currentscene indexini 5e böl kategorisine göre arraya at 
        ///finalde ona göre göster
        ///
        ///-----------------------------------------------------
        if (won)
        {
            Debug.Log(difficulty * (int)(100 * Mathf.Sin(Mathf.Deg2Rad * 90 * scoreRate)));
            totalScore += difficulty * (int)(100 * Mathf.Sin(Mathf.Deg2Rad * 90 * scoreRate));
            Debug.Log("Won the minigame!");
            Transitioner();
        }
        else if(!won && HP == 1)
        {
            if (totalScore > bestScore)
            {
                bestScore = totalScore;
                PlayerPrefs.SetInt("highscore", bestScore);
            }
            Debug.Log("Lost the game! - Score = " + totalScore);
            HP = 0;
            EndScene();
        }
        else
        {
            Debug.Log("Lose the minigame!");
            HP--;
            Transitioner();
        }
    }

    void Transitioner()
    {
        int temp = sceneQueue.Peek();
        nextSceneIndex = temp;
        SceneManager.LoadScene("Transition");
    }

    public string getScore()
    {
        return totalScore.ToString();
    }

    public string getBestScore()
    {
        return bestScore.ToString();
    }
    public bool isGameOver() {
        return gameOver;
    }
    public int Difficulty()
    {
        return difficulty;
    }
    public int CurrentSceneIndex()
    {
        return nextSceneIndex;
    }
}

