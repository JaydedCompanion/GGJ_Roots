using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public static UnityEvent endEvent = new UnityEvent();

    public UIDigits lengthUIDisplay;
    public TMPro.TextMeshProUGUI newHighScore;
    public float highScoreSaturation;
    public float highScoreHueSpinSpeed;
    public float totalRootLength;
    public float lowestPoint;

    private float highScore;

    // Start is called before the first frame update
    void Start() {

        if (!instance)
            instance = this;

        highScore = PlayerPrefs.GetInt("highScore", 0);

        endEvent.AddListener(EvaluateNewHighScore);

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);

        newHighScore.color = (totalRootLength > highScore) ? Color.HSVToRGB((Time.time * highScoreHueSpinSpeed) % 1f, highScoreSaturation, 1) : Color.white;

        newHighScore.text = (totalRootLength > highScore) ? "New High Score!" : "Best: " + highScore + "cm";

        lengthUIDisplay.value = totalRootLength;

    }

    void EvaluateNewHighScore() {
        if (totalRootLength > highScore) {
            PlayerPrefs.SetInt("highScore", Mathf.RoundToInt (totalRootLength));
            PlayerPrefs.Save();
        }
    }

}