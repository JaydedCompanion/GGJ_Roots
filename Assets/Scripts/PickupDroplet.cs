using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDroplet : Pickup {

    public AnimationCurve disappearAnim;

    public float lengthAdd;
    public float animStartTime;

    private MeshRenderer renderer;
    private float startTime;
    private bool activated;

    // Start is called before the first frame update
    void Start() {

        renderer = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update() {

        if (!activated)
            startTime = Time.time;

        renderer.material.SetFloat("_Disappear", disappearAnim.Evaluate(Time.time - startTime));

    }

    public override void Activate() {
        if (!RootRenderer.activeRoot || activated)
            return;
        activated = true;
        GetComponent<Collider2D>().enabled = false;
        RootRenderer.activeRoot.Extend(lengthAdd);
    }

}