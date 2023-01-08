using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTileManager : MonoBehaviour
{
    public Dictionary<Vector2Int, GameObject> tileToRootObject = new Dictionary<Vector2Int, GameObject>();
    public List<GameObject> beetGroupPrefabs = new List<GameObject>();
    public float tileSize = 25f;
    public int loadedTileRange = 10;
    private Vector2Int lastLoadedTile = Vector2Int.one * -1000;
    private GameObject vampire;
    private HashSet<Vector2Int> activeTiles = new HashSet<Vector2Int>();
    void OnEnable()
    {
        vampire = GameObject.Find("Vampire");
    }
    void FixedUpdate()
    {
        Vector2Int newTile = new Vector2Int(Mathf.RoundToInt(vampire.transform.position.x / tileSize), Mathf.RoundToInt(vampire.transform.position.z / tileSize));
        if (newTile == lastLoadedTile) {
            return;
        }

        lastLoadedTile = newTile;
        Debug.Log("Loading around tile " + newTile);

        var tilesToUnload = new List<Vector2Int>();
        foreach (var activeTile in activeTiles) {
            if (Mathf.Abs(activeTile.x - newTile.x) > loadedTileRange || Mathf.Abs(activeTile.y - newTile.y) > loadedTileRange) {
                tilesToUnload.Add(activeTile);
            }
        }

        foreach (var toUnload in tilesToUnload) {
            UnloadTile(toUnload);
        }
        
        for (int xOff = -loadedTileRange; xOff <= loadedTileRange; xOff++) {
            for (int yOff = -loadedTileRange; yOff <= loadedTileRange; yOff++) {
                LoadTile(new Vector2Int(xOff, yOff) + newTile);
            }
        }
    }

    private void UnloadTile(Vector2Int toUnload) {
        if (tileToRootObject.TryGetValue(toUnload, out var obj)) {
            obj.SetActive(false);
        }
        activeTiles.Remove(toUnload);
    }
    private void LoadTile(Vector2Int toLoad) {
        if (tileToRootObject.TryGetValue(toLoad, out var obj)) {
            obj.SetActive(true);
        } else {
            var rootObj = tileToRootObject[toLoad] = new GameObject();
            rootObj.name = "Tile " + toLoad;
            float densityScore = 1f - toLoad.magnitude / 30f;
            Debug.Log("Created tile " + rootObj.name + " with density " + densityScore);
            if (Random.Range(0, 1f) <= densityScore) {
                var newGroup = GameObject.Instantiate(beetGroupPrefabs[Random.Range(0, beetGroupPrefabs.Count)]);
                newGroup.transform.SetParent(rootObj.transform);
                var inUnitCircle = Random.insideUnitCircle;
                newGroup.transform.position = (new Vector3(toLoad.x, 0, toLoad.y) + new Vector3(inUnitCircle.x, 0, inUnitCircle.y) * .15f) * tileSize;

                if (Random.Range(0, 1f) >= densityScore * densityScore) {
                    BloodBeetController[] beetsInBatch = newGroup.GetComponentsInChildren<BloodBeetController>();
                    int amtToHide = Mathf.RoundToInt((1f - Random.Range(densityScore, 1f)) * beetsInBatch.Length);
                    Debug.Log("... and hid " + amtToHide + " beets.");
                    for (int i = 0; i < amtToHide; i++) {
                        beetsInBatch[i].gameObject.SetActive(false);
                    }
                }

                newGroup.transform.localRotation = Quaternion.Euler(0, 90 * Random.Range(0, 4), 0);
            }
        }
        activeTiles.Add(toLoad);
    }
}
