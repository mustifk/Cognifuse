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
    public AudioSource mainMenu,electricitySound, transitionMusic,endGameMusic;
    private int nextSceneIndex,maxSceneIndex = 5,sceneQueueSize = 5,sceneCounter = 0,currentScene;
    static int levelCount, HP, totalScore,bestScore,lastMinigame = 0;
    static int difficulty = new int();
    public int totalSceneCount = 14;
    Queue<int> sceneQueue;
    private int[] cScores = new int[5];
    private bool gameOver = false;
    static bool isListening = true;

    static int counter = 0;
    // Start is called before the first frame update
    public GameObject Detail;

    public void ChangeCanvas()
    {
        // Main tekrar çalışıyor aktif oldugunda düzelt
        // detailde arkaplan sorunu arkaplanı karart
        Detail.SetActive(counter % 2 == 0);
        counter++;
    }
    // Start is called before the first frame update
    void Start()
    {
        endGameMusic.Stop();
        electricitySound.PlayDelayed(0.7f);
        mainMenu.PlayDelayed(3f);
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

    public void Listen(bool tf)
    {
        isListening = tf;   
    }

    public bool Listening()
    {
        return isListening;
    }

    public void BeginTheGame()
    {
        electricitySound.Stop();
        endGameMusic.Stop();
        cScores[0] = 0;
        cScores[1] = 0;
        cScores[2] = 0;
        cScores[3] = 0;
        cScores[4] = 0;
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
        endGameMusic.Stop();
        cScores[0] = 0;
        cScores[1] = 0;
        cScores[2] = 0;
        cScores[3] = 0;
        cScores[4] = 0;
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
        transitionMusic.Stop();

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
        endGameMusic.Play();
    }

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
                while (temp == lastMinigame || sceneQueue.Contains(temp))
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
        ///
        if (won)
        {
            Debug.Log(difficulty * (int)(100 * Mathf.Sin(Mathf.Deg2Rad * 90 * scoreRate)));
            totalScore += difficulty * (int)(100 * Mathf.Sin(Mathf.Deg2Rad * 90 * scoreRate));
            cScores[(currentScene - 1) % 5] += difficulty * (int)(100 * Mathf.Sin(Mathf.Deg2Rad * 90 * scoreRate));
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
            for (int i = 0; i < 5; i++)
            {
                PlayerPrefs.SetInt("CCS" + i, cScores[i]);
                if (cScores[i] > PlayerPrefs.GetInt("CBS" + i))
                {
                    PlayerPrefs.SetInt("CBS" + i, cScores[i]);
                }
            }
            Debug.Log("Lost the game! - Score = " + totalScore);
            Debug.Log("Category scores Percpt  = " + cScores[0] + " Attnt = " + cScores[1]);
            Debug.Log("Motrskl "+ cScores[2] + " Reasng = " + cScores[3] + " Memory = " + cScores[4]);
            Debug.Log("Category best scores Percpt  = " + PlayerPrefs.GetInt("CBS" + 0) + " Attnt = " + PlayerPrefs.GetInt("CBS" + 1));
            Debug.Log("Motrskl "+ PlayerPrefs.GetInt("CBS" + 2) + " Reasng = " + PlayerPrefs.GetInt("CBS" + 3) + " Memory = " + PlayerPrefs.GetInt("CBS" + 4));
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
        mainMenu.Stop();
        currentScene = sceneQueue.Peek();
        nextSceneIndex = currentScene;
        SceneManager.LoadScene("Transition");
        transitionMusic.Play();

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

    public int GetHP()
    {
        return HP;
    }

    public int CurrentSceneIndex()
    {
        return nextSceneIndex;
    }
    
    public int[] CategoricalBestScores()
    {
        int[] CBS = new int[5];
        for (int i = 0; i < 5; i++)
        {
            CBS[i] = PlayerPrefs.GetInt("CBS" + i);
        }
        return CBS;
    }
    public int[] CategoricalCurrentScores()
    {
        int[] CCS = new int[5];
        for (int i = 0; i < 5; i++)
        {
            CCS[i] = PlayerPrefs.GetInt("CCS" + i);
        }
        return CCS;
    }

    public void startTransitionMusic()
    {
        transitionMusic.Play();
    }
    public void stopTransitionMusic()
    {
        transitionMusic.Stop();
    }
}

