using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax {
    private List<Transform> parallaxObjects;

    private ParallaxParameters parameters;
    private float creationOffset;
    private Vector2 origin;
    //GameObject[] sourcePrefabs
    public Parallax (  ParallaxParameters parameters, Vector2 origin, Vector2 cameraSize ) 
    {
        parallaxObjects = new List<Transform>();
        this.parameters = parameters;
        this.origin = Vector2.zero + origin;
        creationOffset = origin.x;
        int cols = Mathf.CeilToInt(cameraSize.x / parameters.objectSize.x) + 1;

        for (int creationIndex = 0; creationIndex < cols; creationIndex++) {

            CreateParalaxObject();
        }
    }

    private void CreateParalaxObject() 
    {

        var parallaxObject = ObjectPooler.Instance.GetPooledObject(GetNextTag());
        creationOffset += parameters.objectSize.x*1.75f;
        Vector2 creationPosition = new Vector2(creationOffset, origin.y);
        var parallaxXform = parallaxObject.transform;
        parallaxXform.position = creationPosition;
        parallaxObjects.Add(parallaxXform);
        parallaxObject.SetActive(true);

    }

    private string GetNextTag() {
        return parameters.objectTags[0];
    }

    public void Refresh ( Vector2 cameraDelta, Rect cameraRect, Vector2 cameraPosition ) 
    {
     
        for( int objectIndex = parallaxObjects.Count-1; objectIndex >= 0; objectIndex -- ) {
            var xform = parallaxObjects[objectIndex];
            xform.position -= (Vector3) cameraDelta * parameters.multiplier;

            var limitPoint = xform.position + parameters.objectSize.x * Vector3.right;

            if ( cameraPosition.x > xform.position.x + (parameters.objectSize.x*2.6f)  && !cameraRect.Contains(limitPoint)) {
                xform.gameObject.SetActive(false);
                parallaxObjects.Remove(xform);
                CreateParalaxObject();
            }
        }
    }
}