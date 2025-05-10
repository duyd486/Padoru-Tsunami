using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zumbai : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private float fallForce = 200f;
    [SerializeField] private float defaultDrag = 1;
    [SerializeField] private float onHoldDrag = 5;
    [SerializeField] private float jumpCheck = 1f;


    private float neighborRadius = 2.5f;
    private float separationDistance = 1.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Interact();
        BoidsMovement();
        isGrounded = Physics.Raycast(transform.position, Vector3.down, jumpCheck);
    }


    private void BoidsMovement()
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
            steer += separation * 0.085f;
            alignment = (alignment / count).normalized;
        }
        steer += cohesion * 0.08f;
        steer += alignment * 0.1f;

        if (isGrounded)
        {
            rb.velocity += steer;
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
                Debug.Log("Human");
            }
            if(bomb != null)
            {
                bomb.Die();
                ZumbaiManager.Instance.RemoveZumbai();
                Die();
                Debug.Log("Bomb");
            }
        }
        Debug.DrawRay(transform.position, Vector3.right, Color.red);
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
    }

    public void Die()
    {
        gameObject.SetActive(false);
        ZumbaiManager.Instance.RemoveZumbai();
    }
}
