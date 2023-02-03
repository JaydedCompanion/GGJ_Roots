using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour {

    public static CameraTracker instance;

    public float lerpSpeed;
    public float recenterLerpSpeed;
    public float lookahead;

    private float defaultLerpSpeed;

    // Start is called before the first frame update
    void Start() {

        if (!instance)
            instance = this;

        defaultLerpSpeed = lerpSpeed;

    }

    // Update is called once per frame
    void LateUpdate() {

        Vector3 lookAt = EndPointController.instance.transform.position + (EndPointController.instance.transform.up * lookahead);
        lookAt.z = -100;

        lerpSpeed = Mathf.Lerp(lerpSpeed, defaultLerpSpeed, Time.deltaTime * 0.5f);

        transform.position = Vector3.Lerp(transform.position, lookAt, lerpSpeed * Time.deltaTime);

    }

    public void Recenter() {

        lerpSpeed = recenterLerpSpeed;

    }

}