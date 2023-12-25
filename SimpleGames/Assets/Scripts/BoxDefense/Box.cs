using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Box : MonoBehaviour {

    public Sprite[] boxSpirtes;

    void Start() {
        float x = Random.Range(-2.5f, 2.5f);
        float y = Random.Range(4.0f, 5.0f);

        transform.position = new Vector3(x, y, 0);

        this.gameObject.GetComponent<SpriteRenderer>().sprite = boxSpirtes[Random.Range(0, 5)];
        float size = Random.Range(0.7f, 1.4f);
        transform.localScale = new Vector3(size, size, 1);
    }

    void Update() {
        if(transform.position.y < -5.0f) Destroy(gameObject); 
    }
}
