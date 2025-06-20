using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    void Update()
    {
        transform.position -= new Vector3(GameManager.Instance.GetGameSpeed(), 0, 0) * Time.deltaTime;
        if (transform.position.x <= -80)
        {
            gameObject.SetActive(false);
            GameObject house = BackgoundManager.Instance.GetPoolHouse();
            house.transform.position = new Vector3(transform.position.x + 200, 2, 15);
            house.GetComponent<House>().Respawn();
            house.SetActive(true);
            Debug.Log(house);
        }
    }
    public void Respawn()
    {
        foreach (Transform son in transform)
        {
            son.gameObject.SetActive(true);
            foreach (Transform chil in son)
            {
                chil.gameObject.SetActive(true);
            }
        }
    }
}
