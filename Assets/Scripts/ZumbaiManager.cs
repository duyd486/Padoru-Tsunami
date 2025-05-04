using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZumbaiManager : MonoBehaviour
{
    [SerializeField] private List<Zumbai> zumbaiList;
    [SerializeField] private GameObject zumbaiPref;
    [SerializeField] private float jumpDelay = 0.1f;

    enum Jump
    {
        JumpPress,
        JumpHold,
        JumpRelease,
    }

    private void Start()
    {
        GameInput.Instance.OnJumpAction += GameInput_OnJumpAction;
        GameInput.Instance.OnJumpHold += GameInput_OnJumpHold;
        GameInput.Instance.OnJumpRelease += GameInput_OnJumpRelease;

        GameInput.Instance.OnTestPress += GameInput_OnTestPress;

        zumbaiList = new List<Zumbai>();
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
        foreach (Zumbai zumbai in zumbaiList)
        {
            switch (jump)
            {
                default:
                case Jump.JumpPress:
                    zumbai.JumpPress();
                    break;
                case Jump.JumpHold:
                    zumbai.JumpHold();
                    break;
                case Jump.JumpRelease:
                    zumbai.JumpRelease();
                    break;
            }
            yield return new WaitForSeconds(jumpDelay);
        }
    }

    private void SortZumbai()
    {
        zumbaiList = zumbaiList.OrderByDescending(Zumbai => Zumbai.transform.position.x).ToList();
    }

    private void AddZumbai()
    {
        GameObject zumbaiOb = Instantiate(zumbaiPref, transform);
        Zumbai zumbai = zumbaiOb.GetComponent<Zumbai>();
        zumbaiList.Add(zumbai);
        zumbaiOb.transform.position = new Vector3(Random.Range(-10f, 10f), 10, Random.Range(-5f, 5f));
    }
}
