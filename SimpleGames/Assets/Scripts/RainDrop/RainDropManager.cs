using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RainDropManager : MonoBehaviour {
    
	public static RainDropManager instance;
	public GameObject rain;
	public GameObject panel;
	public Text scoreText;
	public Text bestScore;
	public Text thisScore;
	public Text timeText;
    
	private int totalScore = 0;
	private float limit = 30.0f;

	private void Awake() {
		instance = this;
	}

	// Start is called before the first frame update
	void Start() {
		InvokeRepeating("MakeRain", 0, 0.5f);
		initGame();
	}
    
	void initGame() {
		Time.timeScale = 1.0f;
		totalScore = 0;
		limit = 30.0f;
	}

	private void Update() {
		limit -= Time.deltaTime;
		timeText.text = limit.ToString("N2");
		if(limit < 0) {
			EndGame();
		}
	}

	void MakeRain() {
		Instantiate(rain);
	}

	public void AddScore(int score) {
		totalScore += score;
		scoreText.text = totalScore.ToString();
	}

	void EndGame() {
		panel.SetActive(true);
		limit = 0.0f;
		Time.timeScale = 0.0f;
        
		if (PlayerPrefs.HasKey("RainBest") == false) {
			PlayerPrefs.SetInt("RainBest", totalScore);
		} else {
			if (PlayerPrefs.GetInt("RainBest") < totalScore) PlayerPrefs.SetInt("RainBest", totalScore);  
		}

		thisScore.text = totalScore.ToString();
		bestScore.text = PlayerPrefs.GetInt("RainBest").ToString();
	}

	public void RetryGame() {
		SceneManager.LoadScene((int)TopManager.SceneName.RAINDROP);
	}
    
	public void MoveIntroScene() {
		SceneManager.LoadScene((int)TopManager.SceneName.INTROSCENE);
	}
}