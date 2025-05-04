using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zumbai : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private bool canJump;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private float defaultDrag = 1;
    [SerializeField] private float onHoldDrag = 5;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void JumpPress()
    {
        canJump = Physics.Raycast(transform.position, Vector3.down, 1f);
        rb.drag = defaultDrag;
        if (canJump)
        {
            rb.AddForce(new Vector3(0, jumpForce));
        }
    }
    public void JumpHold()
    {
        Debug.Log("Holdd");
        rb.drag = onHoldDrag;
    }
    public void JumpRelease()
    {
        rb.drag = defaultDrag;
    }
}
