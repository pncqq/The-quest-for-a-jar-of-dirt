using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MENU
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenuUI;
        public TextMeshProUGUI diamondLevel; // Przypisz tutaj twój panel/menu pauzy

        private bool isPaused = false;
        
        private void Awake()
        {
            pauseMenuUI.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        private void Resume()
        {
            pauseMenuUI.SetActive(false); // Ukryj menu pauzy
            Time.timeScale = 1f; // Wznów normalny bieg czasu
            isPaused = false;
        }

        private void  Pause()
        {
            CountDiamonds();
            pauseMenuUI.SetActive(true); 
            Time.timeScale = 0f; 
            isPaused = true;
        }

        private void CountDiamonds()
        {
            
            GameObject[] diamonds = GameObject.FindGameObjectsWithTag("Diamond");
            if (CollectibleCounter.instance.dCount == 0 && diamonds.Length == 0)
            {
                diamondLevel.text = "5 / 5";
            }
            else
            {
                diamondLevel.text = CollectibleCounter.instance.dCount + " / 5";
            }
            
          
         
        }
        
    }
}

