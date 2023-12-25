using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {

	public void MoveBoxDefenseScene() {
		SceneManager.LoadScene((int)TopManager.SceneName.BOXDEFENSE);
	}

	public void MoveHungryCatScene() {
		SceneManager.LoadScene((int)TopManager.SceneName.HUNGRYCAT);
	}

	public void MoveFindCardScene() {
		SceneManager.LoadScene((int)TopManager.SceneName.FINDCARD);
	}

	public void MoveRainDropScene() {
		SceneManager.LoadScene((int)TopManager.SceneName.RAINDROP);
	}
}