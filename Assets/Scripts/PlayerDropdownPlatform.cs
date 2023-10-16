using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerDropdownPlatform : MonoBehaviour
{


    private GameObject _currentPlatform;
    [SerializeField]  BoxCollider2D _player;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(_currentPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DropdownPlatform"))
        {
            _currentPlatform = collision.gameObject;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DropdownPlatform"))
        {
            _currentPlatform = null;
        }
    }


    private IEnumerator DisableCollision()
    {
        TilemapCollider2D _platformCollider = _currentPlatform.GetComponent<TilemapCollider2D>();

        Physics2D.IgnoreCollision(_player, _platformCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision (_player, _platformCollider, false);

    }
}
