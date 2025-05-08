using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager Instance {  get; private set; }
    private List<int> itemsXLocation = new List<int>
    {
        80, 120, 160, 200, 240, 280
    };
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
                for (int i = 0; i < numberOfItem; i++)
                {
                    SpawnItems();
                }
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
        for (int i = 0; i < itemsPool.Count; i++)
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
        item.transform.position = new Vector3(itemsXLocation[UnityEngine.Random.Range(0,itemsXLocation.Count)],0,0);
        item.SetActive(true);
    }
}
