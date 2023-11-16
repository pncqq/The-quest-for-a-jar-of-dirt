using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dialogues
{
    public class FinisherTrigger : MonoBehaviour
    {
       
            private bool _playerDetected;
            public Dialogue dialogueScript;
            private readonly List<string> _sentences = new List<string>();
            private int _requiredCoins = 2;
            public string nextLevel;
            
            
            
            private void OnTriggerEnter2D(Collider2D other)
            {
                if (other.CompareTag("Player"))
                {
                    _playerDetected = true;
                    dialogueScript.ToggleIndicator(_playerDetected);

                }

                if (CollectibleCounter.instance.gCoinCount >= _requiredCoins)
                {
                    _sentences.Clear();
                    _sentences.Add("Gratulacje!");
                    _sentences.Add("Posiadasz wymaganą liczbę monet!");
                    _sentences.Add("Jednak to jeszcze nie koniec twojej przygody!");
                    _sentences.Add("End level");
                    _sentences.Add(nextLevel);
                }
                else
                {
                    _sentences.Clear();
                    _sentences.Add("Uzbieraj więcej monet frajerze!");
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
                    dialogueScript.StartDialogueWithSentences(_sentences);
                }
            }
            
        }
    }
