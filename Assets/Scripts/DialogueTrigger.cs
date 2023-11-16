using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool _playerDetected;
    public Dialogue dialogueScript;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerDetected = true;
            dialogueScript.ToggleIndicator(_playerDetected);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerDetected = false;
            dialogueScript.ToggleIndicator(_playerDetected);
            dialogueScript.EndDialogue();
        }
    }

    private void Update()
    {
        if (_playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            dialogueScript.StartDialogue();
        }
    }
}
