using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class ZumbaiManager : MonoBehaviour
{
    public static ZumbaiManager Instance { get; private set; }

    public event EventHandler OnZumbaiChanged;
    public event EventHandler OnZumbaiExplosed;

    [SerializeField] private List<GameObject> zumbaiPool;
    [SerializeField] private List<GameObject> zumbaiActiveList;
    [SerializeField] private GameObject zumbaiPref;
    [SerializeField] private float jumpDelay = 0.08f;
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
        GameManager.Instance.OnGameStart += GameManager_OnGameStart;

        zumbaiPool = new List<GameObject>();
        zumbaiActiveList = new List<GameObject>();
    }

    private void GameManager_OnGameStart(object sender, EventArgs e)
    {
        NewGame();
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
        if (!GameManager.Instance.GetIsPlaying()) yield return false;
        else
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
                        yield return new WaitForSeconds(0);
                        break;
                }
                yield return new WaitForSeconds(jumpDelay);
            }
    }

    public void NewGame()
    {
        foreach(GameObject zumbai in zumbaiPool)
        {
            zumbai.SetActive(false);
        }
        zumbaiActiveList = new List<GameObject>();
        AddZumbai(5);
    }

    private void SortZumbai()
    {
        zumbaiActiveList = zumbaiActiveList.OrderByDescending(Zumbai => Zumbai.transform.position.x).ToList();
    }

    public void AddZumbai(float fallHeight = 40)
    {
        GameObject zumbaiOb = GetPooledObject();
        zumbaiOb.transform.position = centerOfBoids.position + new Vector3(0, fallHeight, UnityEngine.Random.Range(-2,2));
        zumbaiOb.SetActive(true);
        zumbaiActiveList.Add(zumbaiOb);
        OnZumbaiChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveZumbai(GameObject zumbai)
    {
        zumbaiActiveList.Remove(zumbai);
        OnZumbaiChanged?.Invoke(this, EventArgs.Empty);
        OnZumbaiExplosed?.Invoke(this, EventArgs.Empty);

    }
    public int GetZumbaiCount()
    {
        return zumbaiActiveList.Count;
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
