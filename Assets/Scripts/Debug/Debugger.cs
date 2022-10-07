using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Singleton.timeScale = 3;
            Time.timeScale = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.Singleton.timeScale = 10;
            Time.timeScale = 10;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            GameManager.Singleton.timeScale = 1;
            Time.timeScale = 1;
        }
    }
}
