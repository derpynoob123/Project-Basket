using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemScore;
    public float destroyTime;
    public int itemType;
    public bool collected;
    [SerializeField] GameObject[] collectionEffects;

    public void SetActiveItem(bool active, float delay)
    {
        if (active)
        {
            Invoke("DelayActive", delay);
        }
        else if (!active)
        {
            Invoke("DelayInactive", delay);
        }
    }

    void DelayActive()
    {
        gameObject.SetActive(true);
    }

    void DelayInactive()
    {
        collected = false;
        gameObject.SetActive(false);
    }

    public void PlayCollectionEffect()
    {
        int index = Random.Range(0, collectionEffects.Length);
        Instantiate(collectionEffects[index], transform);
    }
}
