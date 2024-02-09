using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    public int G;
    public int H;

    public int F {get { return G+H;}}

    public bool isBlocked;
    public OverlayTile previous;
    public Vector3Int gridLocation; 
    // Flags if a tile is lit or not.
    public bool isLit;
    public bool echolocated;
    


    void Start() {
        // Tiles start out as not lit.
        isLit = false;
    }
    
    void Update()
    {
        
    }
    public void makeDark(){
        // sets transparency to full
        if (isLit) {
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            isLit = false;
        }
        
    }
    public void lightUp(){
        // sets transparency to none; invisible
        if (!isLit) {
            gameObject.GetComponent<SpriteRenderer>().color =new Color(1,1,1,0);
            isLit = true;
        }
    }

    public void lightUpAllAdjacent(int range) {
        // Light up the current tile.
        lightUp();
        
        // Get the list of neighbor tiles.
        List<OverlayTile> adjacentTiles = GetAllAdjacentTiles(gameObject.GetComponent<OverlayTile>(), range);

        // Lighten up all the neighbor tiles.
        foreach (var tile in adjacentTiles) {
            tile.lightUp();
        }
    }

    public void darkenAllAdjacent(OverlayTile prev, int range) {
        // The object used to call this is current tile.
        // We need the previous tile to make it work.

        // Generate the lists of all previous tiles and current tiles.
        List<OverlayTile> curTiles = GetAllAdjacentTiles(gameObject.GetComponent<OverlayTile>(), range);
        List<OverlayTile> prevTiles = GetAllAdjacentTiles(prev, range);
        
        // Find only the part that does not overlap. Darken only those.
        foreach (var tile in prevTiles) {
            if (! curTiles.Contains(tile)) {
                tile.makeDark();
            }
        }
    }


    private  List<OverlayTile> GetAllAdjacentTiles(OverlayTile currentOverlayTile, int range)
    {
        var map = MapManager.Instance.map;

        List<OverlayTile> neighbours = new List<OverlayTile>();
        Vector2Int locationToCheck;

        // Loop over all tiles in a square around the player based on the
        // value 'range' given.

        for (int x = -range; x <= range; x++) {
            for (int y = -range; y <= range; y++) {
                locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x + x, 
                                                 currentOverlayTile.gridLocation.y + y);

                if (map.ContainsKey(locationToCheck)) {
                    neighbours.Add(map[locationToCheck]);
                }
            }
        }
        
        return neighbours;
    }
}
