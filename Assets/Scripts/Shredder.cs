using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {
    Transform t;

    void OnTriggerEnter2D (Collider2D col) {
        t = col.transform;
        while (t.parent != null) {
            if(t.parent.tag == "Platforms" || t.parent.tag == "MiddleGrounds" || 
                                                t.parent.tag == "ForeGrounds" || t.parent.tag == "BackGrounds") {
                t.gameObject.SetActive(false);
                break;
            }
            t = t.parent.transform;   
        }       
    }
}