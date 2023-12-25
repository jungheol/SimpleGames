using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RainType {
	small = 0,
	midium,
	large,
	red
}

public class Rain : MonoBehaviour {
    
	private RainType rainType;
	private float size;
	private int score;
    
	void Start() {
		SetPosition();
		SetType();
	}

	void SetPosition() {
		float x = Random.Range(-2.7f, 2.7f);
		float y = Random.Range(3.0f, 5.0f);
		transform.position = new Vector3(x, y, 0);
	}

	void SetType() {
		rainType = (RainType)Random.Range(0, 4);

		switch (rainType) {
			case RainType.large:
				size = 1.2f;
				score = 3;
				GetComponent<SpriteRenderer>().color = new Color(100 / 255f, 100 / 255f, 255 / 255f, 255 / 255f);
				break;
			case RainType.midium:
				size = 1.0f;
				score = 2;
				GetComponent<SpriteRenderer>().color = new Color(130 / 255f, 130 / 255f, 255 / 255f, 255 / 255f);
				break;
			case RainType.small:
				size = 0.8f;
				score = 1;
				GetComponent<SpriteRenderer>().color = new Color(150 / 255f, 150 / 255f, 255 / 255f, 255 / 255f);
				break;
			case RainType.red:
				size = 0.8f;
				score = -5;
				GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 100f / 255f, 100f / 255f, 255f / 255f);
				break;
		}
		transform.localScale = new Vector3(size, size, 0);
	}
    
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Ground") {
			Destroy(gameObject);
		}

		if (coll.gameObject.tag == "Character") {
			RainDropManager.instance.AddScore(score);
			Destroy(gameObject);
		}
	}
}