using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class InputManager : MonoBehaviour {

	public GameObject mobilePrefab;
	public GameObject mousePrefab;
    public Camera cam;
	private Controller controller;

    public Text text;
    string high;
	
	static InputManager instance = null;
	public static InputManager Instance {
		get {
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
	
	public void RegisterControllable(IControllable co) {

		GameObject go;

#if UNITY_IOS || UNITY_ANDROID
		go = Instantiate(mobilePrefab) as GameObject;
        Application.targetFrameRate = 60;

#else
		go = Instantiate(mousePrefab) as GameObject;
#endif
        
        go.transform.parent = cam.transform;
        controller = go.GetComponent<Controller>();
		controller.Initialize(co);
       
	}


    public void SetText(int score) {
        text.text = "Score : " + score.ToString();
    }

    public string GetText() {
        high = Regex.Match(text.text, @"\d+").Value;
        return high;
    }

    public void CleanText() {
        text.text = "Score : 0";
    }
}
