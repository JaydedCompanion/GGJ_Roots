using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public static bool gameStarted;

    public static UnityEvent endEvent = new UnityEvent();
    public static UnityEvent restartEvent = new UnityEvent();

    public bool paused;
    public UIDigits lengthUIDisplay;
    public TMPro.TextMeshProUGUI newHighScore;
    public float highScoreSaturation;
    public float highScoreHueSpinSpeed;
    public float totalRootLength;
    public float lowestPoint;

    private float highScore;
    private GameObject pausedText;

    // Start is called before the first frame update
    void Start() {

        if (!instance) {
            //This code only runs if the game just started, and not if the game _re_started
            instance = this;
            gameStarted = false;
        } else if (instance != this) {
            Destroy(gameObject);
            GameObject.Find("Canvas.MainMenu").SetActive(false);
        }

        DontDestroyOnLoad(gameObject);

        highScore = PlayerPrefs.GetInt("highScore", 0);

        endEvent.AddListener(EvaluateNewHighScore);
        restartEvent.AddListener(() => RootRenderer.rootStack.Clear());
        restartEvent.AddListener(() => PickupBonemeal.pickupBonemealStack.Clear());

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);

        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine("ScreenshotCoroutine");
        }

        newHighScore.color = (totalRootLength > highScore) ? Color.HSVToRGB((Time.time * highScoreHueSpinSpeed) % 1f, highScoreSaturation, 1) : Color.white;

        newHighScore.text = (totalRootLength > highScore) ? "New High Score!" : "Best: " + highScore + "cm";

        lengthUIDisplay.value = totalRootLength;

        if (gameStarted && Input.GetButtonDown("Cancel"))
            TogglePause();

        if (!pausedText)
            pausedText = GameObject.Find("Text.Paused");
        pausedText.SetActive(paused);
        Time.timeScale = (paused || !gameStarted) ? 0 : 1;

    }

    public void StartGame() {
        gameStarted = true;
        paused = false;
    }

    void EvaluateNewHighScore() {
        if (totalRootLength > highScore) {
            PlayerPrefs.SetInt("highScore", Mathf.RoundToInt (totalRootLength));
            PlayerPrefs.Save();
        }
    }

    public void TogglePause () {
        paused = !paused;
    }

    public IEnumerator ScreenshotCoroutine() {

        GameObject canvas = null;
        if (GameObject.Find("Canvas"))
            canvas = GameObject.Find("Canvas");
        ControlsUI.instance.gameObject.SetActive(false);
        if (canvas)
            canvas.SetActive(false);
        yield return null;

        ScreenCapture.CaptureScreenshot(System.DateTime.Now.ToLongTimeString().Replace('/', '.') + " - " + totalRootLength + "cm.png");
        yield return null;

        ControlsUI.instance.gameObject.SetActive(true);
        if (canvas)
            canvas.SetActive(true);

    }

}