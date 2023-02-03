using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointController : MonoBehaviour {

    public static EndPointController instance;

    public float constantForwardSpeed;
    public float zOverTime;
    public float zNoiseSpeed;
    public float angleLerpSpeed;

    // Start is called before the first frame update
    void Start() {

        if (!instance)
            instance = this;

    }

    // Update is called once per frame
    void Update() {

        transform.Translate(((Vector3.up * constantForwardSpeed) + (Vector3.forward * (Mathf.Pow (Mathf.PerlinNoise(Time.time * zNoiseSpeed, 0), 2) - 0.25f) * zOverTime)) * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, ControlsUI.instance.pointingToAngle), angleLerpSpeed * Time.deltaTime);

    }

}