using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem {
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand = true;
}

public class ObjectPooler : MonoBehaviour {
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;
    public GameObject platforms;
    public GameObject backgrounds;
    public GameObject middlegrounds;
    public GameObject foregrounds;

    public static ObjectPooler instance = null;
    public static ObjectPooler Instance {
        get {
            return instance;
        }
    }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else if (instance == null) {
            instance = this;
            //GameObject.DontDestroyOnLoad(gameObject);
            pooledObjects = new List<GameObject>();
            foreach (ObjectPoolItem item in itemsToPool) {
                for (int i = 0; i < item.amountToPool; i++) {
                    GameObject obj = Instantiate(item.objectToPool) as GameObject;
                    obj.SetActive(false);
                    pooledObjects.Add(obj);

                    //to maintain hierarchy
                    if (obj.tag == "Background") {
                        obj.transform.parent = backgrounds.transform;
                    }
                    else if (obj.tag == "Middleground") {
                        obj.transform.parent = middlegrounds.transform;
                    }
                    else if (obj.tag == "Foreground") {
                        obj.transform.parent = foregrounds.transform;
                    }
                    else {
                        obj.transform.parent = platforms.transform;
                    }
                }
            }
        }
    }




    public GameObject GetPooledObject (string tag) {
        for (int i = 0; i < pooledObjects.Count; i++) {
            if(!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag) {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool) {
            if (item.objectToPool.tag == tag) {
                if (item.shouldExpand) {
                    GameObject obj = Instantiate(item.objectToPool) as GameObject;
                    obj.SetActive(true);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
       
    }
}
