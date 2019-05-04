using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundGenerator : MonoBehaviour {

    public ParallaxParameters[] parallaxParameters;
    public Vector2 origin;
    public Vector2 cameraSize; 
    private Parallax[] parallaxes;
    private Transform cameraXform;

    void Start() 
    {
        cameraXform = CameraController.Instance.transform;
        parallaxes = new Parallax[parallaxParameters.Length];
        for ( int parallaxIndex = 0; parallaxIndex < parallaxParameters.Length; parallaxIndex ++ ) {
            var parameter = parallaxParameters[parallaxIndex];
            parallaxes[parallaxIndex] = new Parallax(parameter, origin, cameraSize);
        }   
    }


    void Update() {
        for (int parallaxIndex = 0; parallaxIndex < parallaxParameters.Length; parallaxIndex++) {

            parallaxes[parallaxIndex].Refresh(CameraController.Instance.deltaPos, CameraController.Instance.rect, cameraXform.position);
        }
    }
}