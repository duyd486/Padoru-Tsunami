using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RoadManager : MonoBehaviour
{
    public static RoadManager Instance { get; private set; }

    [SerializeField] private List<GameObject> pooledObjects;
    [SerializeField] private GameObject roadPref;
    [SerializeField] private int amountToPool = 3;
    [SerializeField] private float moveSpeed = 5f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(roadPref, transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        GameObject gameObject;
        gameObject = Instantiate(roadPref, transform);
        gameObject.SetActive(false);
        pooledObjects.Add(gameObject);
        return gameObject;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
