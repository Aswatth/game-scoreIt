using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShoot : MonoBehaviour
{
    private Vector3 mouseDownPos;
    private Vector3 mouseReleasePos;

    [SerializeField] private Rigidbody rb;

    [Range(1, 100)]
    [SerializeField] private float forceMultiplier;

    [SerializeField] private float minPower;
    [SerializeField] private float maxPower;

    Camera cam;
    [SerializeField]
    [Range(1, 100)]
    private float maxVelocity;

    [SerializeField] Transform dragArea;
    float dragAreaSize = 0;

    [SerializeField] LayerMask layerToIgnore;

    public static DragAndShoot Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        cam = Camera.main;
        dragAreaSize = dragArea.localScale.magnitude;

        //Set inital ball style
        GetComponent<MeshRenderer>().material = AchievementHandler.Instance.GetEquippedMaterial();
    }

    //Handle mouse drag
    private void Update()
    {
        //FOR PC using MOUSE
        PcMouseMovement();

        //FOR MOBILE using TOUCH
        MobileTouchMovement();
    }

    private void PcMouseMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, layerToIgnore))
            {
                mouseDownPos = hit.point;

                //mouseDownPos = new Vector3(Mathf.Clamp(mouseDownPos.x, 0, dragAreaSize), 0, Mathf.Clamp(mouseDownPos.x, 0, dragAreaSize));

                //Debug.Log("Mouse down: " + mouseDownPos);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, layerToIgnore))
            {
                DragTrajectory.Instance.OnDrag(mouseDownPos, hit.point);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, layerToIgnore))
            {
                mouseReleasePos = hit.point;
                DragTrajectory.Instance.ResetLine();
                Shoot(mouseDownPos - mouseReleasePos);
            }
        }
    }

    private void MobileTouchMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = cam.ScreenPointToRay(touch.position);
            if (touch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out RaycastHit hit, layerToIgnore))
                {
                    mouseDownPos = hit.point;
                    //Debug.Log("Mouse DownPos = " + mouseDownPos);
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (Physics.Raycast(ray, out RaycastHit hit, layerToIgnore))
                {
                    DragTrajectory.Instance.OnDrag(mouseDownPos, hit.point);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (Physics.Raycast(ray, out RaycastHit hit, layerToIgnore))
                {
                    mouseReleasePos = hit.point;
                    //Debug.Log("Mouse Release = " + mouseReleasePos);
                    DragTrajectory.Instance.ResetLine();
                    Shoot(mouseDownPos - mouseReleasePos);
                }
            }
        }
    }

    //Shoot in the mouse drag direction
    private void Shoot(Vector3 forceDirection)
    {
        forceDirection = new Vector3(Mathf.Clamp(forceDirection.x, minPower, maxPower), 0, Mathf.Clamp(forceDirection.z, minPower, maxPower));
        //Debug.Log("Force direction: " + forceDirection);
        rb.AddForce(new Vector3(forceDirection.x, 0, forceDirection.z) * forceMultiplier * Time.deltaTime, ForceMode.Impulse);
    }

    //Limit velocity of ball
    //private void FixedUpdate()
    //{
    //    if (rb.velocity.magnitude > maxVelocity)
    //        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(mouseDownPos, 0.2f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(mouseReleasePos, 0.2f);
    }
}
