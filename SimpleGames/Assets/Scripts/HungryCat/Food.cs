using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    
    private void Update() {
        transform.position += new Vector3(0, 1.5f, 0);
        if (transform.position.y > 26.0f) Destroy(gameObject);
    }
}
