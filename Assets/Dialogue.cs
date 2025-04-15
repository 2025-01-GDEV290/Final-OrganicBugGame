using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public TMP_Text dialogue;
    public TMP_Text name;
    public GameObject dialogue_box;

    public string item = "none";
    public string zone = "none";

    public bool milkman_complete = false;
    public bool cow_complete = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   //Dialogue on interacting with zone
        if (Input.GetKeyDown("e"))
        {
            switch (zone){
                case "Bucket":
                    item = "bucket";
                    dialogue.text = "(You have picked up the bucket)";
                    break;
                
                case "Cow":
                if (!cow_complete) 
                {
                    if ( item == "bucket" )
                    {
                        cow_complete = true;
                        item = "milk";
                    } else {
                        dialogue.text = "(You can't milk the cow without a bucket)";
                    }
                }
                if(cow_complete) {
                    dialogue.text = "Moo! (You have milked the cow)";
                }
                    break;

                case "Milkman":
                    if (!milkman_complete)
                    {
                        if (item == "milk"){
                            milkman_complete = true;
                        } else {
                            dialogue.text = "I am the milkman. MORE TEXT. MORE TEXT. There is so much text in here to test the text boxes so there will be LOTS OF TEXT BIIIIIG TEXT yes so much text so much text yes yes yes yes yes yes yes I am the milk man";
                        }
                    }
                    if (milkman_complete)
                    {
                        dialogue.text = "Thank you. I now have milk. You may proceed.";
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
        dialogue.text = "Press E";
        dialogue_box.SetActive(true);
        zone = other.tag;
        name.text = zone;
    }
    private void OnTriggerExit2D(Collider2D other) {
        dialogue_box.SetActive(false);
        zone = "none";
    }

}
