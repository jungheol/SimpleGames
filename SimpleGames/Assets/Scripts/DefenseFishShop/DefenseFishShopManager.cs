using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefenseFishShopManager : MonoBehaviour {
    
    public static DefenseFishShopManager instance;
    public GameObject dog;
    public GameObject food;
    public GameObject normalCat;
    public GameObject fatCat;
    public GameObject pirateCat;
    public GameObject endPanel;
    public Text levelText;
    public Text scoreText;
    public Text thisScoreText;
    public Text bestScoreText;
    public GameObject levelFront;

    private int level = 0;
    private int cat = 0;
    private int totalScore = 0;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Time.timeScale = 1.0f;
        InvokeRepeating("MakeFood", 0.0f, 0.1f);
        InvokeRepeating("MakeCat", 0.0f, 1.0f);
    }

    private void MakeFood() {
        float x = dog.transform.position.x;
        float y = dog.transform.position.y;
        Instantiate(food, new Vector3(x, y, 0), Quaternion.identity);
    }

    private void MakeCat() {
        Instantiate(normalCat);
        float p = Random.Range(0, 10);
        
        if (level == 1) {
            if (p < 2) Instantiate(normalCat);
        } else if (level == 2) {
            if (p < 4) Instantiate(normalCat);
        } else if (level == 3) {
            if (p < 5) Instantiate(normalCat);
            Instantiate(fatCat);
        } else if (level >= 4) {
            if (p < 6) Instantiate(normalCat);
            Instantiate(fatCat);
            Instantiate(pirateCat);
        }
    }

    public void GameOver() {
        endPanel.SetActive(true);
        Time.timeScale = 0.0f;
        
        if (PlayerPrefs.HasKey("DogBest") == false) {
            PlayerPrefs.SetInt("DogBest", totalScore);
        } else {
            if (PlayerPrefs.GetInt("DogBest") < totalScore) PlayerPrefs.SetInt("DogBest", totalScore);  
        }

        thisScoreText.text = totalScore.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("DogBest").ToString();
    }

    public void AddCat(int score) {
        cat += 1;
        level = cat / 5;

        levelText.text = level.ToString();
        levelFront.transform.localScale = new Vector3((cat - level * 5) / 5.0f, 1.0f, 1.0f);
        totalScore += score;
        scoreText.text = totalScore.ToString();
    }

    public void RetryGame() {
        SceneManager.LoadScene((int)TopManager.SceneName.DEFENSEFISHSHOP);
    }
    
    public void MoveIntroScene() {
        SceneManager.LoadScene((int)TopManager.SceneName.INTROSCENE);
    }
}
