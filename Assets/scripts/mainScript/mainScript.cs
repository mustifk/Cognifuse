using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SAHNE GEÇİŞLERİNDE SORUNLAR VAR
/// SKOR VE ZAMAN EKLENECEK
/// OYUNLAR DÜZENLENİP EKLENECEK
/// CAN VE FİNAL İÇİN SAHNELER OLUŞTURULACAK
/// SKOR TAKİBİ ÖNEMLİ
/// 
/// </summary>



public class mainScript : MonoBehaviour
{
    public float animSpeed;
    public Animator transition;
    private int nextSceneIndex;
    static int levelCount, HP, totalScore,bestScore,lastMinigame;
    static int difficulty = new int();
    public int sceneCount = 4;
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
        difficulty = ((levelCount++ / 4) + 1);
        if (difficulty > 3)
        {
            difficulty = 3;
        }
        if (sceneQueue.Count == 1)
        {
            lastMinigame = sceneQueue.Pop();
            sceneQueue.Push(lastMinigame);
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
        int temp = Random.Range(1, sceneCount + 1);
        for (int i = 0; i < sceneCount; i++)
        {
            if (i == 0)
            {
                while (sceneQueue.Contains(lastMinigame))
                {
                    temp = Random.Range(1, sceneCount + 1);
                    lastMinigame = temp;
                }
                sceneQueue.Push(temp);
            }
            else
            {
                while (sceneQueue.Contains(temp))
                {
                    temp = Random.Range(1, sceneCount + 1);
                }
                sceneQueue.Push(temp);
            }
            temp = Random.Range(1, sceneCount + 1);
        }
    }


    public void EndOfMinigame(int score,bool won)
    {        
        if (won)
        {
            totalScore += score * difficulty;
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

