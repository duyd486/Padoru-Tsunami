using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public void Die()
    {
        gameObject.SetActive(false);
    }
}
