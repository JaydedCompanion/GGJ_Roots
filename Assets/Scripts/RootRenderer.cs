using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TubeRenderer))]
public class RootRenderer : MonoBehaviour {

    public static RootRenderer activeRoot;
    public static Stack<RootRenderer> rootStack = new Stack<RootRenderer>();

    public UnityEvent deathEvent = new UnityEvent();
    public Transform endpointPosition;
    public float baseRadius;
    public float currentRadius;
    public float radiusSmoothingSpeed;
    public float vertexDistance;
    public float currentRootLength;
    public float maxRootLength;
    public float endpointWidthOffset;

    private TubeRenderer renderer;
    private List<Vector3> rendererPoints;
    private AnimationCurve rendererWidth = new AnimationCurve();
    private Transform endpointSphere;
    private float lineLength;
    private float lineTrailerLength;

    // Start is called before the first frame update
    void Start() {

        if (!activeRoot)
            activeRoot = this;
        else if (activeRoot != this && !rootStack.Contains(this)) {
            rootStack.Push(this);
            enabled = false;
        }

        if (!endpointPosition && EndPointController.instance)
            endpointPosition = EndPointController.instance.transform;

        renderer = GetComponent<TubeRenderer>();
        rendererPoints = new List<Vector3>();
        rendererPoints.Add(transform.position);
        rendererPoints.Add(endpointPosition.position);
        lineTrailerLength += Vector3.Distance(rendererPoints[0], rendererPoints[1]);
        endpointSphere = transform.GetChild(0);

    }

    // Update is called once per frame
    void Update() {

        if (!endpointPosition && EndPointController.instance)
            endpointPosition = EndPointController.instance.transform;

        if (Vector3.Distance(rendererPoints[rendererPoints.Count - 2], endpointPosition.position) > vertexDistance)
            AppendVertex();

        currentRootLength = lineLength + lineTrailerLength;
        if (currentRootLength > maxRootLength) {
            //AppendVertex();
            deathEvent.Invoke();
            ActivateNextRoot();
            return;
        }

        currentRadius = Mathf.Lerp (currentRadius, ((maxRootLength - lineLength - lineTrailerLength + endpointWidthOffset) / maxRootLength) * baseRadius, radiusSmoothingSpeed * Time.deltaTime);

        renderer._radiusOne = baseRadius;
        renderer._radiusTwo = currentRadius;

        rendererPoints[rendererPoints.Count - 1] = endpointPosition.position;
        renderer.SetPositions(rendererPoints.ToArray());
        endpointSphere.position = endpointPosition.position;
        endpointSphere.localScale = Vector3.one * renderer._radiusTwo * 2;
        lineTrailerLength = Vector3.Distance(rendererPoints[rendererPoints.Count - 2], endpointPosition.position);

    }

    private void AppendVertex () {
        rendererPoints.Add(endpointPosition.position);
        renderer.SetPositions(rendererPoints.ToArray());
        lineLength += lineTrailerLength;
    }

    private void ActivateNextRoot() {

        endpointSphere.position = rendererPoints[rendererPoints.Count - 1];
        endpointSphere.localScale = Vector3.one * renderer._radiusTwo * 2;
        enabled = false;
        CameraTracker.instance.Recenter();

        if (rootStack.Count > 0) {
            activeRoot = rootStack.Pop();
            activeRoot.enabled = true;
            activeRoot.Start();
            EndPointController.instance.transform.position = activeRoot.rendererPoints[activeRoot.rendererPoints.Count - 1];
            ControlsUI.instance.Reset();
        } else
            activeRoot = null;

    }

    public void Extend(float amount) {

        maxRootLength += amount;

    }

}