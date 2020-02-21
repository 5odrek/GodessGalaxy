using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private Node[][,] grids;
    int gridIndex = 0;

    private void CopyWaypointsGrid(Node[,] gridToCopy, int gridSize)
    {
            grids[gridIndex] = new Node[gridSize, gridSize];
            System.Array.Copy(gridToCopy, grids[gridIndex], gridToCopy.Length);
            gridIndex++;
    }

    private void OnEnable()
    {
        TerrainFace.onMeshCreated += CopyWaypointsGrid;
    }


// Start is called before the first frame update
    void Start()
    {
        grids = new Node[6][,];

        for (int i = 0; i < grids.Length; i++)
        {
            grids[i] = new Node[0,0];
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public Node[,] GetGrid(int index)
    {
        return grids[index];
    }

    private void OnDrawGizmos()
    {

        if (grids != null)
        {
            for (int i = 0; i < grids.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        Gizmos.color = Color.red;
                        break;
                    case 1:
                        Gizmos.color = Color.yellow;
                        break;
                    case 2:
                        Gizmos.color = Color.green;
                        break;
                    case 3:
                        Gizmos.color = Color.blue;
                        break;
                    case 4:
                        Gizmos.color = Color.black;
                        break;
                    case 5:
                        Gizmos.color = Color.magenta;
                        break;
                }

                foreach(Node n in grids[i])
                {
                   // Gizmos.DrawSphere(n.GetNodeWorldPoint(), 0.02f);
                }
            }
        }
    }
}
