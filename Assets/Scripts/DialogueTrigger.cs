using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool _playerDetected;
    public Dialogue dialogueScript;
    private int _requiredCoins = 2;
    private bool _finisher;
    List<string> dialogueContent = new List<string>();
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

        _finisher = other.gameObject.CompareTag("Finish");


    }

    private void Update()
    {
        if (_finisher && Input.GetKeyDown(KeyCode.E))
        {
            int playerCoins = CollectibleCounter.instance.gCoinCount; 

            

            if (playerCoins >= _requiredCoins)
            {
                // Ustaw treść dialogu dla wystarczającej ilości monet
                dialogueContent.Add("Masz wystarczająco monet! Przechodzisz do następnego poziomu.");
                Debug.Log("sa monety frajerze");
                //nastepny lvl
            }
            else
            {
                // Ustaw treść dialogu dla niewystarczającej ilości monet
                dialogueContent.Add("Potrzebujesz więcej monet, aby kontynuować!");
                Debug.Log("No eni ma monet");
                
            }
        }
           
        
        if (_playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            dialogueScript.StartDialogueWithContent(dialogueContent);
        }
        
    }
}
