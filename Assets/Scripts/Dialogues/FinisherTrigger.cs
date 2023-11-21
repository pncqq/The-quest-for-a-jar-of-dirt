using System;
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

            private void Awake()
            {
                
                if (actualLevel == 1 && PlayerPrefs.GetInt("AllDiamonds1") == 1)
                {
                    RemoveAllDiamonds();
                }
                else if (actualLevel == 2 && PlayerPrefs.GetInt("AllDiamonds2") == 1)
                {
                    RemoveAllDiamonds();
                }
                else if (actualLevel == 3 && PlayerPrefs.GetInt("AllDiamonds3") == 1)
                {
                    RemoveAllDiamonds();
                }
            }

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
                        PlayerPrefs.SetInt("Level1", 1);
                        PlayerPrefs.SetInt("Diamonds1", CollectibleCounter.instance.dCount);
                        if (CollectibleCounter.instance.dCount == 5)
                        {
                            PlayerPrefs.SetInt("AllDiamonds1", 1);
                        }
                        Debug.Log(PlayerPrefs.GetInt("Diamonds1"));
                        _sentences.Clear();
                        _sentences.Add("No, i teraz możemy rozmawiać…");
                        _sentences.Add("End level");
                        _sentences.Add(nextLevel);
                    }
                    else if (actualLevel == 2)
                    {
                        PlayerPrefs.SetInt("Level2", 1);
                        if (CollectibleCounter.instance.dCount == 5)
                        {
                            PlayerPrefs.SetInt("AllDiamonds2", 1);
                        }
                        PlayerPrefs.SetInt("Diamonds2", CollectibleCounter.instance.dCount);
                       
                        _sentences.Clear();
                        _sentences.Add("No, i teraz możemy rozmawiać…");
                        _sentences.Add("End level");
                        _sentences.Add(nextLevel);
                    }
                    else if (actualLevel == 3)
                    {
                        PlayerPrefs.SetInt("Level3", 1);
                        if (CollectibleCounter.instance.dCount == 5)
                        {
                            PlayerPrefs.SetInt("AllDiamonds3", 1);
                        }
                        PlayerPrefs.SetInt("Diamonds3", CollectibleCounter.instance.dCount);
                        _sentences.Clear();
                        _sentences.Add("Szerokiej drogi  człowiecze! Niech Ci się wiedzie!");
                        _sentences.Add("End level");
                        _sentences.Add(nextLevel);
                    }
                    PlayerPrefs.Save();
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
            public void RemoveAllDiamonds()
            {
                GameObject[] diamonds = GameObject.FindGameObjectsWithTag("Diamond");
                
                foreach (var diamond in diamonds)
                {
                    Destroy(diamond);
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
