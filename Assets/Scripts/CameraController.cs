using UnityEngine;
using System;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Rect rect;
    public Vector3 deltaPos;

    private Vector3 lastPos;
    private bool isFirstUpdate = true;
    [SerializeField]
    private float smoothSpeed = 0.125f;
    [SerializeField]
    private bool smooth = true;

    public Transform cameraTransform;

    public IControllable MyControllable {get;set;}
	
	static CameraController instance = null;
	public static CameraController Instance{
		get{
			return instance;
		}
	}

    void Awake() {
		if(instance != null && instance != this)
			Destroy(gameObject);
		else if(instance == null){
			instance = this;
		}
	}

    private void Start() {
        cameraTransform = transform;
        Vector3 leftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        rect = new Rect(cameraTransform.position.x, cameraTransform.position.y, (rightTop - leftBottom).x, (rightTop - leftBottom).y);
        
    }

    void FixedUpdate() {
        Vector3 desiredPosition = new Vector3(MyControllable.Position.x + 8f, 7.74f, -10f);
        if (smooth) {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
        else {
            cameraTransform.position = desiredPosition;
        }

        rect.position = transform.position;

        if (isFirstUpdate) {
            isFirstUpdate = false;
            lastPos = cameraTransform.position;
        }
        else {
            deltaPos = cameraTransform.position - lastPos;
            lastPos = cameraTransform.position;
        }
		
	}
}
