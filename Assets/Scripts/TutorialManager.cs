using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    bool hasMovedBasket;
    Rigidbody basketRb;
    UIManager UIManagerScript;
    bool hasCollectedItem;

    void Awake()
    {
        UIManagerScript = GameObject.Find("User Interface Manager").GetComponent<UIManager>();
        basketRb = GameObject.Find("Basket").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Singleton.paused && !hasMovedBasket && basketRb.velocity.magnitude > 0)
        {
            Debug.Log("Basket moved");
            hasMovedBasket = true;
        }

        if (hasMovedBasket && !hasCollectedItem)
        {
            Invoke("DelayHideControlsText", 1.5f);
            Invoke("DelayActivateProceedText", 1.5f);
        }

        if (GameManager.Singleton.score > 0)
        {
            hasCollectedItem = true;
        }
        if (hasMovedBasket && hasCollectedItem)
        {
            Invoke("DelayHideProceedText", 1.5f);
            Invoke("DelayActivateObjectiveText", 1.5f);
        }
    }

    void DelayHideControlsText()
    {
        UIManagerScript.SetActiveUI(UIManagerScript.controlsTextGameObject, false);
    }

    void DelayActivateProceedText()
    {
        UIManagerScript.SetActiveUI(UIManagerScript.proceedTextGameObject, true);
        SpawnerSetting.spawnerActive = true;
    }

    void DelayHideProceedText()
    {
        UIManagerScript.SetActiveUI(UIManagerScript.proceedTextGameObject, false);
    }

    void DelayActivateObjectiveText()
    {
        UIManagerScript.SetActiveUI(UIManagerScript.exitTutorialGameObject, true);
        UIManagerScript.SetActiveUI(UIManagerScript.objectiveTextGameObject, true);
    }
}
