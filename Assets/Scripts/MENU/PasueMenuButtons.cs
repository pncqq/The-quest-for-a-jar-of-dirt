using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// Potrzebne do obsługi scen

namespace MENU
{
    public class PauseMenuButtons : MonoBehaviour
    {
        public GameObject pauseMenuUI; 

        
        public void ContinueGame()
        {
            pauseMenuUI.SetActive(false); 
            Time.timeScale = 1f; 
        }

       
        public void LoadMenu()
        {
            Time.timeScale = 1f; 
            SceneManager.LoadScene("Scenes/StartMenu"); 
        }

        public void QuitGame()
        {
           
        #if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
        #else
                            Aplication.Quit();
        #endif
                }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}