using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBonemeal : Pickup {

    public static Stack<PickupBonemeal> pickupBonemealStack = new Stack<PickupBonemeal>();

    public float spinSpeed;
    public ParticleSystem particles;
    public ParticleSystem particlesActivate;

    private MeshRenderer renderer;
    private RootRenderer spawnedBy;
    private RootRenderer spawnedRoot;
    private Vector3 activatedPos;
    private Vector2 controllerLocalPos;
    private Vector2 controllerVel;
    private float maxLengthWhenActivated;
    private float lengthWhenActivated;
    private float relativeLengthWhenActivated;
    private bool activated;

    // Start is called before the first frame update
    void Start() {
        renderer = GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update() {

        renderer.transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);

        if (activated)
            transform.position = Vector3.Lerp(transform.position, activatedPos, Time.deltaTime);

        base.Update();

    }

    public void RootEnabled () {
        particles.gameObject.SetActive(false);
        particlesActivate.Play();
        //spawnedRoot.maxRootLength = lengthWhenActivated;
        spawnedRoot.maxRootLength = spawnedBy.maxRootLength;
        spawnedRoot.lineLength = spawnedRoot.maxRootLength * ((lengthWhenActivated / spawnedBy.maxRootLength));
        //spawnedRoot.lineLength = spawnedRoot.maxRootLength * relativeLengthWhenActivated;
        spawnedRoot.baseRadius = spawnedBy.baseRadius * (1 - (lengthWhenActivated / spawnedBy.maxRootLength));
        ControlsUI.instance.sCursor.transform.localPosition = controllerLocalPos;
        ControlsUI.instance.cursorRB.velocity = controllerVel;
    }

    public override void Activate(RootRenderer activatedBy) {

        if (activated)
            return;

        activated = true;
        GetComponent<Collider2D>().enabled = false;
        pickupBonemealStack.Push(this);

        GameObject rootPrefab = Resources.Load("RootRenderer") as GameObject;
        spawnedRoot = Instantiate(rootPrefab, EndPointController.instance.transform.position, Quaternion.identity).GetComponent<RootRenderer>();
        spawnedRoot.enabled = false;
        if (!RootRenderer.rootStack.Contains(spawnedRoot))
            RootRenderer.rootStack.Push(spawnedRoot);

        spawnedBy = activatedBy;

        controllerLocalPos = ControlsUI.instance.sCursor.transform.localPosition;
        controllerVel = ControlsUI.instance.cursorRB.velocity;

        activatedPos = spawnedRoot.transform.position;
        lengthWhenActivated = activatedBy.currentRootLength;
        relativeLengthWhenActivated = activatedBy.currentRootLength / activatedBy.maxRootLength;
        particles.Play();
        renderer.enabled = false;

    }

}