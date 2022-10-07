using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    UIManager UIManagerScript;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] collectionSounds;

    private void Awake()
    {
        UIManagerScript = GameObject.Find("User Interface Manager").GetComponent<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Singleton.gameOver && other.CompareTag("Item"))
        {
            Item collectedItem = other.gameObject.GetComponent<Item>();
            if (!collectedItem.collected)
            {
                GameManager.Singleton.score += collectedItem.itemScore;
                collectedItem.collected = true;
                UIManagerScript.UpdateScore();
                collectedItem.SetActiveItem(false, collectedItem.destroyTime);
                collectedItem.PlayCollectionEffect();
                PlayAudioEffect();
            }
        }
    }

    public void PlayAudioEffect()
    {
        int index = Random.Range(0, collectionSounds.Length);
        audioSource.PlayOneShot(collectionSounds[index]);
    }
}
