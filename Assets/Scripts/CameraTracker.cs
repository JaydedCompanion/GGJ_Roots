using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour {

    public static CameraTracker instance;

    public bool endView;
    public float endViewExtraSize;
    public float lerpSpeed;
    public float recenterLerpSpeed;
    public float lookahead;
    public float camSizeLerpSpeed;

    private float defaultLerpSpeed;
    private float defaultCameraSize;

    // Start is called before the first frame update
    void Start() {

        if (!instance)
            instance = this;

        defaultLerpSpeed = lerpSpeed;
        defaultCameraSize = Camera.main.orthographicSize;

    }

    // Update is called once per frame
    void LateUpdate() {

        Vector3 lookAt = Vector3.zero;

        if (endView) {
            //I couldn't be bothered to figure out whether I'm supposed to use Vec2.up or Vec2.down so just gonna use Mathf.Abs :V don't @ me
            lookAt = Vector2.down * Mathf.Abs (GameManager.instance.lowestPoint / 2);
        } else {
            lookAt = EndPointController.instance.transform.position + (EndPointController.instance.transform.up * lookahead);
        }

        lookAt.z = -100;

        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, endView ? -GameManager.instance.lowestPoint / 2 + endViewExtraSize : defaultCameraSize, camSizeLerpSpeed * Time.deltaTime);

        lerpSpeed = Mathf.Lerp(lerpSpeed, defaultLerpSpeed, Time.deltaTime * 0.5f);

        transform.position = Vector3.Lerp(transform.position, lookAt, lerpSpeed * Time.deltaTime);

    }

    public void Recenter() {

        lerpSpeed = recenterLerpSpeed;

    }

}