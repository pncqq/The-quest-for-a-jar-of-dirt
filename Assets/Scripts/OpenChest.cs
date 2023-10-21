using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private bool isOpen;
    private Animator animator;
    // Start is called before the first frame update
    public void openChest()
    {
        if(!isOpen)
        {
            isOpen = true;
            animator.SetBool("IsOpen", isOpen);
        }
    }

  
}
