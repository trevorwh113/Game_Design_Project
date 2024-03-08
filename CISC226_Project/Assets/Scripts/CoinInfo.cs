using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinInfo : MonoBehaviour
{
    public OverlayTile onTile;
    bool spawned = false;

    public CoinSparkleInfo sparkle;
    public bool isCollected = false;

    public SpriteRenderer spriteRenderer;

    public MouseController cursor;
    private CharacterInfo character;
    private LevelManager levelManager;
    private TMP_Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        cursor = FindObjectOfType<MouseController>();
        character = FindObjectOfType<CharacterInfo>();
        levelManager = FindObjectOfType<LevelManager>();
        coinsText = FindObjectOfType<TMP_Text>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (spawned == false)
        {
            var hit = cursor.GetComponent<MouseController>().GetTileAtPos(transform.position);
            if (hit.HasValue) {
                onTile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
                spawned = true;
            }
            
        }
        
        if (character.onTile != null && onTile != null)
        {
            if (character.onTile.Equals(onTile))
            {
                if (isCollected == false)
                {
                    levelManager.coins_collected++;
                    isCollected = true;
                    //disappear by toggling sprite renderers
                    spriteRenderer.enabled = false; 
                    sparkle.spriteRenderer.enabled = false;

                    coinsText.SetText("Coins: " + levelManager.coins_collected);
                    Debug.Log("Coins: " + levelManager.coins_collected);

                }
                    
            } 
                
        }
        
    }

    public void PositionCoinOnTile(OverlayTile tile)
    {
        transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z);
        GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
        onTile = tile;
    }

    public void Collect()
    {
        
        //do smth to disappear
    }
    

}
