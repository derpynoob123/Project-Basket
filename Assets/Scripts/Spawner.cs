using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Spawner Settings
public struct SpawnerSetting
{
    public static float minimumSpawnTime;
    public static float maximumSpawnTime;
    public static bool spawnerActive;
}

public class Spawner : MonoBehaviour
{
    float xRange;
    float spawnTime;

    private void Awake()
    {
        xRange = 18f;
        SpawnerSetting.spawnerActive = false;
    }

    void StartSpawner()
    {
        if (!GameManager.Singleton.isTutorial)
        {
            SpawnerSetting.spawnerActive = true;
        }
    }

    void SpawnerSystem()
    {
        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0)
        {
            SpawnItem();
            spawnTime = Random.Range(SpawnerSetting.minimumSpawnTime, SpawnerSetting.maximumSpawnTime); ;
        }
    }

    void SpawnItem()
    {
        if (ObjectPool.SharedInstance.activePoolObjects == null)
        {
            return;
        }

        
        GameObject item = ObjectPool.SharedInstance.GetPooledObject();

        if (item == null)
        {
            Debug.Log("All pooled objects are active!");
            return;
        }
        else if (item != null)
        {
            float xPosition = transform.position.x + Random.Range(-xRange, xRange);
            Vector3 spawnPosition = new Vector3(xPosition, transform.position.y);
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.transform.position = spawnPosition;
            item.transform.rotation = Quaternion.Euler(RandomiseRotation());
            item.SetActive(true);
        }
    }

    Vector3 RandomiseRotation()
    {
        return new Vector3(0, 0, Random.Range(0, 360));
    }

    private void Start()
    {
        Invoke("StartSpawner", 4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Singleton.gameOver && !GameManager.Singleton.paused && SpawnerSetting.spawnerActive)
        {
            SpawnerSystem();
        }
    }
}
