using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class ZumbaiManager : MonoBehaviour
{
    public static ZumbaiManager Instance { get; private set; }

    [SerializeField] private List<GameObject> zumbaiPool;
    [SerializeField] private List<GameObject> zumbaiActiveList;
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

        zumbaiPool = new List<GameObject>();
        zumbaiActiveList = new List<GameObject>();
        AddZumbai();
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < zumbaiPool.Count; i++)
        {
            if (!zumbaiPool[i].activeInHierarchy)
            {
                return zumbaiPool[i];
            }
        }
        GameObject gameObject;
        gameObject = Instantiate(zumbaiPref, transform);
        gameObject.SetActive(false);
        zumbaiPool.Add(gameObject);
        return gameObject;
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
        foreach (GameObject zumbai in zumbaiActiveList)
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
        zumbaiActiveList = zumbaiActiveList.OrderByDescending(Zumbai => Zumbai.transform.position.x).ToList();
    }

    public void AddZumbai()
    {
        GameObject zumbaiOb = GetPooledObject();
        zumbaiOb.transform.position = centerOfBoids.position + new Vector3(0,5,0);
        zumbaiOb.SetActive(true);
        zumbaiActiveList.Add(zumbaiOb);
    }
    public void RemoveZumbai(GameObject zumbai)
    {
        zumbaiActiveList.Remove(zumbai);
    }
    public int GetZumbaiCount()
    {
        return zumbaiPool.Count;
    }
    public Vector3 GetCenterOfBoids()
    {
        return centerOfBoids.position;
    }
    public List<GameObject> GetListZumbai()
    {
        return zumbaiPool;
    }
}
