using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class mainScript : MonoBehaviour
{
    public TextMeshProUGUI playButton,trainingButton;
    public float animSpeed;
    public Animator transition;
    public AudioSource mainMenu,electricitySound, transitionMusic,endGameMusic;
    private int nextSceneIndex,currentScene;
    static int levelCount, HP, totalScore, bestScore, lastMinigame = 0, trainingMode = 0, trainingScene = 1;
    static int difficulty = new int();
    public int totalSceneCount = 14;
    Queue<int> sceneQueue;
    private int[] cScores = new int[5];
    int[] randomizerHelper = new int[5];
    private bool gameOver = false;
    static bool isListening = true;

    static int counter = 0,lang = 1;
    public GameObject Detail,Training,Canvas;
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
        switch (lang)
        {
            case 1:
                playButton.text = "Tap To Begin";
                trainingButton.text = "Training";
                break;
            default:
                playButton.text = "Oynamak Icın Basınız";
                trainingButton.text = "Antrenman";
                break;
        }
    }
    public void setTrainingMode(int x)
    {
        trainingMode = x;
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

    public int Language()
    {
        return lang;
    }

    public void ChangeLanguage(int x)
    {
        lang = x;
        GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().tag = "OldScript";
        SceneManager.LoadScene("Main");
    }

    public void ChangeCanvas()
    {
        Detail.SetActive(counter % 2 == 0);
        counter++;
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
        int tourCount = 1;
        int temp = Random.Range(1, 16);
        while (temp == lastMinigame)
        {
            temp = Random.Range(1, 16);
        }
        for (int i = 0; i < 5; i++)
        {
            randomizerHelper[i] = 0;
        }
        for (int c = 0; c < 3; c++)
        {
            for (int i = 0; i < 5; i++)
            {
                while (randomizerHelper[temp % 5] == tourCount || sceneQueue.Contains(temp))
                {
                    temp = Random.Range(1, 16);
                }
                if (c == 2 && i == 4)
                {
                    lastMinigame = temp;
                }
                randomizerHelper[temp % 5] = tourCount;
                sceneQueue.Enqueue(temp);
            }
            tourCount++;
        }
    }

    public void EndOfMinigame(float scoreRate, bool won)
    {
        if (trainingMode == 1)
        {
            endTrainingScene();
        }
        else
        {
            if (won)
            {
                Debug.Log(difficulty * (int)(100 * Mathf.Sin(Mathf.Deg2Rad * 90 * scoreRate)));
                totalScore += difficulty * (int)(100 * Mathf.Sin(Mathf.Deg2Rad * 90 * scoreRate));
                cScores[(currentScene - 1) % 5] += difficulty * (int)(100 * Mathf.Sin(Mathf.Deg2Rad * 90 * scoreRate));
                Debug.Log("Won the minigame!");
                Transitioner();
            }
            else if (!won && HP == 1)
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
                Debug.Log("Motrskl " + cScores[2] + " Reasng = " + cScores[3] + " Memory = " + cScores[4]);
                Debug.Log("Category best scores Percpt  = " + PlayerPrefs.GetInt("CBS" + 0) + " Attnt = " + PlayerPrefs.GetInt("CBS" + 1));
                Debug.Log("Motrskl " + PlayerPrefs.GetInt("CBS" + 2) + " Reasng = " + PlayerPrefs.GetInt("CBS" + 3) + " Memory = " + PlayerPrefs.GetInt("CBS" + 4));
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
        if (trainingMode == 1)
        {
            return trainingScene;
        }
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

    public void startTraining(int sceneNumber,int diff)
    {
        difficulty = diff;
        trainingScene = sceneNumber;
        SceneManager.LoadScene("Transition", LoadSceneMode.Additive);
        Canvas.GetComponent<CanvasGroup>().interactable = false;
        Canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void startTrainingScene()
    {
        SceneManager.UnloadSceneAsync("Transition");
        SceneManager.LoadScene(trainingScene, LoadSceneMode.Additive);
    }

    public void interruptTrainingScene()
    {
        SceneManager.UnloadSceneAsync("Transition");
    }

    public void endTrainingScene() 
    {
        SceneManager.UnloadSceneAsync(trainingScene);
        Canvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Canvas.GetComponent<CanvasGroup>().interactable = true;
        setTrainingScene();
        Destroy(GameObject.FindGameObjectsWithTag("Finish")[0].gameObject);
    }

    public void startTransitionMusic()
    {
        transitionMusic.Play();
    }
    public void stopTransitionMusic()
    {
        transitionMusic.Stop();
    }
    public void setTrainingScene()
    {
        Training.SetActive(true);
        trainingMode = 1;
    }

    public bool isTraining()
    {
        return trainingMode == 1;
    }
}

