using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {
    static LevelManager instance = null;
    public static LevelManager Instance {
        get {
            return instance;
        }
    }

    void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else if (instance == null) {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadLevel(string name){
        SceneManager.LoadScene (name);

    }

	public void QuitRequest(){
		Application.Quit ();
	}

}
