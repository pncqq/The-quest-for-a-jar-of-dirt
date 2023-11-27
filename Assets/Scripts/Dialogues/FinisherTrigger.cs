using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public class FinisherTrigger : MonoBehaviour
    {
        [SerializeField] private int requiredCoins = 2;
        private bool _playerDetected;
        public Dialogue dialogueScript;
        private readonly List<string> _sentences = new List<string>();
        public int actualLevel;
        public string nextLevel;
        private bool _firstDialogue;


        private void Awake()
        {
            //FILIP
            _firstDialogue = true;

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

        private void Update()
        {
            if (_playerDetected && Input.GetKeyDown(KeyCode.E))
                dialogueScript.StartDialogueWithSentences(_sentences);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerDetected = true;
                dialogueScript.ToggleIndicator(_playerDetected);
            }

            //FILIP
            if (_firstDialogue)
            {
                DefaultDialogue();
                return;
            }

            if (CollectibleCounter.instance.gCoinCount == 0)
            {
                switch (actualLevel)
                {
                    case 1:
                        _sentences.Clear();
                        _sentences.Add("Zartujesz sobie? Jestes biedny!");
                        break;
                    case 2:
                        _sentences.Clear();
                        _sentences.Add("Nie denerwuj mnie...");
                        break;
                    case 3:
                        _sentences.Clear();
                        _sentences.Add(
                            "Kierowniku... Chcesz zebym umarl z glodu? Az mi się rece trzesa. " +
                            "I to ze zlosci, nie z delirki.");
                        break;
                }
            }
            else if (CollectibleCounter.instance.gCoinCount >= requiredCoins)
            {
                switch (actualLevel)
                {
                    case 1:
                    {
                        PlayerPrefs.SetInt("Level1", 1);
                        PlayerPrefs.SetInt("Diamonds1", CollectibleCounter.instance.dCount);
                        if (CollectibleCounter.instance.dCount == 5)
                        {
                            PlayerPrefs.SetInt("AllDiamonds1", 1);
                        }

                        Debug.Log(PlayerPrefs.GetInt("Diamonds1"));
                        _sentences.Clear();
                        _sentences.Add("No, i teraz mozemy rozmawiac...");
                        _sentences.Add("End level");
                        _sentences.Add(nextLevel);
                        break;
                    }
                    case 2:
                    {
                        PlayerPrefs.SetInt("Level2", 1);
                        if (CollectibleCounter.instance.dCount == 5)
                        {
                            PlayerPrefs.SetInt("AllDiamonds2", 1);
                        }

                        PlayerPrefs.SetInt("Diamonds2", CollectibleCounter.instance.dCount);

                        _sentences.Clear();
                        _sentences.Add("No, i teraz mozemy rozmawiac...");
                        _sentences.Add("End level");
                        _sentences.Add(nextLevel);
                        break;
                    }
                    case 3:
                    {
                        PlayerPrefs.SetInt("Level3", 1);
                        if (CollectibleCounter.instance.dCount == 5)
                        {
                            PlayerPrefs.SetInt("AllDiamonds3", 1);
                        }

                        PlayerPrefs.SetInt("Diamonds3", CollectibleCounter.instance.dCount);
                        _sentences.Clear();
                        _sentences.Add("Szerokiej drogi  czlowiecze! Niech Ci sie wiedzie!");
                        _sentences.Add("End level");
                        _sentences.Add(nextLevel);
                        break;
                    }
                }

                PlayerPrefs.Save();
            }
            else
            {
                switch (actualLevel)
                {
                    case 1:
                        _sentences.Clear();
                        _sentences.Add("Za malo! Przynies wiecej!");
                        break;
                    case 2:
                        _sentences.Clear();
                        _sentences.Add("Za malo! Wracaj na te lodki!");
                        break;
                    case 3:
                        _sentences.Clear();
                        _sentences.Add("Szefie, super, ale to nawet na rum nie starczy.");
                        break;
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

        private static void RemoveAllDiamonds()
        {
            var diamonds = GameObject.FindGameObjectsWithTag("Diamond");

            foreach (var diamond in diamonds)
            {
                Destroy(diamond);
            }
        }


        //FILIP
        private void DefaultDialogue()
        {
            //Default dialogue FILIP
            switch (actualLevel)
            {
                case 1:
                    _sentences.Clear();
                    _sentences.Add("Co tu robi czlowiek?!");
                    _sentences.Add("Dobra, niewazne. Daj mi "
                                   + requiredCoins + " zlotych monet i przejdziesz dalej.");
                    break;
                case 2:
                    _sentences.Clear();
                    _sentences.Add("Nie dostales choroby morskiej?");
                    _sentences.Add("Niewazne. Widze ze masz nowy sprzet. Skad na niego miales?");
                    _sentences.Add("Powodzi Ci sie, fajnie.");
                    _sentences.Add("To dawaj " + requiredCoins + " monet.");
                    break;
                default:
                    _sentences.Clear();
                    _sentences.Add("Niezle sobie z tym wszystkim poradziles. " +
                                   "Ale niestety, przyjacielu, to chyba koniec wycieczki.");
                    _sentences.Add("Fajnie sie wspolpracowalo.");
                    _sentences.Add("Ale daj no jeszcze, ze " + requiredCoins + " zlote.");
                    break;
            }

            _firstDialogue = false;
        }
    }
}