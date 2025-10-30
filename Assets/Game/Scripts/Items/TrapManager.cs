using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class TrapManager : MonoBehaviour
{
    public static TrapManager Instance {  get; private set; }
    [SerializeField] private List<GameObject> trapList = new List<GameObject>(); 
    [SerializeField] private List<GameObject> trapPool;
    [SerializeField] private float accelerationTimer = 0f;
    [SerializeField] private float accelerationTimerMax = 6f;



    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        trapPool = new List<GameObject>();
        SpawnDefaultPool();
    }


    private void Update()
    {
        if (GameManager.Instance.GetIsPlaying())
        {
            accelerationTimer -= Time.deltaTime;
            if(accelerationTimer <= 0f)
            {
                accelerationTimer = accelerationTimerMax;
                SpawnTrap();
            }
        }
    }

    private void SpawnDefaultPool()
    {
        foreach(GameObject itemInList in trapList)
        {
            for(int i = 0; i < 3; i++)
            {
                GameObject item;
                item = Instantiate(itemInList, transform);
                item.SetActive(false);
                trapPool.Add(item);
            }

        }
    }

    public GameObject GetPooledObject()
    {
        int count = 0;
        for (int i = UnityEngine.Random.Range(0, trapPool.Count); i < trapPool.Count; i = UnityEngine.Random.Range(0, trapPool.Count))
        {
            if (!trapPool[i].activeInHierarchy)
            {
                return trapPool[i];
            }
            count++;
            if (count == 10) break;
        }
        GameObject item;
        item = Instantiate(trapList[UnityEngine.Random.Range(0, trapList.Count - 1)], transform);
        item.SetActive(false);
        trapPool.Add(item);
        return item;
    }

    public void SpawnTrap()
    {
        GameObject item = GetPooledObject();
        item.transform.position = new Vector3(240,0,0);
        item.SetActive(true);
        item.GetComponent<Item>().Respawn();
    }

    public void ResetItems()
    {
        foreach(GameObject item in trapPool)
        {
            item.SetActive(false);
        }
        accelerationTimer = 0f;
    }
}
