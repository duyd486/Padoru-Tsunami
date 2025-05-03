using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;


    void Update()
    {
        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        if(transform.position.x < -40)
        {
            gameObject.SetActive(false);
            GameObject road = RoadManager.Instance.GetPooledObject();
            if(road != null)
            {
                road.transform.position = new Vector3(40,0,0);
                road.SetActive(true);
            }
        }
    }
}
