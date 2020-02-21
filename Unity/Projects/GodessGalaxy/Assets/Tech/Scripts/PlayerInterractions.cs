using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterractions : MonoBehaviour
{
    public LayerMask planetMask;

    private int faceHitId;
    private Vector3 hitPos;

    private void OnEnable()
    {
        InputManager.onFingerTap += FindClosestNodeToTap;
    }

    private void OnDisable()
    {
        
    }

    private void FindClosestNodeToTap(Vector3 touchPos)
    {
        Debug.Log("called");
        Ray touchRay = Camera.main.ScreenPointToRay(touchPos);
       
        RaycastHit touchRayHit;

        if (Physics.Raycast(touchRay, out touchRayHit, 200f, planetMask))
        {
            hitPos = touchRayHit.point;
            faceHitId = touchRayHit.transform.GetSiblingIndex();
            Debug.Log(faceHitId);
        }


    }

}
