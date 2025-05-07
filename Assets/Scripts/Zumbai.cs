using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zumbai : MonoBehaviour
{
    private Rigidbody rb;
    private Transform childrenTransform;
    [SerializeField] private bool canJump;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private float defaultDrag = 1;
    [SerializeField] private float onHoldDrag = 5;
    [SerializeField] private float jumpCheck = 1f;


    [SerializeField] private float neighborRadius = 2f;
    [SerializeField] private float separationDistance = 2f;






    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        childrenTransform = GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        Interact();
        BoidsMovement();
    }


    private void BoidsMovement()
    {
        List<Transform> neighbors = GetNeighbors();
        Vector3 alignment = Vector3.zero;
        Vector3 separation = Vector3.zero;
        Vector3 cohesion = Vector3.zero;

        int count = 0;

        foreach (Transform neighbor in neighbors)
        {
            if (neighbor == transform) continue;

            Vector3 toNeighbor = neighbor.position - transform.position;
            float distance = toNeighbor.magnitude;

            alignment += neighbor.GetComponentInParent<Zumbai>().transform.position;

            if (distance < separationDistance)
            {
                separation -= toNeighbor / distance;
            }
            count++;
        }
        if (count > 0)
        {
            alignment = (alignment / count).normalized;

            cohesion = (ZumbaiManager.Instance.GetCenterOfBoids() - transform.position).normalized;

            Vector3 steer = alignment
                          + cohesion
                          + separation;
        }

    }

    private List<Transform> GetNeighbors()
    {
        List<Transform> neighbors = new List<Transform>();
        Collider[] hits = Physics.OverlapSphere(transform.position, neighborRadius);

        foreach (var hit in hits)
        {
            if (hit.transform != childrenTransform && hit.GetComponentInParent<Zumbai>() != null)
            {
                neighbors.Add(hit.transform);
            }
        }
        Debug.Log(neighbors.Count);
        return neighbors;
    }


    private void Interact()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.right, out hit, 1f))
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
