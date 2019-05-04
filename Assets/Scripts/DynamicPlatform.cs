using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatform : MonoBehaviour {
    int counter = 0;
    int selection;
    bool movingRight;
    readonly float speed = 4f;
    private Transform dynamicXform;

    private void Start() {
        dynamicXform = transform;
        selection = Random.Range(0, 2);
        if(selection == 0) {
            movingRight = true;
        }
        if(selection > 0 ) {
            movingRight = false;
        }
    }

    void FixedUpdate () {
        MoveAround();
    }

    public void MoveAround() {
        if (movingRight) {
            dynamicXform.position += Vector3.right * speed * Time.deltaTime;
            if (counter == 30) {
                movingRight = false;
                counter = 0;
            }
            counter++;
        }
        else {
            dynamicXform.position += Vector3.left * speed * Time.deltaTime;
            if (counter == 30) {
                movingRight = true;
                counter = 0;
            }
            counter++;
        }
    }
}