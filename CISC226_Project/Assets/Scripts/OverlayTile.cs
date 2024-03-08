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

    // Flags a goal tile.
    public bool is_goal_tile;
    


    void Start() {
        // Tiles start out as not lit.
        isLit = false;
    }
    
    void Update()
    {
        // If the tile is echolocated, light it up. Otherwise, turn it off.
        // isLit is used to make sure the tile is not light up for other reasons (i.e. player vision).
        if (echolocated && !isLit) {
            gameObject.GetComponent<OverlayTile>().EchoON();

            // Turn it off after a delay.
            echolocated = false;
            StartCoroutine("FadeEcho");

        }
        // The tiles in the vision radius should not fade.
        if (echolocated && isLit) {
            echolocated = false;
        }
    }


    private IEnumerator FadeEcho() {
        Echolocator echolocator = FindObjectOfType<Echolocator>();
        echolocator.Disable();

        yield return new WaitForSeconds(echolocator.echo_duration);

        EchoOFF();
        echolocator.Enable();
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

        // If the range is more than 1, remove the corners to make a round shape.
        if (range > 1) {
            // Top right.
            locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x + range, 
                                                 currentOverlayTile.gridLocation.y + range);
            if (map.ContainsKey(locationToCheck)) {
                    neighbours.Remove(map[locationToCheck]);
            }

            // Top Left.
            locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x - range, 
                                                 currentOverlayTile.gridLocation.y + range);
            if (map.ContainsKey(locationToCheck)) {
                    neighbours.Remove(map[locationToCheck]);
            }
            
            // Bottom right.
            locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x + range, 
                                                 currentOverlayTile.gridLocation.y - range);
            if (map.ContainsKey(locationToCheck)) {
                    neighbours.Remove(map[locationToCheck]);
            }

            // Bottom left.
            locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x - range, 
                                                 currentOverlayTile.gridLocation.y - range);
            if (map.ContainsKey(locationToCheck)) {
                    neighbours.Remove(map[locationToCheck]);
            }
        }
        
        return neighbours;
    }


    public void EchoON(){
        // sets transparency to transparent.
        gameObject.GetComponent<SpriteRenderer>().color =new Color(1,1,1,0);
    }

    public void EchoOFF(){
        // sets the color back to black
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }
    
    public void MarkTileAsEchoed() {
        echolocated = true;
    }

    public void UnmarkTileAsEchoed() {
        echolocated = false;
    }

    public void MarkAdjacentAsEchoed(OverlayTile currentOverlayTile)
    {
        var map = MapManager.Instance.map;

        List<OverlayTile> neighbours = new List<OverlayTile>();
        neighbours.Add(currentOverlayTile);
        Vector2Int locationToCheck;

        // Get the tiles in a + around and including the given tile.

        locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x + 1, 
                                         currentOverlayTile.gridLocation.y);
        if (map.ContainsKey(locationToCheck)) {
            neighbours.Add(map[locationToCheck]);
        }
        locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x - 1, 
                                         currentOverlayTile.gridLocation.y);
        if (map.ContainsKey(locationToCheck)) {
            neighbours.Add(map[locationToCheck]);
        }

        locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x, 
                                         currentOverlayTile.gridLocation.y + 1);
        if (map.ContainsKey(locationToCheck)) {
            neighbours.Add(map[locationToCheck]);
        }
        locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x, 
                                         currentOverlayTile.gridLocation.y - 1);
        if (map.ContainsKey(locationToCheck)) {
            neighbours.Add(map[locationToCheck]);
        }

        // Loop over the neighbours and mark them as echolocated.
        foreach (var tile in neighbours) {
            tile.MarkTileAsEchoed();
        } 
    }

    public void UnmarkAdjacentAsEchoed(OverlayTile currentOverlayTile)
    {
        var map = MapManager.Instance.map;

        List<OverlayTile> neighbours = new List<OverlayTile>();
        neighbours.Add(currentOverlayTile);
        Vector2Int locationToCheck;

        // Get the tiles in a + around and including the given tile.

        locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x + 1, 
                                         currentOverlayTile.gridLocation.y);
        if (map.ContainsKey(locationToCheck)) {
            neighbours.Add(map[locationToCheck]);
        }
        locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x - 1, 
                                         currentOverlayTile.gridLocation.y);
        if (map.ContainsKey(locationToCheck)) {
            neighbours.Add(map[locationToCheck]);
        }

        locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x, 
                                         currentOverlayTile.gridLocation.y + 1);
        if (map.ContainsKey(locationToCheck)) {
            neighbours.Add(map[locationToCheck]);
        }
        locationToCheck = new Vector2Int(currentOverlayTile.gridLocation.x, 
                                         currentOverlayTile.gridLocation.y - 1);
        if (map.ContainsKey(locationToCheck)) {
            neighbours.Add(map[locationToCheck]);
        }

        // Loop over the neighbours and mark them as echolocated.
        foreach (var tile in neighbours) {
            tile.UnmarkTileAsEchoed();
        } 
    }

}
