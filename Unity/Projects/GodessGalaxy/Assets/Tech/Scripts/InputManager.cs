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

    private float tapTimer;

    [SerializeField]
    private float tapTimingTreshold;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (onFingerDown != null)
                {
                    onFingerDown(Input.GetTouch(0).position);
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (onFingerMove != null)
                {
                    onFingerMove(Input.GetTouch(0).position);
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                onFingerUp();
            }
        }

   
        
    }

  
}
