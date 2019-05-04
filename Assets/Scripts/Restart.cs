using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {
    public GameObject gameOver;
    public Text sessionScore;
    public Text highScore;
    string scoreString;
    int best;
    int score = 0;
    public Animator anim;

    static Restart instance = null;
    public static Restart Instance {
        get {
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else if (instance == null) {
            instance = this;
        }
    }

    public void Replay() {
        anim.SetTrigger("died");
        scoreString = InputManager.Instance.GetText();
        int.TryParse(scoreString, out score);
        if (score > best) {
            PlayerPrefs.SetInt("Highscore", score);
        }
        best = PlayerPrefs.GetInt("Highscore", 0);

        sessionScore.text = scoreString;
        highScore.text = best.ToString();

        gameOver.SetActive(true);
        GameController.Instance.gameMenu.SetActive(false);
    }

}
