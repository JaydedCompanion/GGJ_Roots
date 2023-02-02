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
    public Shapes.Polyline sArrowDown;
    public Shapes.Polyline sArrowLeft;
    public Shapes.Polyline sArrowRight;
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
    [Header("Area Parameters")]
    public float cursorRadius;
    public float radiusMin = 1;
    public float radiusMax = 2;

    private Rigidbody2D cursorRB;
    private CircleCollider2D collCursor;
    private CircleCollider2D collInnerRadius;
    private bool axisInputLockHori;
    private bool axisInputLockVert;

    // Start is called before the first frame update
    void Start() {

        if (!instance)
            instance = this;

        cursorRB = sCursor.GetComponent<Rigidbody2D>();
        collCursor = sCursor.GetComponent<CircleCollider2D>();
        collInnerRadius = sInnerRadius.GetComponent<CircleCollider2D>();

        cursorRB.velocity = Vector2.down;

        SetUpUI();

    }

    // Update is called once per frame
    void Update() {

        if (!Application.isPlaying)
            SetUpUI();

        pointingTo = sCursor.transform.localPosition.normalized;
        pointingToAngle = Vector2.SignedAngle(Vector2.up, sCursor.transform.localPosition);

        cursorMagnitude = (Vector2.Distance(transform.position, sCursor.transform.position) - cursorRadius);
        sConnector.End = Vector3.down * cursorMagnitude;
        sConnector.transform.localRotation = Quaternion.Euler(0, 0, pointingToAngle + 180);
        sInnerRadius.transform.localRotation = Quaternion.Euler(0, 0, pointingToAngle + 180);

        collOuterRadius.transform.localPosition = sCursor.transform.localPosition.normalized * radiusMax;
        collOuterRadius.transform.localRotation = Quaternion.Euler(0, 0, pointingToAngle);

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
        cursorRB.velocity = Vector2.down;
    }

}