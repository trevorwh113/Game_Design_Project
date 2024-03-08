using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // The properties unique to each level, such as number of coins,
    // crystals, enemies, etc.

    //list of enemies: the int in brackets is the list capacity. change as needed
    
    [SerializeField] public List<EnemyMovement> enemies = new List<EnemyMovement>();
    public List<OverlayTile> enemySpawnTile;
    public List<bool> enemiesSpawned = new List<bool>();

    public int crystals_remaining;      // Set for the level.
    public int coins_collected = 0;     // Should default to 0 always.

    // Indicates the string to transition to on a win.
    public string win_scene;

    // Sets the possible win conditions.
    public bool break_crystals;
    public bool kill_enemies;
    public bool reach_goal;


    // The character.
    public CharacterInfo character;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Win conditions.
        if (break_crystals) {
            if (crystals_remaining == 0) {
                WinLevel();
            }
        }

        if (kill_enemies) {
            if (enemies.Count == 0) {
                WinLevel();
            }
        }

        if (reach_goal) {
            if (character.onTile != null 
                && character.onTile.is_goal_tile) {
                WinLevel();
            }
        }

        
        // // Testing reasons.
        // if (Input.GetKeyDown(KeyCode.E)) {
        //     collectCoin();
        //     breakCrystal();
        // }
        // if (Input.GetKeyDown(KeyCode.R)) {
        //     ResetLevel();
        // }
        // if (Input.GetKeyDown(KeyCode.W)) {
        //     WinLevel();
        // }
    }


    // Method to reset the scene.
    public void ResetLevel() {
        // Debug.Log("resetting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Called when you win a level. Loads the win screen and adds
    // everything relevant to the PlayerInfo object.
    public void WinLevel() {
        // Adds the collected coins to the player's total if the data is found.
        PlayerInfo playerInfo = FindObjectOfType<PlayerInfo>();
        if (playerInfo != null) {
            playerInfo.coins += coins_collected;
        }


        // Loads the win screen so that the player knows they won.
        SceneManager.LoadScene(win_scene);
    }


    // Methods to increase the coins collected.
    public void collectCoin() {
        coins_collected += 1;
    }

    // Methods to decrease the crystals remaining.
    public void breakCrystal() {
        crystals_remaining -= 1;
        // Debug.Log(crystals_remaining);
    }
}
