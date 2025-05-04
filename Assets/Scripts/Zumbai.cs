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
    [SerializeField] private float jumpCheck = 2f;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Interact();
    }


    private void Interact()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.right, out hit, 1f))
        {
            Debug.Log("Hit something");
            Hoomen hoomen = hit.collider.GetComponentInParent<Hoomen>();
            if (hoomen != null)
            {
                hoomen.Die();
                ZumbaiManager.Instance.AddZumbai();
            }
        }
        Debug.DrawRay(transform.position, Vector3.right, Color.red);
    }

    public void JumpPress()
    {
        canJump = Physics.Raycast(transform.position, Vector3.down, jumpCheck);
        Debug.DrawRay(transform.position, Vector3.down * jumpCheck);
        rb.drag = defaultDrag;
        if (canJump)
        {
            rb.AddForce(new Vector3(0, jumpForce));
        }
    }
    public void JumpHold()
    {
        rb.drag = onHoldDrag;
    }
    public void JumpRelease()
    {
        rb.drag = defaultDrag;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
