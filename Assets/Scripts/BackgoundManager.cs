using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgoundManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemList = new List<GameObject>();
    [SerializeField] private List<GameObject> itemPool;
    [SerializeField] private float accelerationTimer = 0f;
    [SerializeField] private float accelerationTimerMax = 6f;

    private void Start()
    {
        itemPool = new List<GameObject>();
        SpawnDefaultPool();
    }


    private void Update()
    {
        accelerationTimer -= Time.deltaTime;
        if (accelerationTimer <= 0f)
        {
            accelerationTimer = accelerationTimerMax;
            SpawnItem();
        }
    }

    private void SpawnDefaultPool()
    {
        foreach (GameObject itemInList in itemList)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject item;
                item = Instantiate(itemInList, transform);
                item.SetActive(false);
                itemPool.Add(item);
            }

        }
    }

    public GameObject GetPooledObject()
    {
        int count = 0;
        for (int i = Random.Range(0, itemPool.Count - 1); i < itemPool.Count; i = Random.Range(0, itemPool.Count))
        {
            if (!itemPool[i].activeInHierarchy)
            {
                return itemPool[i];
            }
            count++;
            if (count == 10) break;
        }
        GameObject item;
        item = Instantiate(itemList[Random.Range(0, itemList.Count - 1)], transform);
        item.SetActive(false);
        itemPool.Add(item);
        return item;
    }

    public void SpawnItem()
    {
        GameObject item = GetPooledObject();
        item.transform.position = new Vector3(240, 0, 0);
        item.SetActive(true);
        item.GetComponent<Item>().Respawn();
    }

    public void ResetItems()
    {
        foreach (GameObject item in itemPool)
        {
            item.SetActive(false);
        }
        accelerationTimer = 0f;
    }
}
