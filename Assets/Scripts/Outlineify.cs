using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlineify : MonoBehaviour
{
    public bool skipUpDownTris = true;
    public bool disableAfterCreating = false;
    public Material outlineMaterial;
    private GameObject outlineObj;
    void Start()
    {
        var outlineTransform = transform.Find("outline");
        if (outlineTransform) {
            outlineObj = outlineTransform.gameObject;
            return;
        }

        // The mesh combination assumes the object is at 0,0,0 and scale 1,1,1
        Transform storedParent = transform.parent;
        Vector3 storedPosition = transform.position;
        Vector3 storedScale = transform.localScale;
        transform.parent = null;
        transform.position = Vector3.zero;
        transform.localScale = Vector3.one;

        // Create a sub object that will house and render the outline.
        outlineObj = new GameObject("outline");
        outlineObj.transform.SetParent(transform);
        outlineObj.AddComponent<MeshRenderer>().material = outlineMaterial;
        outlineObj.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        // Loop through child meshes to find ones that need outlines.
        var combinedMeshFilter = outlineObj.AddComponent<MeshFilter>();
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

        // The beet model has two flat planes between it's top half and bottom half. We exclude these by checking for triangles pointed straight up or down.
        int[] flippedTriangles = new int[combinedMesh.triangles.Length];
        int skippedCount = 0;
        for (int i = 0; i < flippedTriangles.Length; i += 3) {
            if(skipUpDownTris){
                Vector3 aToB = combinedMesh.vertices[combinedMesh.triangles[i + 1]] - combinedMesh.vertices[combinedMesh.triangles[i + 0]];
                Vector3 bToC = combinedMesh.vertices[combinedMesh.triangles[i + 2]] - combinedMesh.vertices[combinedMesh.triangles[i + 1]];
                Vector3 normalDir = Vector3.Cross(aToB, bToC);
                if (Mathf.Abs(normalDir.y) > .9f) {
                    skippedCount++;
                    continue;
                }
            }
            flippedTriangles[i + 0 - skippedCount*3] = combinedMesh.triangles[i + 2];
            flippedTriangles[i + 1 - skippedCount*3] = combinedMesh.triangles[i + 1];
            flippedTriangles[i + 2 - skippedCount*3] = combinedMesh.triangles[i + 0];
        }
        combinedMesh.SetTriangles(flippedTriangles, 0);
        combinedMeshFilter.sharedMesh = combinedMesh;

        // Restore saved pos/scale.
        transform.parent = storedParent;
        transform.position = storedPosition;
        transform.localScale = storedScale;

        if(disableAfterCreating) outlineObj.SetActive(false);
    }

    void OnEnable() {
        outlineObj?.SetActive(true);
    }

    void OnDisable() {
        outlineObj?.SetActive(false);
    }
}
