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
        120, 150, 180, 210, 240
    };
    [SerializeField] private List<GameObject> itemsList = new List<GameObject>(); 
    [SerializeField] private List<GameObject> itemsPool;
    [SerializeField] private int numberOfItem = 1;
    [SerializeField] private float accelerationTimer = 0f;
    [SerializeField] private float accelerationTimerMax = 6f;


    private void Start()
    {
        Instance = this;
        itemsPool = new List<GameObject>();
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
        item.transform.position = new Vector3(itemsXLocation[UnityEngine.Random.Range(0,itemsXLocation.Count)],0,0);
        item.SetActive(true);
    }
}
