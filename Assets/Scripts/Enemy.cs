using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private readonly float speed = 10f;
    private Transform enemyXform;

    private void Start() {
        enemyXform = transform;
    }

    private void Update() {
        if (enemyXform.position.x - CameraController.Instance.transform.position.x < 45f && GameController.Instance.paused == false) {
            enemyXform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}