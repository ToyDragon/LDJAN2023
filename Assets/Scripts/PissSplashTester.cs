using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PissSplashTester : MonoBehaviour
{
    public float baseWidth = .1f;
    public Material pissMat;
    private Mesh splashMesh;
    void OnEnable() {
        splashMesh = CreateSplashMesh();
    }
    Mesh CreateSplashMesh() {
        Mesh mesh = new Mesh();

        var vertices = new Vector3[4];
        vertices[0] = new Vector3(+baseWidth*.5f, 0, 0);
        vertices[1] = new Vector3(-baseWidth*.5f, 0, 0);
        vertices[2] = new Vector3(-.5f, 0, 1f);
        vertices[3] = new Vector3(+.5f, 0, 1f);

        var normals = new Vector3[4];
        normals[0] = normals[1] = normals[2] = normals[3] = Vector3.up;

        var triangles = new int[2 * 3];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetTriangles(triangles, 0);

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        return mesh;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject pissObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
            pissObj.GetComponent<MeshFilter>().sharedMesh = splashMesh;
            pissObj.transform.position = transform.position;
            pissObj.transform.rotation = transform.rotation;
            pissObj.transform.localScale = Vector3.one * 10f;
            Destroy(pissObj.GetComponent<MeshCollider>());
            var pissRenderer = pissObj.GetComponent<MeshRenderer>();
            pissRenderer.material = Material.Instantiate(pissMat);
            pissRenderer.material.SetFloat("_StartTime", Time.time + .05f);
            pissObj.AddComponent<FollowPosition>().target = transform;
            pissObj.AddComponent<GoDieAfterTime>().dieTime = Time.time + .5f;
        }
    }


}
