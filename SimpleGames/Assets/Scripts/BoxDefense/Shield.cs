using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    
    public Transform targetObject;
    public float desiredDistance = 1.5f;

    private void Update() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
	    
        float currentDistance = Vector3.Distance(transform.position, targetObject.position);

        // 거리 조절
        if (Mathf.Abs(currentDistance - desiredDistance) > 0.01f) {
            Vector3 direction = (targetObject.position - transform.position).normalized;
            transform.position = targetObject.position - direction * desiredDistance;
        }
    }
}
