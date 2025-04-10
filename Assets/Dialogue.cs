using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public TMP_Text dialogue;
    public GameObject dialogue_box;

    public string item = "none";
    public string zone = "none";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   //Dialogue pn interacting with zone
        if (Input.GetKeyDown("e"))
        {
            switch (zone){
                case "Bucket":
                    item = "bucket";
                    dialogue.text = "(You have picked up the bucket)";
                    break;
                
                case "Cow":
                    if ( item == "bucket" )
                    {
                        item = "milk";
                        dialogue.text = "Moo! (You have milked the cow)";
                    } else {
                        dialogue.text = "(You can't milk the cow without a bucket)";
                    }
                    break;

                case "Milkman":
                    if (item == "milk") {
                        dialogue.text = "Thank you. I now have milk. You may proceed.";
                        item = "none";
                    }
                    break;
                
                default:
                    Debug.Log("Nothing to interact with");
                    break;
            }
        }
    }

    //Dialogue on approaching Dialogue zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        zone = other.tag;
        switch (other.tag){
            case "Milkman":
                if( item == "milk") { //If the player has milk
                    dialogue.text = "Please, give me the milk, bug.";
                } else {
                    dialogue.text = "I am the milkman. MORE TEXT. MORE TEXT. There is so much text in here to test the text boxes so there will be LOTS OF TEXT BIIIIIG TEXT yes so much text so much text yes yes yes yes yes yes yes I am the milk man";
                }
                dialogue_box.SetActive(true);
                break;
            
            case "Cow":
                dialogue.text = "Moo!";
                dialogue_box.SetActive(true);
                break;

            case "Bucket":
                dialogue.text = "(Press E to pick up the bucket)";
                dialogue_box.SetActive(true);
                break;
                
            default:
                dialogue.text = "";
                break;
        }

    }
    private void OnTriggerExit2D(Collider2D other) {
        dialogue_box.SetActive(false);
        zone = "none";
    }

}
