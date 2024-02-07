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
                        map.Add(tileKey, overlayTile);
                    }
                }
            }
        }
    }
}
