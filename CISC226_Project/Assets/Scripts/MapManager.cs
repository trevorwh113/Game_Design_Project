using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;

    // immutable instance; cant be changed?
    public static MapManager Instance{ get {return _instance;}}
    public OverlayTile overlayTilePrefab;
    public GameObject overlayContainer;

    // dict to store all overlay tiles by position
    public Dictionary<Vector2Int, OverlayTile> map;
    private List<string> blockedTiles = new List<string> { "Cave_v7_4", "Cave_v7_5", "Cave_v7_6",
                                                           "Cave_v7_7", "Cave_v7_8", "Cave_v7_9",
                                                           "Cave_v7_10", "Cave_v7_11", "Cave_v7_12",
                                                           "Cave_v7_13", "Cave_v7_14", "Cave_v7_15",
                                                           "Cave_v7_16" };

    private void Awake(){
        //makes a singleton manager
        // check if instance already exists (need to not be empty and also not this current object)
        if (_instance != null && _instance != this){
            // if so get rid of it
            Destroy(this.gameObject);
        } else {
            // else make it this one
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        var tileMap = gameObject.GetComponentInChildren<Tilemap>();
        map = new Dictionary<Vector2Int, OverlayTile>();
        BoundsInt bounds = tileMap.cellBounds;

        for (int z = bounds.max.z; z >= bounds.min.z; z--){
            for(int y = bounds.min.y; y < bounds.max.y; y++){ 
                for(int x = bounds.min.x; x < bounds.max.x; x++){ 
                    var tileLocation = new Vector3Int(x, y, z);
                    var tileKey = new Vector2Int(x, y);

                    // makes a bunch of tile objects in the overlay manager
                    if(tileMap.HasTile(tileLocation) && !map.ContainsKey(tileKey)){
                        
                        var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);
                        var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);
                        overlayTile.transform.position = cellWorldPosition;
                        overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;
                        overlayTile.gridLocation = tileLocation;
                        if (blockedTiles.Contains(tileMap.GetSprite(new Vector3Int((int)cellWorldPosition.x, (int)cellWorldPosition.y, (int)cellWorldPosition.z)).ToString())){
                            overlayTile.isBlocked = true;
                        }
                        map.Add(tileKey, overlayTile);
                    }
                }
            }
        }
    }
}
