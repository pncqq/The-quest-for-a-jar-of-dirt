using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MENU
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenuUI; // Przypisz tutaj twój panel/menu pauzy

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
            pauseMenuUI.SetActive(true); 
            Time.timeScale = 0f; 
            isPaused = true;
        }
        
    }
}

