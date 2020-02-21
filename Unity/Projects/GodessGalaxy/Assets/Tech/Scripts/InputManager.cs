using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void FingerDownAction(Vector3 fingerPosition);
    public static event FingerDownAction onFingerDown;

    public delegate void FingerMoveAction(Vector3 fingerPosition);
    public static event FingerMoveAction onFingerMove;

    public delegate void FingerUpAction();
    public static event FingerUpAction onFingerUp;

    public delegate void FingerTapAction(Vector3 fingerPosition);
    public static event FingerTapAction onFingerTap;

    [SerializeField]
    private float tapTimer;

    [SerializeField, Range(0,100)]
    private float deadZone;

    
   

    private Vector3 fingerStartPos;
    private Vector3 fingerCurrentPos;

    private bool isHolding;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                fingerStartPos = Input.GetTouch(0).position;
                onFingerDown?.Invoke(fingerStartPos);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                onFingerMove?.Invoke(Input.GetTouch(0).position);
                StartCoroutine("TapTimer");
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                onFingerUp?.Invoke();
                StopAllCoroutines();

                if (!isHolding)
                {
                    onFingerTap(Input.GetTouch(0).position);
                }
                isHolding = false;
            }

        }
    }

  IEnumerator TapTimer()
    {
        yield return new WaitForSeconds(tapTimer);
        isHolding = true;
    }
}
