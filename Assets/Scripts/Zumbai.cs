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
            Bomb bomb = hit.collider.GetComponentInParent<Bomb>();
            
            if (hoomen != null)
            {
                hoomen.Die();
                ZumbaiManager.Instance.AddZumbai();
            }
            if(bomb != null)
            {
                bomb.Die();
                ZumbaiManager.Instance.RemoveZumbai();
                Die();
            }
        }
        Debug.DrawRay(transform.position, Vector3.right, Color.red);
    }

    public void JumpPress()
    {
        RaycastHit hit;
        canJump = Physics.Raycast(transform.position, Vector3.down, out hit, jumpCheck);
        Debug.DrawRay(transform.position, Vector3.down * jumpCheck);
        rb.drag = defaultDrag;
        if (canJump)
        {
            rb.AddForce(new Vector3(0, jumpForce));
            Zumbai zumbai = hit.collider.GetComponentInParent<Zumbai>();
            if (zumbai != null)
            {
                Debug.Log("I fall in a Zumbai!!!");
                rb.AddForce(Vector3.left * 100f);
            }
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
        ZumbaiManager.Instance.RemoveZumbai();
    }
}
