using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class GameSystem : MonoBehaviour
{
    static GameSystem singleton;
    public Dictionary<int, string> scenes;

    public static GameSystem Singleton
    {
        get
        {
            if (singleton == null)
            {
                singleton = new GameSystem();
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

        DontDestroyOnLoad(gameObject);

        singleton.scenes = new Dictionary<int, string>();
        singleton.scenes.Add(0, "Menu");
        singleton.scenes.Add(1, "Main");
        singleton.scenes.Add(2, "Tutorial");
    }

    public void LoadScene(int key)
    {
        SceneManager.LoadScene(singleton.scenes[key]);
    }

    public bool IsActiveScene(int key)
    {
        if (SceneManager.GetActiveScene().buildIndex == key)
        {
            return true;
        }
        return false;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
