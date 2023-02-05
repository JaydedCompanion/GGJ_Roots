using System.Collections;
using UnityEngine;

public abstract class Pickup: MonoBehaviour {

    abstract public void Activate(RootRenderer activatedBy);

    public float staggerAppear;

    private float deathTime = -1;

    protected void Update() {
        float scaleTo = 0;
        if (!RootRenderer.activeRoot) {
            if (deathTime <= float.Epsilon)
                deathTime = Time.time;
            if (transform.localScale.y <= float.Epsilon) {
                gameObject.SetActive(false);
                enabled = false;
            }
            if ((Time.time - deathTime) * staggerAppear > -transform.position.y)
                scaleTo = 0;
        } else {
            if (Time.time * staggerAppear > -transform.position.y)
                scaleTo = 1;
            deathTime = -1;
        }
        transform.localScale = Vector3.one * Mathf.Lerp(transform.localScale.y, scaleTo, Time.deltaTime * 4);
    }

}