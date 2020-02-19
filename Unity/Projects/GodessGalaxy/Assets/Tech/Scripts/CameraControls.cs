using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{

    private Vector2 touchStartPosition;
    private Vector2 touchActualPosition;

    private Vector3 pivotVectorStart;
    private Vector3 pivotVectorEnd;
    private Vector3 pivot;

    Vector3 forwardRef;

    private bool onRotation;
    private bool onInertia;

    private float swipeSpeedModifier;

    [SerializeField]
    private LayerMask safePlaneMask;

    private Transform myCam;

    [SerializeField]
    private float touchDeadZone;

    [SerializeField]
    private float rotSpeed;
    private float currentRotSpeed;

    void Start()
    {
        myCam = transform.GetChild(0);
        currentRotSpeed = rotSpeed;
    }

    private void OnEnable()
    {
        InputManager.onFingerDown += SetTouchStartPos;
        InputManager.onFingerMove += TrackTouchMovement;
        InputManager.onFingerUp += PlanetInertia;
    }

    private void OnDisable()
    {
        InputManager.onFingerDown -= SetTouchStartPos;
        InputManager.onFingerMove -= TrackTouchMovement;
        InputManager.onFingerUp -= PlanetInertia;
    }

    #region Raycast Logic

    private Vector3 CalculateRaycastHitpoint(Vector3 touchPos)
    {
        Ray touchRay = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit touchRayHit;

        if (Physics.Raycast(touchRay, out touchRayHit, 100f, safePlaneMask))
        {
            return touchRayHit.point;
        }
        return touchRayHit.point;

    }

    #endregion

    private void Update()
    {
        if (onInertia)
        {
            RotatePlanet(pivot, currentRotSpeed);
            currentRotSpeed = Mathf.Lerp(currentRotSpeed, 0, 1 * Time.deltaTime);

            if (currentRotSpeed < 0.5f)
            {
                onInertia = false;
            }
        }
    }

    private void RotatePlanet(Vector3 refVector, float speed)
    {
        this.transform.RotateAround(transform.position, Vector3.Cross(refVector, forwardRef), speed * Time.deltaTime);
    }

    private void SetTouchStartPos(Vector3 touchPos)
    {
        touchStartPosition = touchPos;
        pivotVectorStart = CalculateRaycastHitpoint(touchStartPosition);
        onInertia = false;
        currentRotSpeed = rotSpeed;
    }

    private void TrackTouchMovement(Vector3 touchPos)
    {
        touchActualPosition = touchPos;
        pivotVectorEnd = CalculateRaycastHitpoint(touchActualPosition);
        Debug.Log("start" + pivotVectorStart);
        Debug.Log("end" + pivotVectorEnd);
        if (Mathf.Abs(Vector3.Distance(touchStartPosition, touchActualPosition)) > touchDeadZone)
        {
            swipeSpeedModifier = Mathf.Clamp(Mathf.Abs(Vector3.Distance(pivotVectorStart, pivotVectorEnd)) * 10,0,5f);

            
            forwardRef = transform.forward;
            onRotation = true;
            pivot = pivotVectorStart - pivotVectorEnd;
            RotatePlanet(pivot, rotSpeed * swipeSpeedModifier);
        }
        pivotVectorStart = pivotVectorEnd;

    }

 
    private void PlanetInertia()
    {
        if (onRotation)
        {
            currentRotSpeed = rotSpeed * swipeSpeedModifier;
            forwardRef = transform.forward;
            onInertia = true;
            onRotation = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (Input.touchCount > 0)
        {
            Gizmos.DrawSphere(pivotVectorStart, 0.1f);
            Gizmos.DrawSphere(pivotVectorEnd, 0.1f);
        }


    }

}
