using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct SubPool
{
    public GameObject objectToPool;
    public int amountToPool;
    public List<GameObject> subPoolObjects;
}

public sealed class ObjectPool : MonoBehaviour
{
    static ObjectPool sharedInstance;
    public SubPool[] subPools;
    //combined pool of game objects from all pooledObjects lists
    public List<GameObject> mainPoolObjects;
    public int poolSize;
    public List<GameObject> activePoolObjects;
    public int activePoolSize;

    public static ObjectPool SharedInstance
    {
        get
        {
            if (sharedInstance == null)
            {
                sharedInstance = new ObjectPool();
            }
            return sharedInstance;
        }
    }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else if (sharedInstance != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mainPoolObjects = new List<GameObject>();
        for (int i = 0; i < subPools.Length; i++)
        {
            subPools[i].subPoolObjects = new List<GameObject>();
            GameObject tmp;
            
            for (int j = 0; j < subPools[i].amountToPool; j++)
            {
                tmp = Instantiate(subPools[i].objectToPool);
                tmp.SetActive(false);
                subPools[i].subPoolObjects.Add(tmp);
                mainPoolObjects.Add(tmp);
                poolSize++;
            }
        }
    }

    public void AddToActivePool(SubPool subPool)
    {
        for (int i = 0; i < subPool.amountToPool; i++)
        {
            activePoolObjects.Add(subPool.subPoolObjects[i]);
            activePoolSize++;
        }
    }

    public void RemoveFromActivePool(SubPool subPool)
    {

        for (int i = 0; i < subPool.amountToPool; i++)
        {
            Item item = subPool.objectToPool.GetComponent<Item>();
            int itemType = item.itemType;
            activePoolObjects.Remove(subPool.subPoolObjects[i]);
            activePoolSize--;
        }
    }

    public GameObject GetPooledObject()
    {
        List<GameObject> activeObjects = new List<GameObject>();
        GameObject pooledObject;
        //if activeObjects is not empty
        if (activeObjects != null)
        {
            for (int i = 0; i < activeObjects.Count; i++)
            {
                activeObjects.RemoveAt(0);
            }
        }
        
        for (int i = 0; i < activePoolSize; i++)
        {
            do
            {
                pooledObject = activePoolObjects[Random.Range(0, activePoolSize)];
            } while (activeObjects.Contains(pooledObject));

            if (!pooledObject.activeInHierarchy)
            {
                return pooledObject;
            }
            else if (pooledObject.activeInHierarchy)
            {
                activeObjects.Add(pooledObject);
            }
        }
        return null;
    }

    public void DeactivateActiveObjects()
    {
        for (int i = 0; i < activePoolSize; i++)
        {
            if (activePoolObjects[i].activeInHierarchy)
            {
                Item item = activePoolObjects[i].GetComponent<Item>();
                item.SetActiveItem(false, 5f);
            }
        }
    }
}
