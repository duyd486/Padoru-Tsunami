using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZumbaiCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI zumbaiCountText;
    private int zumbaiCount = 0;


    private void Start()
    {
        zumbaiCount = 0;
        zumbaiCountText.text = "X " + zumbaiCount;
        ZumbaiManager.Instance.OnZumbaiAdded += ZumbaiManager_OnZumbaiAdded;
        ZumbaiManager.Instance.OnZumbaiRemoved += ZumbaiManager_OnZumbaiRemoved;
    }

    private void ZumbaiManager_OnZumbaiRemoved(object sender, System.EventArgs e)
    {
        zumbaiCount--;
        zumbaiCountText.text = "X " + zumbaiCount;
    }

    private void ZumbaiManager_OnZumbaiAdded(object sender, System.EventArgs e)
    {
        zumbaiCount++;
        zumbaiCountText.text = "X " + zumbaiCount;
    }
}
