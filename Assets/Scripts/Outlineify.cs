using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlineify : MonoBehaviour
{
    public Material outlineMaterial;
    void Start()
    {
        if (transform.Find("outline")) {
            return;
        }
        Vector3 storedPosition = transform.position;
        transform.position = Vector3.zero;
        GameObject outline = new GameObject("outline");
        outline.transform.SetParent(transform);
        outline.AddComponent<MeshRenderer>().material = outlineMaterial;
        outline.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        var combinedMeshFilter = outline.AddComponent<MeshFilter>();
        List<CombineInstance> combineInstances = new List<CombineInstance>();
        var combinedMesh = new Mesh();
        foreach (var childMesh in GetComponentsInChildren<MeshFilter>()) {
            if (childMesh.GetComponent<OutlineMe>()) {
                var combineInstance = new CombineInstance();
                combineInstance.mesh = childMesh.sharedMesh;
                combineInstance.transform = childMesh.transform.localToWorldMatrix;
                combineInstances.Add(combineInstance);
            }
        }
        combinedMesh.CombineMeshes(combineInstances.ToArray());
        int[] flippedTriangles = new int[combinedMesh.triangles.Length];
        int skippedCount = 0;
        for (int i = 0; i < flippedTriangles.Length; i += 3) {
            Vector3 aToB = combinedMesh.vertices[combinedMesh.triangles[i + 1]] - combinedMesh.vertices[combinedMesh.triangles[i + 0]];
            Vector3 bToC = combinedMesh.vertices[combinedMesh.triangles[i + 2]] - combinedMesh.vertices[combinedMesh.triangles[i + 1]];
            Vector3 normalDir = Vector3.Cross(aToB, bToC);
            if (Mathf.Abs(normalDir.y) > .9f) {
                skippedCount++;
                continue;
            }
            flippedTriangles[i + 0 - skippedCount*3] = combinedMesh.triangles[i + 2];
            flippedTriangles[i + 1 - skippedCount*3] = combinedMesh.triangles[i + 1];
            flippedTriangles[i + 2 - skippedCount*3] = combinedMesh.triangles[i + 0];
        }
        combinedMesh.SetTriangles(flippedTriangles, 0);
        combinedMeshFilter.sharedMesh = combinedMesh;
        transform.position = storedPosition;
    }
}
