using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseController : MonoBehaviour
{

    public float speed;
    [SerializeField] private CharacterInfo character; 
    private LevelManager levelManager;

    private bool touchingEnemy = false;

    private PathFinder pathFinder;
    private List<OverlayTile> path = new List<OverlayTile>();
    private OverlayTile spawnTile;
    private OverlayTile prevTile;
    private bool spawned = false;

    // Reference to the camera.
    private UnityEngine.Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        pathFinder = new PathFinder();

        levelManager = FindObjectOfType<LevelManager>();

        // Gets the main camera to know whehter enemies are on screen----------------------------------
        cam = UnityEngine.Camera.main;

        // for (int i = 0; i < levelManager.enemies.Capacity; i++)
        // {
        //     newEnemy = new EnemyMovement();
        //     levelManager.enemies.Add(newEnemy);
        //     Debug.Log("added 1");
        // }
        
        
        
    }

    // usinh late update so it occurs after overlay update but this is a lazy way
    // so we might need to make an event handler system in the future
    void LateUpdate()
    {
        if (!spawned){
            // Debug.Log("entered !spawned");
            var hit = GetTileAtPos(character.transform.position);
            // Debug.Log("spawned" + hit.HasValue);
            if (hit.HasValue)
            {
                spawnTile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
                PositionCharacterOnTile(spawnTile);
                character.onTile = spawnTile;
                prevTile = spawnTile;

                spawned = true;
                // Debug.Log("spawn");
            }

        }

        if (spawned && levelManager.enemiesSpawned.Contains(false)){
            // Debug.Log("entered !enemiesSpawned");
            for (int i = 0; i < levelManager.enemies.Count; i++)
            {
                var hit = GetTileAtPos(levelManager.enemies[i].transform.position);
                // Debug.Log("enemies" + i + hit.HasValue);
                if (hit.HasValue && levelManager.enemiesSpawned[i] == false)
                {
                    levelManager.enemySpawnTile.Add(hit.Value.collider.gameObject.GetComponent<OverlayTile>());
                    levelManager.enemies[i].PositionEnemyOnTile(levelManager.enemySpawnTile[i]);
                    levelManager.enemies[i].onTile = levelManager.enemySpawnTile[i];

                    levelManager.enemiesSpawned[i] = true;
                    //Debug.Log("enemy spawned i");
                }
            
            }

        }

        
        var focusedTileHit = GetFocusedOnTile();

        if(focusedTileHit.HasValue){
            OverlayTile tile = focusedTileHit.Value.collider.gameObject.GetComponent<OverlayTile>();
            transform.position = tile.transform.position;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
            
            // if (character.onTile.gridLocation == character.transform.position){
            //     character.onTile.hideTile();    
            // } else {
            //     character.onTile.showTile();
            // }
            
            //else {
            //     character.onTile.hideTile();
            // }

            // Lighten-up a square of tiles.
            if (character.onTile != null) {
                character.onTile.lightUpAllAdjacent(character.spotlightSize);

                if (Input.GetMouseButtonDown(0)){
                    // should be this?: dk how to fix: overlayTile.GetComponent<Overlay>().showTile();
                    // just copied and pasted code from the function here lol



                    if (character != null){
                        path = pathFinder.FindPath(character.onTile, tile);
                    }
                }
            }
            
        }
        if(path.Count>0){
            MoveAlongPath();
        }
        if (character.onTile != null && prevTile != null) {
            if (prevTile.gridLocation != character.onTile.gridLocation){
                //Debug.Log("showtile");
                
                // Darken a square of tiles.
                // prevTile.makeDark();
                character.onTile.darkenAllAdjacent(prevTile, character.spotlightSize);
            }
            prevTile=character.onTile;
        }

        // for echolocation attracting enemy movement:
        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < levelManager.enemies.Count; i++)
            {
                // Requires the enemy to be onscreen to move   ----------------------------------
                Vector3 viewPos = cam.WorldToViewportPoint(levelManager.enemies[i].transform.position);
                if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0) {

                    var enemyHit = GetTileAtPos(levelManager.enemies[i].transform.position);
                    OverlayTile enemy1Tile = enemyHit.Value.collider.gameObject.GetComponent<OverlayTile>();
                    levelManager.enemies[i].ApproachPlayer(enemy1Tile, character.onTile);

                }

                
            }
            
        }

        //checks if enemies are touching player
        for (int i = 0; i < levelManager.enemies.Count; i++)
        {
            if (character.onTile != null && levelManager.enemies[i].onTile != null && touchingEnemy == false)
            {
                if (character.onTile.Equals(levelManager.enemies[i].onTile))
                {
                    touchingEnemy = true;
                    levelManager.ResetLevel();
                }
            }
        }

    }

    private void MoveAlongPath()
    {
        var step = speed * Time.deltaTime;
        character.transform.position = Vector2.MoveTowards(character.transform.position, path[0].transform.position, step);

        if(Vector2.Distance(character.transform.position, path[0].transform.position) < 0.0001f){
            PositionCharacterOnTile(path[0]);
            path.RemoveAt(0);
        }
    }

    private void PositionCharacterOnTile(OverlayTile tile)
    {
        character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z);
        character.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
        character.onTile = tile;
    }


    // rename this one lol
    public RaycastHit2D? GetFocusedOnTile(){
        // get mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //convert mouse position vector into 2d object
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);
        // list of objects in raycast
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

        if(hits.Length > 0){
            return hits.OrderByDescending( i => i.collider.transform.position.z).First();
        }

        return null;
    }

    public RaycastHit2D? GetTileAtPos(Vector2 pos){
        // list of objects in raycast
        RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector2.zero);

        if(hits.Length > 0){
            return hits.OrderByDescending( i => i.collider.transform.position.z).First();
        }

        return null;
    }

}
