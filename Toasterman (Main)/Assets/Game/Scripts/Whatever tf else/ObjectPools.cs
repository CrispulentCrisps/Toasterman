﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{

    [System.Serializable]

    public class Pool
    {

        public string tag;
        public GameObject prefab;
        public int size;

    }

    #region Singleton

    public static ObjectPools Instance;

    private void Awake()
    {

        Instance = this;

    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {

                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);

            }

            poolDictionary.Add(pool.tag, objectPool);

        }

    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Object with tag:" + tag + " is not found");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {

            pooledObj.OnObjectSpawn();

        }
        else
        {
            Debug.LogWarning("Error, Object not found");
        }


        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;

    }

}
