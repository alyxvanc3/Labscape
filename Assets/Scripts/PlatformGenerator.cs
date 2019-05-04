using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {
	public GameObject platformGenerationPoint;
	
	public float randomMiddle;
	public float randomDistance;
	
	Vector3 GenerationPos;
    Quaternion GenerationRot;

    private Transform generatorXform;
    float generationPointX;
    int selection;
    enum PlatformTypes {
        Prefab1 = 0,
        Prefab2 = 1,
        Prefab3 = 2,
        Prefab4 = 3,
        Prefab5 = 4
    }

    private void Start() {
        generatorXform = transform;
    }


    void FixedUpdate () {

        generationPointX = platformGenerationPoint.transform.position.x;

        if (generatorXform.position.x < generationPointX) {
		
            selection = Random.Range(0,5);

            //position for pooled platform
            GenerationPos = generatorXform.position;
            GenerationRot = Quaternion.identity;
            
            GameObject platform = ObjectPooler.Instance.GetPooledObject(((PlatformTypes)selection).ToString());
            if (platform != null) {
                platform.transform.position = GenerationPos;
                platform.transform.rotation = GenerationRot;
                platform.SetActive(true);
            }

            //position for platform generator
            generatorXform.position += Vector3.right * 50f;
        }
	}
}