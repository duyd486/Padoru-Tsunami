using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void Update()
    {
        transform.position -= new Vector3(RoadManager.Instance.GetMoveSpeed() * Time.deltaTime, 0, 0);
        if (transform.position.x <= -80)
        {
            Die();
            ItemsManager.Instance.SpawnItems();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
