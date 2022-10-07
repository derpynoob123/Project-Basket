using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public bool mouseOverBasket;
    public Vector3 mousePos;
    public int score;
    public float countdownTime;
    public bool paused;
    UIManager UIManagerScript;
    public float startLevelDelay;
    //Logic to end ongoing gameplay
    public bool gameOver;
    public float timeScale;
    public bool isTutorial;
    BGM BGMScript;

    static GameManager singleton;
    public static GameManager Singleton
    {
        get
        {
            if (singleton == null)
            {
                singleton = new GameManager();
            }
            return singleton;
        }
    }

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != null)
        {
            Destroy(gameObject);
        }

        singleton.UIManagerScript = GameObject.Find("User Interface Manager").GetComponent<UIManager>();
        singleton.BGMScript = GameObject.Find("BGM").GetComponent<BGM>();
        singleton.score = 0;
        singleton.countdownTime = 30f;
        singleton.startLevelDelay = 4f;
        singleton.gameOver = false;
        singleton.timeScale = 1;
        singleton.paused = true;
        if (GameSystem.Singleton.IsActiveScene(2))
        {
            singleton.isTutorial = true;
        }
        else
        {
            singleton.isTutorial = false;
        }
        Time.timeScale = singleton.timeScale;
    }

    private void Start()
    {
        if (!singleton.isTutorial)
        {
            BGMScript.PlayBGM();
        }
        Invoke("StartLevel", singleton.startLevelDelay);
    }

    void StartLevel()
    {
        singleton.paused = false;
        singleton.UIManagerScript.SetActiveUI(UIManagerScript.readyPanelGameObject, false);
        if (singleton.isTutorial)
        {
            singleton.UIManagerScript.SetActiveUI(UIManagerScript.controlsTextGameObject, true);
        }
        else
        {
            singleton.UIManagerScript.SetActiveUI(UIManagerScript.goTextGameObject, true);
        }
        
        singleton.UIManagerScript.pauseButton.interactable = true;
        Invoke("DelayHideGoText", 2f);
    }

    void DelayHideGoText()
    {
        singleton.UIManagerScript.SetActiveUI(UIManagerScript.goTextGameObject, false);
    }

    private void FixedUpdate()
    {
        if (!singleton.paused && !singleton.gameOver)
        {
            UpdateMousePosition();
        }
    }

    void UpdateMousePosition()
    {
        singleton.mouseOverBasket = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Basket"))
            {
                singleton.mouseOverBasket = true;
                singleton.mousePos = hit.point;
            }
        }
    }

    private void Update()
    {
        if (!singleton.paused && !singleton.isTutorial)
        {
            if (singleton.countdownTime > 0)
            {
                singleton.countdownTime -= Time.deltaTime;
            }
            if (!singleton.gameOver && singleton.countdownTime <= 0)
            {
                singleton.countdownTime = 0;
                GameOver();
            }
        }
    }

    public void Pause()
    {
        if (!singleton.paused)
        {
            Time.timeScale = 0;
            singleton.paused = true;
            singleton.UIManagerScript.SetActiveUI(singleton.UIManagerScript.pausePanelGameObject, true);
        }
        else if (paused)
        {
            Time.timeScale = singleton.timeScale;
            singleton.paused = false;
            singleton.UIManagerScript.SetActiveUI(singleton.UIManagerScript.pausePanelGameObject, false);
        }
    }

    void GameOver()
    {
        if (!DataPersistence.Singleton.playedOnce)
        {
            DataPersistence.Singleton.playedOnce = true;
        }

        ObjectPool.SharedInstance.DeactivateActiveObjects();
        singleton.gameOver = true;
        TimerStopAnimation1();
        Invoke("GameOverUI", 2f);

        if (singleton.score > DataPersistence.Singleton.bestScore)
        {
            DataPersistence.Singleton.bestScore = singleton.score;
            DataPersistence.Singleton.Save();
        }
    }

    void TimerStopAnimation1()
    {
        UIManagerScript.SetActiveUI(UIManagerScript.timerStopTextGameObject, true);
        Invoke("TimerStopAnimation2", 0.5f);
    }

    void TimerStopAnimation2()
    {
        UIManagerScript.SetActiveUI(UIManagerScript.timerStopTextGameObject, false);
        Invoke("TimerStopAnimation1", 0.5f);
    }

    void GameOverUI()
    {
        singleton.UIManagerScript.SetActiveUI(singleton.UIManagerScript.exitButton2GameObject, true);
        singleton.UIManagerScript.SetActiveUI(singleton.UIManagerScript.restartButton2GameObject, true);
    }
}
