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

    // Number of previous stories
    public int num_previous_stories;


    public void Start() {
        playerInfo = FindObjectOfType<PlayerInfo>();
    }

    public void TriggerDialogue() {

        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();

        // Avoids issues when not starting in the right scene.
        if (playerInfo != null && dialogueManager != null) {
            
            
            // Only trigger the dialogue if it should be unlocked.
            if (doesStoryUnlock(dialogueManager) && previousStoriesRead(dialogueManager)) {
                // Disactivate thisk game object (hide it).
                gameObject.SetActive(false);
                // Disactivate all the other buttons.
                other_story_1.SetActive(false);
                other_story_2.SetActive(false);
                other_story_3.SetActive(false);

                // Activate the continue button.
                continue_button.SetActive(true);

                // Start the whole conversation stored in 'dialogue'
                dialogueManager.StartConversation(conversation, gameObject.GetComponent<DialogueTrigger>());
            }

        }
        
        


    }


    // Decides whether the story should trigger or not based on affection (items).
    public bool doesStoryUnlock(DialogueManager dialogueManager) {
        if (playerInfo != null) {
            return (
                (playerInfo.affection_bird >= affection_needed 
                    && dialogueManager.npc_name == "bird") || 
                (playerInfo.affection_worm >= affection_needed 
                    && dialogueManager.npc_name == "worm") || 
                (playerInfo.affection_croc >= affection_needed 
                    && dialogueManager.npc_name == "croc")); 
        }
        
        else {
            return false;
        }
    }

    // Decides whether the story should trigger or not based on stories read.
    public bool previousStoriesRead(DialogueManager dialogueManager) {
        if (playerInfo != null) {
            return (
                (playerInfo.stories_read_bird >= num_previous_stories
                    && dialogueManager.npc_name == "bird") || 
                (playerInfo.stories_read_worm >= num_previous_stories 
                    && dialogueManager.npc_name == "worm") || 
                (playerInfo.stories_read_croc >= num_previous_stories 
                    && dialogueManager.npc_name == "croc")); 
        } 
        else {
            return false;
        }
    }


}
