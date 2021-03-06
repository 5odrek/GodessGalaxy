﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    int faceID = 0;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;
   

    [Range(2,256)]
    public int resolution = 10;

    Vector3[][,] wayPoints;

    private void Start()
    {
  
        Initialize();
        
        GenerateMesh();
        Debug.Log("called");
        GenerateCollider();
        
    }

    void Initialize()
    {
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }

        terrainFaces = new TerrainFace[6];

        wayPoints = new Vector3[6][,];

        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = new Vector3[resolution, resolution];
        }

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
                
                
                GameObject meshObject = new GameObject("face " + faceID);
                faceID++;
                meshObject.transform.parent = transform;

                meshObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObject.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            
            

            terrainFaces[i] = new TerrainFace(meshFilters[i].sharedMesh, resolution, directions[i]);
            meshObject.layer = gameObject.layer;
        }


      
    }

    private void GenerateCollider()
    {
        
        float colliderRadius = Vector3.Distance(transform.position, terrainFaces[0].wayPoints[0, 0].GetNodeWorldPoint());
        SphereCollider mySC = gameObject.AddComponent<SphereCollider>();
        mySC.radius = colliderRadius;
    }

    void GenerateMesh()
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        } 
    }

   
}
