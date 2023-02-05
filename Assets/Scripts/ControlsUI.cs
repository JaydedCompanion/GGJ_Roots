using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ControlsUI : MonoBehaviour {

    public static ControlsUI instance;

    [Header("Components")]
    public Shapes.Disc sOuterRadius;
    public Shapes.Disc sInnerRadius;
    public Shapes.Disc sCursor;
    public Shapes.Line sConnector;
    public Shapes.Polyline sArrowUp;
    public Shapes.Polyline sArrowDn;
    public Shapes.Polyline sArrowLt;
    public Shapes.Polyline sArrowRt;
    public Collider2D collOuterRadius;
    [Header("Status")]
    public Vector2 velocity;
    public Vector2 pointingTo;
    public float pointingToAngle;
    public float cursorMagnitude;
    [Header("Movement Parameters")]
    public float speedAccel;
    public float speedMin;
    public float speedMax;
    public float underSpeedAccel;
    public float perlinForce;
    public float perlinSpeed;
    [Header("Area Parameters")]
    public float cursorRadius;
    public float radiusMin = 1;
    public float radiusMax = 2;
    [Header("Arrow Parameters")]
    public float arrowResetSpeed;
    public float arrowBumpDistance;

    public Rigidbody2D cursorRB { get; private set; }
    private CircleCollider2D collCursor;
    private CircleCollider2D collInnerRadius;
    private Vector3 arrowLocalPosUp;
    private Vector3 arrowLocalPosDn;
    private Vector3 arrowLocalPosLt;
    private Vector3 arrowLocalPosRt;
    private bool axisInputLockHori;
    private bool axisInputLockVert;

    // Start is called before the first frame update
    void Start() {

        if (!instance)
            instance = this;

        cursorRB = sCursor.GetComponent<Rigidbody2D>();
        collCursor = sCursor.GetComponent<CircleCollider2D>();
        collInnerRadius = sInnerRadius.GetComponent<CircleCollider2D>();

        //cursorRB.velocity = Vector2.down;
        arrowLocalPosUp = sArrowUp.transform.localPosition;
        arrowLocalPosDn = sArrowDn.transform.localPosition;
        arrowLocalPosLt = sArrowLt.transform.localPosition;
        arrowLocalPosRt = sArrowRt.transform.localPosition;

        SetUpUI();

    }

    // Update is called once per frame
    void Update() {

        if (!Application.isPlaying)
            SetUpUI();
        else
            transform.localScale = Vector3.Lerp(transform.localScale, CameraTracker.instance.endView ? Vector3.zero : Vector3.one, Time.deltaTime);

        pointingTo = sCursor.transform.localPosition.normalized;
        pointingToAngle = Vector2.SignedAngle(Vector2.up, sCursor.transform.localPosition);

        cursorMagnitude = (Vector2.Distance(transform.position, sCursor.transform.position) - cursorRadius);
        sConnector.End = Vector3.down * cursorMagnitude;
        sConnector.transform.localRotation = Quaternion.Euler(0, 0, pointingToAngle + 180);
        sInnerRadius.transform.localRotation = Quaternion.Euler(0, 0, pointingToAngle + 180);

        collOuterRadius.transform.localPosition = sCursor.transform.localPosition.normalized * radiusMax;
        collOuterRadius.transform.localRotation = Quaternion.Euler(0, 0, pointingToAngle);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            sArrowUp.transform.localPosition = arrowLocalPosUp * arrowBumpDistance;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            sArrowDn.transform.localPosition = arrowLocalPosDn * arrowBumpDistance;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            sArrowLt.transform.localPosition = arrowLocalPosLt * arrowBumpDistance;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            sArrowRt.transform.localPosition = arrowLocalPosRt * arrowBumpDistance;

        sArrowUp.transform.localPosition = Vector3.Lerp(sArrowUp.transform.localPosition, arrowLocalPosUp, Time.deltaTime * arrowResetSpeed);
        sArrowDn.transform.localPosition = Vector3.Lerp(sArrowDn.transform.localPosition, arrowLocalPosDn, Time.deltaTime * arrowResetSpeed);
        sArrowLt.transform.localPosition = Vector3.Lerp(sArrowLt.transform.localPosition, arrowLocalPosLt, Time.deltaTime * arrowResetSpeed);
        sArrowRt.transform.localPosition = Vector3.Lerp(sArrowRt.transform.localPosition, arrowLocalPosRt, Time.deltaTime * arrowResetSpeed);

    }

    private void FixedUpdate() {

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f) {
            if (!axisInputLockHori)
                cursorRB.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), 0) * speedAccel);
            axisInputLockHori = true;
        } else
            axisInputLockHori = false;

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f) {
            if (!axisInputLockVert)
                cursorRB.AddForce(new Vector2(0, Input.GetAxisRaw("Vertical")) * speedAccel);
            axisInputLockVert = true;
        } else
            axisInputLockVert = false;

        cursorRB.velocity = Vector2.ClampMagnitude(cursorRB.velocity, speedMax);

        if (cursorRB.velocity.magnitude < speedMin)
            cursorRB.AddForce(cursorRB.velocity.normalized * underSpeedAccel * Time.fixedDeltaTime);
        cursorRB.AddForce(new Vector2(Mathf.PerlinNoise(Time.time * perlinSpeed, 0) - 0.5f, Mathf.PerlinNoise(Time.time * perlinSpeed, 32) - 0.5f) * perlinSpeed);

        velocity = cursorRB.velocity;

    }

    private void SetUpUI () {

        sOuterRadius.Radius = radiusMax - cursorRadius;
        sInnerRadius.Radius = radiusMin;
        collInnerRadius.radius = radiusMin;
        sCursor.transform.localPosition = Vector3.down * (radiusMin + cursorRadius);
        sCursor.Radius = cursorRadius;
        collCursor.radius = cursorRadius;
        sConnector.Start = Vector3.down * (0.25f);
        sConnector.End = Vector3.down * (Vector3.Distance(transform.position, sCursor.transform.position) - cursorRadius);

    }

    public void Reset() {
        sCursor.transform.localPosition = Vector3.down * (radiusMin + cursorRadius);
        //cursorRB.velocity = Vector2.down;
    }

}