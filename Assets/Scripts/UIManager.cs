using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    TextMeshProUGUI timerText;
    public GameObject readyPanelGameObject;
    public GameObject goTextGameObject;
    TextMeshProUGUI readyText;
    public GameObject timerStopTextGameObject;
    public GameObject pausePanelGameObject;
    public Button pauseButton;
    public GameObject exitButton2GameObject;
    TextMeshProUGUI highScoreText;
    public GameObject controlsTextGameObject;
    public GameObject proceedTextGameObject;
    public GameObject objectiveTextGameObject;
    public GameObject exitTutorialGameObject;
    public GameObject restartButton2GameObject;

    private void Awake()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("Countdown Timer Text").GetComponent<TextMeshProUGUI>();
        readyPanelGameObject = GameObject.Find("Ready Panel");
        goTextGameObject = GameObject.Find("Go Text");
        readyText = GameObject.Find("Ready... Text").GetComponent<TextMeshProUGUI>();
        timerStopTextGameObject = GameObject.Find("Timer Stop Text");
        pausePanelGameObject = GameObject.Find("Pause Panel");
        pauseButton = GameObject.Find("Pause Button").GetComponent<Button>();
        exitButton2GameObject = GameObject.Find("Exit Button 2");
        highScoreText = GameObject.Find("High Score Text").GetComponent<TextMeshProUGUI>();
        controlsTextGameObject = GameObject.Find("Controls Text");
        proceedTextGameObject = GameObject.Find("Collect Item Text");
        objectiveTextGameObject = GameObject.Find("Objective Text");
        exitTutorialGameObject = GameObject.Find("Exit Tutorial Button");
        restartButton2GameObject = GameObject.Find("Restart Button 2");
    }

    private void Start()
    {
        SetActiveUI(readyPanelGameObject, true);
        SetActiveUI(goTextGameObject, false);
        SetActiveUI(timerStopTextGameObject, false);
        SetActiveUI(pausePanelGameObject, false);
        SetActiveUI(exitButton2GameObject, false);
        SetActiveUI(restartButton2GameObject, false);

        if (GameManager.Singleton.isTutorial)
        {
            SetActiveUI(controlsTextGameObject, false);
            SetActiveUI(proceedTextGameObject, false);
            SetActiveUI(objectiveTextGameObject, false);
            SetActiveUI(exitTutorialGameObject, false);
        }
        UpdateScore();
        UpdateCountdownTimer(GameManager.Singleton.countdownTime);
        Invoke("ReadyAnimation1", 0.8f);
        Invoke("ReadyAnimation2", 1.6f);
        Invoke("ReadyAnimation3", 2.4f);
        Invoke("ReadyAnimation4", 3.2f);
        pauseButton.interactable = false;
        highScoreText.text = "Best Score: " + DataPersistence.Singleton.bestScore;
    }

    void ReadyAnimation1()
    {
        readyText.text = "Ready.";
    }

    void ReadyAnimation2()
    {
        readyText.text = "Ready..";
    }

    void ReadyAnimation3()
    {
        readyText.text = "Ready...";
    }

    void ReadyAnimation4()
    {
        readyText.text = "Ready....";
    }

    public void SetActiveUI(GameObject UI, bool active)
    {
        UI.SetActive(active);
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + GameManager.Singleton.score;
    }

    public void UpdateCountdownTimer(float time)
    {
        time *= 10;
        time = Mathf.Round(time);
        //if the time is not .0
        if (time % 10 != 0)
        {
            time /= 10;
            timerText.text = time.ToString();
        }
        //if the time is .0
        else
        {
            time /= 10;
            timerText.text = $"{time}.0";
        }
    }

    private void LateUpdate()
    {
        UpdateCountdownTimer(GameManager.Singleton.countdownTime);
    }

    public void ExitTutorial()
    {
        if (DataPersistence.Singleton.playedOnce)
        {
            GameSystem.Singleton.LoadScene(0);
        }
        else
        {
            GameSystem.Singleton.LoadScene(1);
        }
    }
}
