using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Can be attached to any button to start a unique line of 
// dialogue.



public class DialogueTrigger : MonoBehaviour
{
    public List<Dialogue> conversation;

    // Stores references to the 3 other story trigger panels.
    public GameObject other_story_1;
    public GameObject other_story_2;
    public GameObject other_story_3;

    // Stores a reference to the continue button for dialogue.
    public GameObject continue_button;

    // Stores a reference to the PlayerInfo object.
    public PlayerInfo playerInfo;

    // Affection level needed for the trigger to unlock.
    public int affection_needed;


    public void Start() {
        playerInfo = FindObjectOfType<PlayerInfo>();
    }

    public void TriggerDialogue() {

        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();

        // Only trigger the dialogue if it should be unlocked.
        if (doesStoryUnlock(dialogueManager)) {
            
            // Disactivate this game object (hide it).
            gameObject.SetActive(false);
            // Disactivate all the other buttons.
            other_story_1.SetActive(false);
            other_story_2.SetActive(false);
            other_story_3.SetActive(false);

            // Activate the continue button.
            continue_button.SetActive(true);

            // Start the whole conversation stored in 'dialogue'
            dialogueManager.StartConversation(conversation);
        }



    }


    // Decides whether the story should trigger or not.
    public bool doesStoryUnlock(DialogueManager dialogueManager) {
        return ((playerInfo.affection_bird == affection_needed 
                    && dialogueManager.npc_name == "bird") || 
                (playerInfo.affection_worm == affection_needed 
                    && dialogueManager.npc_name == "worm") || 
                (playerInfo.affection_croc == affection_needed 
                    && dialogueManager.npc_name == "croc")); 
    }


}
