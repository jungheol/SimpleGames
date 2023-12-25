using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatType {
    normalCat = 0,
    fatCat,
    pirateCat
}
public class Cat : MonoBehaviour {
    
    private float full = 5.0f;
    private float energy = 0.0f;
    private int score;
    private bool isFull = false;
    public CatType type;
    
    private void Start() {
        float x = Random.Range(-8.5f, 8.5f);
        float y = 30.0f;
        transform.position = new Vector3(x, y, 0);
        score = 1;
        
        if (type == CatType.fatCat) {
            full = 10.0f;
            score = 3;
        } else if (type == CatType.pirateCat) {
            score = 5;
        }
    }

    private void FixedUpdate() {
        if (energy < full) {
            if (type == CatType.normalCat) {
                transform.position += new Vector3(0.0f, -0.1f, 0.0f);
            } else if (type == CatType.fatCat) {
                transform.position += new Vector3(0.0f, -0.05f, 0.0f);
            } else if (type == CatType.pirateCat) {
                transform.position += new Vector3(0.0f, -0.2f, 0.0f);
            }

            if (transform.position.y < -16.0f) HungryCatManager.instance.GameOver();
            
        } else {
            if (transform.position.x > 0) {
                transform.position += new Vector3(0.05f, 0.0f, 0.0f);
            } else {
                transform.position += new Vector3(-0.05f, 0.0f, 0.0f);
            }
            Destroy(gameObject, 3.0f);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Food") {
            if (energy < full) {
                energy += 1.0f;
                Destroy(coll.gameObject);
                gameObject.transform.Find("Hungry/Canvas/Front").transform.localScale = new Vector3(energy / full, 1.0f, 1.0f);
            } else {
                if (isFull == false) {
                    HungryCatManager.instance.AddCat(score);
                    gameObject.transform.Find("Hungry").gameObject.SetActive(false);  
                    gameObject.transform.Find("Full").gameObject.SetActive(true);
                    isFull = true;
                }
            }
        }
    }
}
