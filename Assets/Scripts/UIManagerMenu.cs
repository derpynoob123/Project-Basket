using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManagerMenu : MonoBehaviour
{
    TextMeshProUGUI highScoreText;
    GameObject highScoreTextGameObject;
    GameObject YesNoPanelGameObject;
    GameObject tutorialButtonGameObject;

    private void Awake()
    {
        highScoreText = GameObject.Find("Highest Score Text").GetComponent<TextMeshProUGUI>();
        highScoreTextGameObject = GameObject.Find("Highest Score Text");
        YesNoPanelGameObject = GameObject.Find("YesNo Panel");
        tutorialButtonGameObject = GameObject.Find("Tutorial Button");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (DataPersistence.Singleton.playedOnce)
        {
            highScoreText.text = "Highest Score: " + DataPersistence.Singleton.bestScore;
            SetActiveUI(tutorialButtonGameObject, true);
        }
        else
        {
            SetActiveUI(highScoreTextGameObject, false);
            SetActiveUI(tutorialButtonGameObject, false);
        }
        SetActiveUI(YesNoPanelGameObject, false);
    }

    public void SetActiveUI(GameObject UI, bool active)
    {
        if (UI == null)
        {
            Debug.LogWarning("Empty Game Object!");
            return;
        }
        UI.SetActive(active);
    }

    public void ResetProgress()
    {
        SetActiveUI(YesNoPanelGameObject, true);
    }

    public void HideYesNoPanel()
    {
        SetActiveUI(YesNoPanelGameObject, false);
    }

    public void StartButton()
    {
        if (DataPersistence.Singleton.playedOnce)
        {
            GameSystem.Singleton.LoadScene(1);
        }
        else
        {
            GameSystem.Singleton.LoadScene(2);
        }
    }
}
