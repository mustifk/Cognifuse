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
    static int levelCount, HP, totalScore,lastMinigame;
    static int difficulty = new int();
    public int sceneCount = 4;
    Stack<int> sceneQueue;

    private bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
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
        HP = 3;
        totalScore = 0;
        levelCount = 0;
        difficulty = 1;
        SceneRandomizer();
        transition.SetTrigger("End");
        Lagger();
    }

    public void Lagger()
    {
        StartCoroutine(Lag());
    }
    IEnumerator Lag()
    {
        yield return new WaitForSeconds(animSpeed);
        NextScene();
    }

    void NextScene()
    {
        difficulty = ((levelCount++ / 4) + 1);
        if (difficulty > 3)
        {
            difficulty = 3;
        }
        SceneManager.LoadScene(sceneQueue.Pop());
        if (sceneQueue.Count == 0)
        {
            SceneRandomizer();
        }
    }

    void EndScene()
    {

        SceneManager.LoadScene(0);
        //olması gereken
        //SceneManager.LoadScene(sceneCount + 1);
    }

    void SceneRandomizer()
    {
        int temp = Random.Range(1, sceneCount + 1);
        for (int i = 0; i < sceneCount; i++)
        {
            if (i == 0)
            {
                lastMinigame = temp;
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

    public bool isGameOver() {
        return gameOver;
    }


    public void EndOfMinigame(int score,bool won)
    {
        totalScore += score;
        if (won)
        {
            Debug.Log("Won the minigame!");
            NextScene();
        }
        else if(!won && HP == 1)
        {
            Debug.Log("Lost the game! - Score = " + totalScore);
            HP = 0;
            EndScene();
        }
        else
        {
            Debug.Log("Lose the minigame!");
            HP--;
            NextScene();
        }
    }

    public int Difficulty()
    {
        return difficulty;
    }
}

