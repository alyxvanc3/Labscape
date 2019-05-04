using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	Player player;
    public Text text;
    int highscore;
    public bool paused = false;
    public GameObject pause;
    public GameObject gameMenu;
    Vector2 oldVelocity;

    static GameController instance = null;
    public static GameController Instance {
        get {
            return instance;
        }
    }

    void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else if (instance == null) {
            instance = this;
        }
    }

    void SpawnPlayer1() {
		GameObject p = Instantiate(Resources.Load("Player",typeof(GameObject))) as GameObject;
		
		player = p.GetComponentInChildren<Player>();
		player.Initialize();
	}	

	void Start() {
        InputManager.Instance.CleanText();
        highscore = PlayerPrefs.GetInt("Highscore",0);
        text.text = "High Score : " + highscore.ToString();
		SpawnPlayer1();
		InputManager.Instance.RegisterControllable(player);
		CameraController.Instance.MyControllable = player;
        pause.SetActive(false);
        Restart.Instance.gameOver.SetActive(false);
        gameMenu.SetActive(true);
    }
	
    public void PauseGame() {
        paused = !paused;
        if (paused) {
            pause.SetActive(true);
            gameMenu.SetActive(false);
            player.GetComponent<Rigidbody2D>().isKinematic = true;
            oldVelocity = player.GetComponent<Rigidbody2D>().velocity;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else {
            pause.SetActive(false);
            gameMenu.SetActive(true);
            player.GetComponent<Rigidbody2D>().isKinematic = false;
            player.GetComponent<Rigidbody2D>().velocity = oldVelocity;
        }
    }

	
}
