using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager Instance {  get; private set; }

    [SerializeField] private List<GameObject> itemsList = new List<GameObject>(); 
    [SerializeField] private List<GameObject> itemsPool;
    [SerializeField] private int numberOfItem = 5;

    private void Start()
    {
        Instance = this;
        itemsPool = new List<GameObject>();

        for (int i = 0; i < numberOfItem; i++)
        {
            SpawnItems();
        }
    }


    public GameObject GetPooledObject()
    {
        for (int i = 0; i < itemsPool.Count; i++)
        {
            if (!itemsPool[i].activeInHierarchy)
            {
                return itemsPool[i];
            }
        }
        GameObject gameObject;
        gameObject = Instantiate(itemsList[UnityEngine.Random.Range(0, itemsList.Count)], transform);
        gameObject.SetActive(false);
        itemsPool.Add(gameObject);
        return gameObject;
    }

    public void SpawnItems()
    {
        GameObject item = GetPooledObject();
        item.transform.position = new Vector3(UnityEngine.Random.Range(40, 120),0,0);
        item.SetActive(true);
    }
}
