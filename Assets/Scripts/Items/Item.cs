using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position -= new Vector3(GameManager.Instance.GetGameSpeed() * Time.deltaTime, 0, 0);
        if (transform.position.x <= -80)
        {
            gameObject.SetActive(false);
        }
    }
    public void Respawn()
    {
        foreach(Transform chil in transform)
        {
            chil.gameObject.SetActive(true);
        }
    }
}
