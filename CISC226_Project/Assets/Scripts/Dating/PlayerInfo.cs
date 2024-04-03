using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    // Tracks the player's affection level with each character.
    public int affection_worm = 0;
    public int affection_bird = 0;
    public int affection_croc = 0;

    // Tracks the stories read for all cahracters.
    public int stories_read_worm = 0;
    public int stories_read_bird = 0;
    public int stories_read_croc = 0;

    public List<string> tutLvl;

    public string currentScene = "lvl_1";
    
    public bool[] lvlwin = new bool[10];

    public int lvlPtr = 0;

    // Tracks the coins that the player has.
    public int coins = 0;

    public AudioSource map;


    private void Start() {
      
        // Make sure it does not get unloaded.
        DontDestroyOnLoad(gameObject);
        lvlwin[0] = true;
        tutLvl = new List<string>();

    }

    private void Update(){
        string scene = SceneManager.GetActiveScene().name;
        if (!map.isPlaying){
            if ((scene == "PreloadScene") || (scene == "CityMap") || (scene == "shop_tut") || (scene == "start_tut") || (scene == "lvl1_tut") || (scene == "lvl2_tut") || (scene == "lvl8_tut")){
                map.Play();
                Debug.Log("play");
            } else {
                map.Stop();
            }
        } else {
            if (!((scene == "PreloadScene") || (scene == "CityMap") || (scene == "shop_tut") || (scene == "start_tut") || (scene == "lvl1_tut") || (scene == "lvl2_tut") || (scene == "lvl8_tut"))){
                map.Stop();
            }
        }

    }
}
