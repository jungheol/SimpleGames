using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButtonManager : MonoBehaviour {
    
    private GameObject pausePanel;

    private void Start() {
        pausePanel = transform.Find("PausePanel").gameObject;
    }

    public virtual void ClickPauseButton() {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        Button continueButton = pausePanel.transform.Find("Continue").GetComponent<Button>();
        Button exitButton = pausePanel.transform.Find("Exit").GetComponent<Button>();
        
        continueButton.onClick.AddListener(ContinueGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void ContinueGame() {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    private void ExitGame() {
        SceneManager.LoadScene((int)TopManager.SceneName.INTROSCENE);
    } 
}
