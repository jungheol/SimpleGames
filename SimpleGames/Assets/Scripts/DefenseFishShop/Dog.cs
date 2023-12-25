using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {
    
    void FixedUpdate() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = mousePos.x;
        if (x > 8.5f) { 
            x = 8.5f;
        }
        if (x < -8.5f) {
            x = -8.5f;
        }
        transform.position = new Vector3(x, transform.position.y, 0);
    }
}
