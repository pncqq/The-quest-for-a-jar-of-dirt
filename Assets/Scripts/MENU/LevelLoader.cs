using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MENU
{
    public class LevelLoader : MonoBehaviour
    {
       [SerializeField] private Animator transition;
       [SerializeField] private float transitionTime = 1f;
       private static readonly int Start1 = Animator.StringToHash("Start");
    
       

  

       private void Update()
       {
           if (PlayerPrefs.GetInt("EndLevel") == 1)
           {
               LoadNextLevel();
           }
       }

       public void LoadNextLevel()
        {
            PlayerPrefs.SetInt("EndLevel", 0);
            var nextLevel = PlayerPrefs.GetString("NextLevel");
            PlayerPrefs.Save();
            StartCoroutine(LoadLevel(nextLevel));
        }

        IEnumerator LoadLevel(string level)
        {
            transition.SetTrigger(Start1);
            
            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(level);
        }
    }
}