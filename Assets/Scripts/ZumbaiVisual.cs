using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumbaiVisual : MonoBehaviour
{
    private Zumbai zumbai;
    private Animator animator;


    private void Awake()
    {
        
    }
    private void Start()
    {
        zumbai = GetComponentInParent<Zumbai>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (zumbai.GetIsGrounded())
        {
            animator.CrossFade("Run", 0);
        }
        else
        {
            animator.CrossFade("Jump", 0);
        }
    }
}
