using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RootRenderer : MonoBehaviour {

    public static RootRenderer activeRoot;
    public static Stack<RootRenderer> rootStack = new Stack<RootRenderer>();

    public Transform endpointPosition;
    public float vertexDistance;
    public float maxRootLength;
    public float endpointWidthOffset;

    private LineRenderer renderer;
    private List<Vector3> rendererPoints;
    private AnimationCurve rendererWidth = new AnimationCurve();
    private float lineLength;
    private float lineTrailerLength;

    // Start is called before the first frame update
    void Start() {

        if (!activeRoot)
            activeRoot = this;
        else
            rootStack.Push(this);

        renderer = GetComponent<LineRenderer>();
        rendererPoints = new List<Vector3>();
        rendererPoints.Add(transform.position);
        rendererPoints.Add(endpointPosition.position);
        lineTrailerLength += Vector3.Distance(rendererPoints[0], rendererPoints[1]);
        rendererWidth = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(1, 1) });

    }

    // Update is called once per frame
    void Update() {

        if (Vector3.Distance(rendererPoints[rendererPoints.Count - 2], endpointPosition.position) > vertexDistance)
            AppendVertex();

        if (lineLength + lineTrailerLength > maxRootLength) {
            AppendVertex();
            ActivateNextRoot();
            CameraTracker.instance.Recenter();
            enabled = false;
            return;
        }

        rendererPoints[rendererPoints.Count - 1] = endpointPosition.position;
        renderer.SetPosition(renderer.positionCount - 1, endpointPosition.position);
        lineTrailerLength = Vector3.Distance(rendererPoints[rendererPoints.Count - 2], endpointPosition.position);

        rendererWidth.MoveKey(1, new Keyframe(1, (maxRootLength - lineLength - lineTrailerLength + endpointWidthOffset) / maxRootLength));
        renderer.widthCurve = rendererWidth;

        if (activeRoot != this)
            enabled = false;

        Debug.Log(rootStack.Count);

    }

    private void AppendVertex () {
        Debug.Log("Expanding Line Renderer Array");
        rendererPoints.Add(endpointPosition.position);
        renderer.positionCount = rendererPoints.Count;
        renderer.SetPositions(rendererPoints.ToArray());
        lineLength += lineTrailerLength;
    }

    private void ActivateNextRoot() {

        if (rootStack.Count > 0) {
            activeRoot = rootStack.Pop();
            activeRoot.enabled = true;
            EndPointController.instance.transform.position = activeRoot.rendererPoints[activeRoot.rendererPoints.Count - 1];
            ControlsUI.instance.Reset();
        } else
            activeRoot = null;

    }

}