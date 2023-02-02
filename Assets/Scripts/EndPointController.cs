using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointController : MonoBehaviour {

    public static EndPointController instance;

    public float constantForwardSpeed;
    public float zOverTime;
    public float angleLerpSpeed;

    // Start is called before the first frame update
    void Start() {

        if (!instance)
            instance = this;

    }

    // Update is called once per frame
    void Update() {

        transform.Translate(((Vector3.up * constantForwardSpeed) + (Vector3.forward * ControlsUI.instance.cursorMagnitude * zOverTime)) * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, ControlsUI.instance.pointingToAngle), angleLerpSpeed * Time.deltaTime);

    }

}