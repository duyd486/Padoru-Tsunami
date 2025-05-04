using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;


    void Update()
    {
        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        if(transform.position.x <= -80)
        {
            gameObject.SetActive(false);
            GameObject road = RoadManager.Instance.GetPooledObject();
            road.transform.position = new Vector3(80, 0, 0);
            road.SetActive(true);
        }
    }
}
