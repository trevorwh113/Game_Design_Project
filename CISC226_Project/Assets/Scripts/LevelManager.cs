using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // The properties unique to each level, such as number of coins,
    // crystals, enemies, etc.

    public int crystals_remaining;      // Set for the level.
    public int coins_collected = 0;     // Should default to always.

    // Indicates the string to transition to on a win.
    public string win_scene;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Testing reasons.
        if (Input.GetKeyDown(KeyCode.E)) {
            collectCoin();
            breakCrystal();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            ResetLevel();
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            WinLevel();
        }
    }


    // Method to reset the scene.
    public void ResetLevel() {
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
    }
}
