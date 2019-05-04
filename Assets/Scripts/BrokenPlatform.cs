using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPlatform : MonoBehaviour {
    bool pooled = false;


    void OnEnable() {
        // we dont want to create an enemy on first OnEnable call, because it also gets enabled while creating the pool.
        if(pooled == false) {
            pooled = true;
        }
        else {
            GameObject enemy = ObjectPooler.Instance.GetPooledObject("Enemy");
            if (enemy != null) {
                enemy.transform.position = transform.position + Vector3.up * 8f + Vector3.right * 18f;
                enemy.transform.rotation = Quaternion.identity;
                enemy.SetActive(true);
            }
        }
    }

	void OnCollisionEnter2D (Collision2D col)
    {
        col.transform.position += Vector3.right * 0.5f;
        Restart.Instance.Replay();
    }

}
