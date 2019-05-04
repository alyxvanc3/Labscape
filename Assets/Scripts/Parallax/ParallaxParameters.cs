using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ParallaxParameters   
{
    public string[] objectTags;
    public Vector2 objectSize;
    public float multiplier;

    public ParallaxParameters( string[] objectTags, Vector2 objectSize, float multiplier ) {
        this.multiplier = multiplier;
        this.objectSize = objectSize;
        this.objectTags = objectTags;
    }
    
}
