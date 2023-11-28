using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MENU
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenuUI;
        public TextMeshProUGUI diamondLevel;
        [SerializeField] private AudioSource clickSound;

        private bool isPaused = false;
        
        private void Awake()
        {
            pauseMenuUI.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
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
            clickSound.Play();
            pauseMenuUI.SetActive(false); 
            Time.timeScale = 1f; 
            isPaused = false;
        }

        private void  Pause()
        {
            clickSound.Play();
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

