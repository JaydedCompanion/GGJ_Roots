using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUtils : MonoBehaviour {

    private CanvasGroup group;
    private float fadeOutSpeed;
    private bool fadeOut;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (fadeOut = true && group)
            group.alpha -= Time.deltaTime * fadeOutSpeed;

    }

    public void StartGame() { GameManager.instance.StartGame(); }
    public void EnableOnClick(GameObject obj) { obj.SetActive(true); }
    public void DisableOnClick(GameObject obj) { obj.SetActive(false); }
    public void ToggleAbleOnClick(GameObject obj) { obj.SetActive(!obj.activeInHierarchy); }
    public void QuitGame() { Application.Quit(); }
    public void FadeOut(float fadeOutSpeed) { group = GetComponent<CanvasGroup>(); fadeOut = true; this.fadeOutSpeed = fadeOutSpeed; }

}
