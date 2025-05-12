using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZumbaiManager : MonoBehaviour
{
    public static ZumbaiManager Instance { get; private set; }

    [SerializeField] private List<GameObject> zumbaiList;
    [SerializeField] private GameObject zumbaiPref;
    [SerializeField] private float jumpDelay = 0.1f;
    [SerializeField] private Transform centerOfBoids;

    enum Jump
    {
        JumpPress,
        JumpHold,
        JumpRelease,
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnJumpAction += GameInput_OnJumpAction;
        GameInput.Instance.OnJumpHold += GameInput_OnJumpHold;
        GameInput.Instance.OnJumpRelease += GameInput_OnJumpRelease;

        GameInput.Instance.OnTestPress += GameInput_OnTestPress;

        zumbaiList = new List<GameObject>();
        AddZumbai();
    }


    private void GameInput_OnTestPress(object sender, System.EventArgs e)
    {
        AddZumbai();
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e)
    {
        StartCoroutine(TriggerJump(Jump.JumpPress));
    }
    private void GameInput_OnJumpHold(object sender, System.EventArgs e)
    {
        StartCoroutine(TriggerJump(Jump.JumpHold));
    }
    private void GameInput_OnJumpRelease(object sender, System.EventArgs e)
    {
        StartCoroutine(TriggerJump(Jump.JumpRelease));
    }

    private IEnumerator TriggerJump(Jump jump)
    {
        SortZumbai();
        foreach (GameObject zumbai in zumbaiList)
        {
            switch (jump)
            {
                default:
                case Jump.JumpPress:
                    zumbai.GetComponent<Zumbai>().JumpPress();
                    break;
                case Jump.JumpHold:
                    zumbai.GetComponent<Zumbai>().JumpHold();
                    break;
                case Jump.JumpRelease:
                    zumbai.GetComponent<Zumbai>().JumpRelease();
                    break;
            }
            yield return new WaitForSeconds(jumpDelay);
        }
    }

    private void SortZumbai()
    {
        zumbaiList = zumbaiList.OrderByDescending(Zumbai => Zumbai.transform.position.x).ToList();
    }

    public void AddZumbai()
    {
        GameObject zumbaiOb = Instantiate(zumbaiPref, transform);
        zumbaiOb.transform.position = centerOfBoids.position + new Vector3(0,5,0);
        zumbaiList.Add(zumbaiOb);
    }
    public void RemoveZumbai()
    {

    }
    public int GetZumbaiCount()
    {
        return zumbaiList.Count;
    }
    public Vector3 GetCenterOfBoids()
    {
        return centerOfBoids.position;
    }
    public List<GameObject> GetListZumbai()
    {
        return zumbaiList;
    }
}
