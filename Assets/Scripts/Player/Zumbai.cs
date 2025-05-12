using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zumbai : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private float defaultDrag = 1;
    [SerializeField] private float onHoldDrag = 5;
    [SerializeField] private float jumpCheck = 1f;
    [SerializeField] private float gravityScale = 3;


    [SerializeField] private float neighborRadius = 2.5f;
    [SerializeField] private float separationDistance = 1.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInteract();

    }
    private void FixedUpdate()
    {
        HandleJump();
        if (isGrounded)
        {
            HandleBoidsMovement();
        }
    }

    private void HandleBoidsMovement()
    {
        List<GameObject> neighbors = GetNeighbors();
        Vector3 alignment = Vector3.zero;
        Vector3 separation = Vector3.zero;
        Vector3 cohesion = Vector3.zero;
        Vector3 steer = Vector3.zero;

        int count = 0;
        foreach(GameObject neighbor in neighbors)
        {
            if (neighbor == gameObject) continue;
            float distance = (neighbor.transform.position - transform.position).magnitude;
            if(distance < separationDistance)
            {
                separation -= (neighbor.transform.position - transform.position) / distance;
            }
            alignment += neighbor.transform.position;
            count++;
        }

        cohesion = isGrounded ? (ZumbaiManager.Instance.GetCenterOfBoids() - transform.position).normalized : Vector3.zero;

        if(count > 0)
        {
            steer += separation * 0.04f;
            alignment = (alignment / count).normalized;
        }
        steer += cohesion * 0.033f;
        steer += alignment * 0.035f;

        if (isGrounded)
        {
            rb.velocity += steer * gravityScale;
        }
    }

    private List<GameObject> GetNeighbors()
    {
        List<GameObject> neighbors = new List<GameObject>();
        foreach(GameObject zumbaiOb in ZumbaiManager.Instance.GetListZumbai())
        {
            if(zumbaiOb != this && (zumbaiOb.transform.position - transform.position).magnitude <= neighborRadius)
            {
                neighbors.Add(zumbaiOb);
            }
        }
        return neighbors;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, neighborRadius);
        foreach(GameObject zumbaiOb in GetNeighbors())
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, zumbaiOb.transform.position);
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, Vector3.down * jumpCheck);
    }

    private void HandleInteract()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.right, out hit, 2f))
        {
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
                Die();
            }
        }
        Debug.DrawRay(transform.position, Vector3.right, Color.red);
    }

    private void HandleJump()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down,out hit, jumpCheck);
        if (isGrounded)
        {
            Zumbai zumbai = hit.collider.GetComponentInParent<Zumbai>();
            if(zumbai != null)
            {
                rb.AddForce(Vector3.left * 20f);
            }
        }
    }

    public void JumpPress()
    {
        Debug.DrawRay(transform.position, Vector3.down * jumpCheck);
        rb.drag = defaultDrag;
        if (isGrounded)
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
        rb.AddForce(new Vector3(0, -jumpForce / 5));
    }

    public void Die()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.black;
        rb.AddForce(new Vector3(0, jumpForce));
        GetComponentInChildren<CapsuleCollider>().isTrigger = true;

        ZumbaiManager.Instance.RemoveZumbai(this.gameObject);
        StartCoroutine(ResetAfterFall(2f));

    }

    private IEnumerator ResetAfterFall(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
        GetComponentInChildren<CapsuleCollider>().isTrigger = false;
        gameObject.SetActive(false);
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }
}
