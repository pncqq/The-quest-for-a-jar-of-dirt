using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _menuView;
    [SerializeField] private GameObject _levelsView;


    private void Awake()
    {
        _menuView.SetActive(true);
        _levelsView.SetActive(false);
    }

    #region Main view

    public void StartClicked()
    {
        _menuView.SetActive(false);
        _levelsView.SetActive(true);
    }

    public void ExitClicked()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
            Aplication.Quit();
        #endif
    }
    
    #endregion

    #region Levels View

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void BackClicked()
    {
        _menuView.SetActive(true);
        _levelsView.SetActive(false);
    }
        

    #endregion
}
