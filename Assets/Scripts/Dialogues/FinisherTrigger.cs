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
            public int actualLevel;
            public string nextLevel;
            
            
            
            private void OnTriggerEnter2D(Collider2D other)
            {
                if (other.CompareTag("Player"))
                {
                    _playerDetected = true;
                    dialogueScript.ToggleIndicator(_playerDetected);

                }

                if (CollectibleCounter.instance.gCoinCount == 0)
                {
                    
                    
                    if (actualLevel == 1)
                    {
                        _sentences.Clear();
                        _sentences.Add("Żartujesz sobie? Jesteś biedny!");
                    } else if (actualLevel == 2)
                    {
                        _sentences.Clear();
                        _sentences.Add("Nie denerwuj mnie");
                       
                    }
                    else if (actualLevel == 3)
                    {
                        _sentences.Clear();
                        _sentences.Add("Kierowniku… Chcesz żebym umarł z głodu? Aż mi się ręce trzęsą. I to ze złości, nie z delirki.");
                    }
                    
                } else if (CollectibleCounter.instance.gCoinCount >= _requiredCoins)
                {
                    if (actualLevel == 1)
                    {
                        _sentences.Clear();
                        _sentences.Add("No, i teraz możemy rozmawiać…");
                        _sentences.Add("End level");
                        _sentences.Add(nextLevel);
                    }
                    else if (actualLevel == 2)
                    {
                        _sentences.Clear();
                        _sentences.Add("No, i teraz możemy rozmawiać…");
                        _sentences.Add("End level");
                        _sentences.Add(nextLevel);
                    }
                    else if (actualLevel == 3)
                    {
                        _sentences.Clear();
                        _sentences.Add("Szerokiej drogi  człowiecze! Niech Ci się wiedzie!");
                        _sentences.Add("End level");
                        _sentences.Add(nextLevel);
                    }
                   
                }
                else
                {
                    if (actualLevel == 1)
                    {
                        _sentences.Clear();
                        _sentences.Add("Za mało! Przynieś więcej!");
                    } else if (actualLevel == 2)
                    {
                        _sentences.Clear();
                        _sentences.Add("Za mało! Wracaj na te łódki!");
                       
                    }
                    else if (actualLevel == 3)
                    {
                        _sentences.Clear();
                        _sentences.Add("Szefie, super, ale to nawet na rum nie starczy.");
                    }
                   
                    
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
