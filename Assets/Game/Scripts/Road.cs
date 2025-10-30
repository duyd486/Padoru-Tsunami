using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    void Update()
    {
        transform.position -= new Vector3(GameManager.Instance.GetGameSpeed(), 0, 0) * Time.deltaTime;
        if(transform.position.x <= -80)
        {
            gameObject.SetActive(false);
            GameObject road = RoadManager.Instance.GetPooledObject();
            road.transform.position = new Vector3(transform.position.x + 160, 0, 0);
            road.SetActive(true);
        }
    }
}
