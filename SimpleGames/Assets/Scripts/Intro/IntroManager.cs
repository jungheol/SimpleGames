using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {

	public void MoveBubbleGameScene() {
		SceneManager.LoadScene((int)TopManager.SceneName.BOXDEFENSE);
	}

	public void MoveDogvsCatGameScene() {
		SceneManager.LoadScene((int)TopManager.SceneName.HUNGRYCAT);
	}

	public void MoveFindCardGameScene() {
		SceneManager.LoadScene((int)TopManager.SceneName.FINDCARD);
	}

	public void MoveRainDropGameScene() {
		SceneManager.LoadScene((int)TopManager.SceneName.RAINDROP);
	}
}