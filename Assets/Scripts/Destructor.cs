using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour {

	void Update () {
		if(transform.position.x < (CameraController.Instance.cameraTransform.position.x - 80f)) {
            gameObject.SetActive(false);
        }
	}
}
