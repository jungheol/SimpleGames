using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {
    
	float direction = 0.05f;
	float toward = 4.0f;
    
	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			toward *= -1;
			direction *= -1;
		}
		if (transform.position.x > 2.5f) {
			direction = -0.05f;
			toward = -4.0f;
		}
		if (transform.position.x < -2.5f) {
			direction = 0.05f;
			toward = 4.0f;
		}
		transform.localScale = new Vector3(toward, 4, 1);
	}
    
	private void FixedUpdate() {
		transform.position += new Vector3(direction, 0, 0);
	}
}