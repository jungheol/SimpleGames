using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class IntroManager : MonoBehaviour {
	
	private void Start() {
		MobileAds.Initialize((initStatus) => {
			AdsManager.Instance.LoadAd();
		});
	}

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