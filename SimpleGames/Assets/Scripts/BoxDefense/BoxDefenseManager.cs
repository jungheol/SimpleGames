using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoxDefenseManager : MonoBehaviour {

    public GameObject box;
    public GameObject endPanel;
    public Text timeText;
    public Text thisScoreText;
    public Text bestScoreText;
    public Animator anim;

    private float bubbleAlive = 0f;
    private bool isRunning = true;
    
    void Start() {
        Time.timeScale = 1.0f;
        InvokeRepeating("MakeBox", 0.0f, 0.5f);
    }
    
    void MakeBox() {
        Instantiate(box);
    }

    void Update() {
        if (isRunning) {
            bubbleAlive += Time.deltaTime;
            timeText.text = bubbleAlive.ToString("N2");
        }
    }
    
    public void GameOver() {
        anim.SetBool("isDie", true);
        
        isRunning = false;
        Invoke("TimeStop", 0.15f);
        thisScoreText.text = bubbleAlive.ToString("N2");
        endPanel.SetActive(true);

        if (PlayerPrefs.HasKey("BubbleBest") == false) {
            PlayerPrefs.SetFloat("BubbleBest", bubbleAlive);
        } else {
            if (PlayerPrefs.GetFloat("BubbleBest") < bubbleAlive) PlayerPrefs.SetFloat("BubbleBest", bubbleAlive);  
        }

        bestScoreText.text = PlayerPrefs.GetFloat("BubbleBest").ToString("N2");
    }
    
    public void RetryGame() {
        SceneManager.LoadScene((int)TopManager.SceneName.BOXDEFENSE);
    }

    public void MoveIntroScene() {
        SceneManager.LoadScene((int)TopManager.SceneName.INTROSCENE);
    }

    void TimeStop() {
        Time.timeScale = 0.0f;
    }
}
