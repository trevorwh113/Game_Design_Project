using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Can be attached to any button to start a unique line of 
// dialogue.



public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    // Stores references to the 3 other story trigger panels.
    public GameObject other_story_1;
    public GameObject other_story_2;
    public GameObject other_story_3;

    // Stores a reference to the continue button for dialogue.
    public GameObject continue_button;

    // Stores a reference to the character we are talking to.
    public NPC character;

    // Stores a reference to the PlayerInfo object.
    public PlayerInfo playerInfo;


    public void TriggerDialogue() {

        // Disactivate this game object (hide it).
        gameObject.SetActive(false);
        // Disactivate all the other buttons.
        other_story_1.SetActive(false);
        other_story_2.SetActive(false);
        other_story_3.SetActive(false);

        // Activate the continue button.
        continue_button.SetActive(true);

        // Start the dialogue stored in 'dialogue'
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

    }

    // private void unlockDialogue(GameObject storyTrigger, PlayerInfo playerInfo, int condition) {
    //     if (playerInfo.)

    // }


}
