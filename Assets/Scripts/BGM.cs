using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField] AudioSource BGMAudio;
    [SerializeField] AudioClip[] BGMClips;
    static BGM singleton;
    public static BGM Singleton
    {
        get
        {
            if (singleton == null)
            {
                singleton = new BGM();
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
    }

    public void PlayBGM()
    {
        int index = Random.Range(0, singleton.BGMClips.Length);
        singleton.BGMAudio.PlayOneShot(singleton.BGMClips[index], 0.8f);
    }

    public void StopBGM()
    {
        singleton.BGMAudio.Stop();
    }
}
