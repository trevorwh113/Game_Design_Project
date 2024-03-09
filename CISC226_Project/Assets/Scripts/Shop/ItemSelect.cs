using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{
    // Reference to the player info (for affection levels).
    private PlayerInfo playerInfo;
    
    // Reference to the textbox name and item fields.
    public TextMeshProUGUI text_field;
    public TextMeshProUGUI name_field;

    public GameObject confirm_button;


    // The name, image and cost on the button.
    public TextMeshProUGUI button_name;
    public TextMeshProUGUI button_cost;

    public GameObject button_icon;

    public List<ItemInfo> item_list;

    public int state_index = 0;

    // Determines which affection state to look at.
    public string npc;


    
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the player info instance.
        playerInfo = FindObjectOfType<PlayerInfo>();

        // Determine the state the button is in and update it.
        DetermineButtonState();
        UpdateButtonState();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void UpdateButtonState() {
        // Updates the text and graphics on the button with the information
        // in the current item (state index).
        if (state_index < item_list.Count) {
            button_name.text = item_list[state_index].name;
            button_cost.text = "" + item_list[state_index].cost; 
            button_icon.GetComponent<UnityEngine.UI.Image>().sprite = item_list[state_index].icon; 
        }
        else {
            // Null out the icons on the button.
            button_name.text = "";
            button_cost.text = "";
            // Add a out-of-stock srpite.
        }
    }

    public void IntiatePurchase() {
        // The on-click method that puts the item name in the dispaly field.
        
        if (state_index < item_list.Count) {
            name_field.text = item_list[state_index].name;
            text_field.text = item_list[state_index].description + "\nPrice: " + item_list[state_index].cost; 

            // Show the confirm button.
            confirm_button.SetActive(true);
        }
        else {
            // Print an out-of-stock message.
            name_field.text = "";
            text_field.text = "Out of stock.";
            confirm_button.SetActive(false);
        }

        // Tell the confirm button which was the most recent button pressed.
        confirm_button.GetComponent<ConfirmPurchase>().most_recent = gameObject.GetComponent<ItemSelect>();


        // Testing // ----------------
        // state_index++;
        
        
    }

    public void DetermineButtonState() {
        if (playerInfo != null) {

            // Use the affection to set the correct button state.
            if (npc == "worm") {
                state_index = playerInfo.affection_worm;
            }
            else if (npc == "bird") {
                state_index = playerInfo.affection_bird;
            }
            else if (npc == "croc") {
                state_index = playerInfo.affection_croc;
            }
            else {
                state_index = 0;
            }



        }
    }
}
