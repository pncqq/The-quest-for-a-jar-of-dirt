using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    private bool inRange;
    [SerializeField] private BoatScript boat;

    void Update()
    {
        if (inRange && boat.isIdle && boat.isDone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                boat.isDone = false;
                boat.isIdle = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
