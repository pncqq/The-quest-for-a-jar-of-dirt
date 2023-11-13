using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [SerializeField] private int dVal;
    [SerializeField] private int scVal;
    [SerializeField] private int gcVal;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private bool isOpen;
    private Animator animator;
    public void Open()
    {
        if(!isOpen)
        {
            if (gameObject.CompareTag("LockedChest"))
            {
                if (CollectibleCounter.instance.kCount <= 0)
                    return;
                CollectibleCounter.instance.UseKey();
            }
            isOpen = true;
            animator.SetBool("IsOpen", isOpen);
            if(dVal > 0)
                CollectibleCounter.instance.IncreaseCount(dVal, 'd');
            if(gcVal > 0)
                CollectibleCounter.instance.IncreaseCount(gcVal, 'g');
            if(scVal > 0)
                CollectibleCounter.instance.IncreaseCount(scVal, 's');
        }
    }

  
}
