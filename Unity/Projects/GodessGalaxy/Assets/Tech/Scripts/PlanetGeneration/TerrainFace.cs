using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace 
{
    private Mesh mesh;
    private int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public Node[,] wayPoints;

    [Range(0,1)]
    public float planetRadius;

    public delegate void SendGrid(Node[,] gridToSend, int resolution);
    public static event SendGrid onMeshCreated;


    public TerrainFace(Mesh mesh, int resolution, Vector3 localUp)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;
        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1)*6];

        wayPoints = new Node[resolution-1, resolution-1];
        int triangleIndex = 0;

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector2 waypointPercent = new Vector2(1, 1) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB;


                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = pointOnUnitSphere;
                


                if (x != resolution-1 && y != resolution - 1)
                {
                    wayPoints[x, y] = new Node((pointOnUnitCube + (waypointPercent.x) * axisA + (waypointPercent.y) * axisB).normalized, true);

                    triangles[triangleIndex] = i;
                    triangles[triangleIndex + 1] = i + resolution+1;
                    triangles[triangleIndex + 2] = i + resolution;

                    triangles[triangleIndex + 3] = i;
                    triangles[triangleIndex + 4] = i + 1;
                    triangles[triangleIndex + 5] = i + resolution + 1;

                    triangleIndex += 6;
                }
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        onMeshCreated(wayPoints, resolution-1);
    }

    
}
