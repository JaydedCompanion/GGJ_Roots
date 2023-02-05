using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointController : MonoBehaviour {

    public static EndPointController instance;
    public Material rootMat;

    public float constantForwardSpeed;
    public float zOverTime;
    public float zNoiseSpeed;
    public float angleLerpSpeed;

    private CircleCollider2D coll;

    // Start is called before the first frame update
    void Start() {

        if (!instance)
            instance = this;

        coll = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update() {

        transform.Translate(((Vector3.up * constantForwardSpeed) + (Vector3.forward * (Mathf.Pow (Mathf.PerlinNoise(Time.time * zNoiseSpeed, 0), 2) - 0.25f) * zOverTime)) * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, ControlsUI.instance.pointingToAngle), angleLerpSpeed * Time.deltaTime);

        if (RootRenderer.activeRoot) {
            coll.radius = RootRenderer.activeRoot.currentRadius;
            if (transform.position.y < GameManager.instance.lowestPoint)
                GameManager.instance.lowestPoint = transform.position.y;
        }

        rootMat.SetVector("_Cursor_Position", transform.position);

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!RootRenderer.activeRoot)
            return;
        if (collision.GetComponent<Pickup>())
            collision.GetComponent<Pickup>().Activate(RootRenderer.activeRoot);
    }

}