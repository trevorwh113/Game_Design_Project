using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseController : MonoBehaviour
{
    public float speed;
    public GameObject characterPrefab;
    private CharacterInfo character; 

    private PathFinder pathFinder;
    private List<OverlayTile> path = new List<OverlayTile>();
    private OverlayTile spawnTile;
    private OverlayTile prevTile;


    // Start is called before the first frame update
    void Start()
    {
        pathFinder = new PathFinder();
    
        
    }

    // usinh late update so it occurs after overlay update but this is a lazy way
    // so we might need to make an event handler system in the future
    void LateUpdate()
    {
        
        if (character == null){
            var hit = GetTileAtPos(new Vector2(-0.5f, -4.5f));
            spawnTile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
            character = Instantiate(characterPrefab).GetComponent<CharacterInfo>();
            //character.transform.position = new Vector3(-0.5f, -4.5f, 0);
            PositionCharacterOnTile(spawnTile);
            character.onTile = spawnTile;
            prevTile = spawnTile;
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
            character.onTile.lightUp();
            if (Input.GetMouseButtonDown(0)){
                // should be this?: dk how to fix: overlayTile.GetComponent<Overlay>().showTile();
                // just copied and pasted code from the function here lol



                if (character != null){
                    path = pathFinder.FindPath(character.onTile, tile);
                }
            }
        }
        if(path.Count>0){
            MoveAlongPath();
        }
        if (prevTile.gridLocation != character.onTile.gridLocation){
                Debug.Log("showtile");
                prevTile.makeDark();
            }
        prevTile=character.onTile;
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
