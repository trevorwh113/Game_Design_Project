using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Can be attached to any button to start a unique line of 
// dialogue.



public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue() {

        // Start the dialogue stored in 'dialogue'
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
