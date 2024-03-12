using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmPurchase : MonoBehaviour
{
    // Reference to the player info (for affection levels).
    private PlayerInfo playerInfo;
    
    // Reference to the textbox name and item fields.
    public TextMeshProUGUI text_field;
    public TextMeshProUGUI name_field;

    // Reference to the three item select buttons.
    public ItemSelect worm_item_button;
    public ItemSelect bird_item_button;
    public ItemSelect croc_item_button;

    // The item that was most recently selected.
    public ItemSelect most_recent;


    
    // Start is called before the first frame update
    void Start()
    {
        playerInfo = FindObjectOfType<PlayerInfo>();

    }

    public void Buy() {
        // THe on-click button that makes a succesful/unsuccesful purchase.
        if (playerInfo != null && most_recent != null) {

            // If the player has enough coins, purchase.
            int cost = most_recent.item_list[most_recent.state_index].cost;
            if (playerInfo.coins >= cost) {
                
                // Subtract from the coins.
                playerInfo.coins -= cost;

                // Update the text and name.
                text_field.text = "Purchased!";
                name_field.text = "";

                // Update the affection value.
                if (most_recent.Equals(worm_item_button)) {               
                    playerInfo.affection_worm += 1;
                }
                else if (most_recent.Equals(bird_item_button)) {               
                    playerInfo.affection_bird += 1;
                }
                else if (most_recent.Equals(croc_item_button)) {               
                    playerInfo.affection_croc += 1;
                }

                // Update the recently clicked button.
                most_recent.DetermineButtonState();
                most_recent.UpdateButtonState();

                // Hide the button.
                gameObject.SetActive(false);

            }
            // The player does not have enough coins
            else {
                text_field.text = "You do not have enough coins...";
                name_field.text = "";
                gameObject.SetActive(false);
            }

        }
    }
}
