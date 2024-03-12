using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class lvlButton : MonoBehaviour
{

    private PlayerInfo playerInfo;
    private ButtonGoToScene goTo;
    private string curScene;

    [SerializeField] private int myPtr;
    // Start is called before the first frame update
    void Start()
    {
        playerInfo = FindObjectOfType<PlayerInfo>();
        if (playerInfo.lvlwin[myPtr] == true){
                this.gameObject.GetComponent<Button>().interactable = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


}
