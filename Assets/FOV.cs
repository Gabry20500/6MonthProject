using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = new Mesh();

        //float fov = 90.0f;
        //int rayCount = 2;
        //float angle = 0.0f;
        //float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = new Vector3(0,    0,     0);
        vertices[1] = new Vector3(-1000, 1000, 0);
        vertices[1] = new Vector3(1000,  1000, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
