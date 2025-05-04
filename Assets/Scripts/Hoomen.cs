using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoomen : MonoBehaviour
{

    private void Update()
    {
        transform.position -= new Vector3(RoadManager.Instance.GetMoveSpeed() * Time.deltaTime, 0, 0);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
