using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterractions : MonoBehaviour
{
    public LayerMask planetMask;

    private Vector3 hitPos;

    Grid myGrid;

    Node targetNode;
    Node playerNode;

    Planet myPlanet;

    private void OnEnable()
    {
        InputManager.onFingerTap += FindClosestNodeToTap;
    }

    private void OnDisable()
    {
        InputManager.onFingerTap -= FindClosestNodeToTap;
    }

    private void Start()
    {
        myGrid = FindObjectOfType<Grid>();
        myPlanet = FindObjectOfType<Planet>();
    }

    private void FindClosestNodeToTap(Vector3 touchPos)
    {

        Ray touchRay = Camera.main.ScreenPointToRay(touchPos);
        float currentDist = 0;
        Node closestNodeToTap = null;
        RaycastHit touchRayHit;

        if (Physics.Raycast(touchRay, out touchRayHit, 200f, planetMask))
        {
            hitPos = touchRayHit.point;
        }

        for (int i = 0; i < 6; i++)
        {
            for (int y = 0; y < myPlanet.resolution-1; y++)
            {
                for (int x = 0; x < myPlanet.resolution-1; x++)
                {
                    
                    
                    if (closestNodeToTap == null)
                    {
                        closestNodeToTap = myGrid.GetGrid(i)[x, y];
                        currentDist = Vector3.Distance(closestNodeToTap.GetNodeWorldPoint(),hitPos);
                    }

                    if (Vector3.Distance(hitPos, myGrid.GetGrid(i)[x, y].GetNodeWorldPoint()) < currentDist)
                    {
                        Debug.Log("switch");
                        closestNodeToTap = myGrid.GetGrid(i)[x, y];
                        currentDist = Vector3.Distance(closestNodeToTap.GetNodeWorldPoint(), hitPos);
                    }
                    
                }
            }
            
        }
        targetNode = closestNodeToTap;
    }

    private void OnDrawGizmos()
    {
        if (targetNode != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(targetNode.GetNodeWorldPoint(), 0.02f);
        }
        
        if(hitPos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hitPos, 0.04f);
        }
    }

}
