using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// Potrzebne do obsługi scen

namespace MENU
{
    public class PauseMenuButtons : MonoBehaviour
    {
        public GameObject pauseMenuUI;
        [SerializeField] private AudioSource clickSound;

        
        public void ContinueGame()
        {
            clickSound.Play();
            pauseMenuUI.SetActive(false); 
            Time.timeScale = 1f; 
        }

       
        public void LoadMenu()
        {
            clickSound.Play();
            Time.timeScale = 1f; 
            SceneManager.LoadScene("Scenes/StartMenu"); 
        }

        public void QuitGame()
        {
            clickSound.Play();
            Application.Quit();
        }

        public void RestartGame()
        {
            clickSound.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }
}