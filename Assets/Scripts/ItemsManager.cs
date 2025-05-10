using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager Instance {  get; private set; }
    [SerializeField] private List<GameObject> itemsList = new List<GameObject>(); 
    [SerializeField] private List<GameObject> itemsPool;
    [SerializeField] private int numberOfItem = 1;
    [SerializeField] private float accelerationTimer = 0f;
    [SerializeField] private float accelerationTimerMax = 6f;



    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        itemsPool = new List<GameObject>();
        accelerationTimer = accelerationTimerMax;
        SpawnDefault();
    }


    private void Update()
    {
        if (GameManager.Instance.GetIsPlaying())
        {
            accelerationTimer -= Time.deltaTime;
            if(accelerationTimer <= 0f)
            {
                accelerationTimer = accelerationTimerMax;
                SpawnItems();
            }
        }
    }

    private void SpawnDefault()
    {
        foreach(GameObject itemInList in itemsList)
        {
            for(int i = 0; i < 3; i++)
            {
                GameObject item;
                item = Instantiate(itemInList, transform);
                item.SetActive(false);
                itemsPool.Add(item);
            }

        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = UnityEngine.Random.Range(0, itemsPool.Count); i < itemsPool.Count; i = UnityEngine.Random.Range(0, itemsPool.Count))
        {
            if (!itemsPool[i].activeInHierarchy)
            {
                return itemsPool[i];
            }
        }
        GameObject item;
        item = Instantiate(itemsList[UnityEngine.Random.Range(0, itemsList.Count)], transform);
        item.SetActive(false);
        itemsPool.Add(item);
        return item;
    }

    public void SpawnItems()
    {
        GameObject item = GetPooledObject();
        item.transform.position = new Vector3(240,0,0);
        item.SetActive(true);
    }
}
