using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MENU
{
    public class MenuManager : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private AudioSource clickSound;
        [SerializeField] private GameObject _menuView;
        [SerializeField] private GameObject _levelsView;
        public Button level2; // Przycisk do aktywacji
        public Button level3; // Przycisk do aktywacji
        public TextMeshProUGUI diamondLevel1;
        public TextMeshProUGUI diamondLevel2;
        public TextMeshProUGUI diamondLevel3;
        
        
        

        public void LevelCompleted2()
        {
            level2.interactable = PlayerPrefs.GetInt("Level1") == 1; // Aktywuje przycisk
            level3.interactable = PlayerPrefs.GetInt("Level2") == 1; // Aktywuje przycisk
        }
     
   


        private void Awake()
        {
            _menuView.SetActive(true);
            _levelsView.SetActive(false);
            DiamondsUpdate();
            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 0.5f);
            }
        }

        #region Main view

        public void StartClicked()
        {
            clickSound.Play();
            LevelCompleted2();
            DiamondsUpdate();
            _menuView.SetActive(false);
            _levelsView.SetActive(true);
        }

        public void ClearGame()
        {
            clickSound.Play();
            PlayerPrefs.DeleteAll();
        }

        public void ExitClicked()
        {
            clickSound.Play();
            EditorApplication.isPlaying = false;

        }
    
        #endregion

        #region Levels View

        public void LoadLevel(string sceneName)
        {
            clickSound.Play();
            SceneManager.LoadScene(sceneName);
        }

        public void BackClicked()
        {
            clickSound.Play();
            _menuView.SetActive(true);
            _levelsView.SetActive(false);
        }
        

        #endregion

        #region Diaomonds

        private void DiamondsUpdate()
        {
            diamondLevel1.text = PlayerPrefs.GetInt("Diamonds1") + " / 5";
            diamondLevel2.text = PlayerPrefs.GetInt("Diamonds2") + " / 5";
            diamondLevel3.text = PlayerPrefs.GetInt("Diamonds3") + " / 5";
            if (PlayerPrefs.GetInt("AllDiamonds1") == 1)
            {
                diamondLevel1.text =  "5 / 5";
            }
            if (PlayerPrefs.GetInt("AllDiamonds2") == 1)
            {
                diamondLevel2.text =  "5 / 5";
            }
            if (PlayerPrefs.GetInt("AllDiamonds3") == 1)
            {
                diamondLevel3.text =  "5 / 5";
            }
        }

        #endregion
    }
}
