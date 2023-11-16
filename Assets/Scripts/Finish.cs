using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Finish : MonoBehaviour
{
    private readonly int _requiredCoins = 2;
    public Dialogue dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            int playerCoins = CollectibleCounter.instance.gCoinCount; // Załóżmy, że jest taka metoda

            List<string> dialogueContent = new List<string>();

            if (playerCoins >= _requiredCoins)
            {
                // Ustaw treść dialogu dla wystarczającej ilości monet
                dialogueContent.Add("Masz wystarczająco monet! Przechodzisz do następnego poziomu.");
                dialogueManager.StartDialogueWithContent(dialogueContent);
                //nastepny lvl
            }
            else
            {
                // Ustaw treść dialogu dla niewystarczającej ilości monet
                dialogueContent.Add("Potrzebujesz więcej monet, aby kontynuować!");
                dialogueManager.StartDialogueWithContent(dialogueContent);
            }
        }
    }

    private void CompleteLevel()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
