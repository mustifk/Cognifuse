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
    private int nextSceneIndex,maxSceneIndex = 5,minSceneIndex = 0,sceneQueueSize = 5,sceneIndexCounter = 0;
    static int levelCount, HP, totalScore,bestScore,lastMinigame = 0;
    static int difficulty = new int();
    public int sceneCount = 14;
    Stack<int> sceneQueue;

    private bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("OldScript"));
        bestScore = PlayerPrefs.GetInt("highscore");
        difficulty = 1;
        sceneQueue = new Stack<int>();
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
        sceneQueue = new Stack<int>();
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
        sceneQueue = new Stack<int>();
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
        difficulty = ((levelCount++ / 7) + 1);
        if (difficulty > 3)
        {
            difficulty = 3;
        }
        nextSceneIndex = sceneQueue.Pop();
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

    void SceneRandomizer()
    {
        int temp = Random.Range(minSceneIndex + 1, sceneCount + 1);
        for (int i = 0; i < sceneQueueSize; i++)
        {
            if (i == 0)
            {
                while (temp == lastMinigame)
                {
                    temp = Random.Range(minSceneIndex + 1, sceneCount + 1);
                }
                lastMinigame = temp;
                sceneQueue.Push(temp);
            }
            else
            {
                while (sceneQueue.Contains(temp))
                {
                    temp = Random.Range(minSceneIndex + 1, sceneCount + 1);
                }
                sceneQueue.Push(temp);
            }
            temp = Random.Range(minSceneIndex + 1, sceneCount + 1);
        }

        switch (sceneIndexCounter)
        {
            case 0:
                maxSceneIndex = 5;
                minSceneIndex = 0;
                break;
            case 1:
                maxSceneIndex = 10;
                minSceneIndex = 0;
                break;
            case 2:
                maxSceneIndex = 10;
                minSceneIndex = 5;
                break;
            case 3:
                maxSceneIndex = 15;
                minSceneIndex = 5;
                break;
            default:
                maxSceneIndex = 15;
                minSceneIndex = 0;
                break;
        }
        sceneIndexCounter++;
    }

    public void EndOfMinigame(float scoreRate, bool won)
    {        
        //////
        ///Bu noktada skorların kategorik kaydını gelen skor ile tutabilirsin
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
        int temp = sceneQueue.Pop();
        nextSceneIndex = temp;
        sceneQueue.Push(temp);
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

