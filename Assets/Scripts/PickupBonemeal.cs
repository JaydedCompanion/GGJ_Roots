using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBonemeal : Pickup {

    public float spinSpeed;

    private ParticleSystem particles;
    private ParticleSystem.LimitVelocityOverLifetimeModule particlesLimitVel;
    private MeshRenderer renderer;
    private RootRenderer spawnedBy;
    private RootRenderer spawnedRoot;
    private Vector3 activatedPos;
    private float lengthWhenActivated;
    private bool activated;

    // Start is called before the first frame update
    void Start() {

        particles = GetComponentInChildren<ParticleSystem>();
        particlesLimitVel = particles.limitVelocityOverLifetime;
        renderer = GetComponentInChildren<MeshRenderer>();

    }

    // Update is called once per frame
    void Update() {

        renderer.transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);

        if (activated)
            transform.position = Vector3.Lerp(transform.position, activatedPos, Time.deltaTime);

    }

    public void RootEnabled () {
        particlesLimitVel.enabled = false;
        spawnedRoot.maxRootLength = spawnedBy.maxRootLength;
        spawnedRoot.lineLength = spawnedRoot.maxRootLength * ((lengthWhenActivated / spawnedBy.maxRootLength));
        spawnedRoot.baseRadius = spawnedBy.baseRadius * (1 - (lengthWhenActivated / spawnedBy.maxRootLength));
    }

    public override void Activate() {

        if (activated)
            return;

        activated = true;
        GetComponent<Collider2D>().enabled = false;

        GameObject rootPrefab = Resources.Load("RootRenderer") as GameObject;
        spawnedRoot = Instantiate(rootPrefab, EndPointController.instance.transform.position, Quaternion.identity).GetComponent<RootRenderer>();
        spawnedRoot.enabled = false;
        if (!RootRenderer.rootStack.Contains(spawnedRoot))
            RootRenderer.rootStack.Push(spawnedRoot);

        spawnedBy = RootRenderer.activeRoot;
        spawnedBy.deathEvent.AddListener(RootEnabled);

        activatedPos = spawnedRoot.transform.position;
        lengthWhenActivated = RootRenderer.activeRoot.currentRootLength;
        particles.Play();
        renderer.enabled = false;

    }

}