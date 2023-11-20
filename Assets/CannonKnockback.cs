using UnityEngine;

public class CannonKnockback : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag($"CannonBall")) return;
        gameObject.GetComponent<BasicPlayerMovement>().knockbackTimer =
            gameObject.GetComponent<BasicPlayerMovement>().knockbackTotal;

        gameObject.GetComponent<BasicPlayerMovement>().knockbackRight =
            !(other.transform.position.x <= transform.position.x);
    }
}