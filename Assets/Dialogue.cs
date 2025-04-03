using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public TMP_Text dialogue;
    public GameObject dialogue_box; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag){
            case "Milkman":
                dialogue.text = "I am the milkman. MORE TEXT. MORE TEXT. There is so much text in here to test the text boxes so there will be LOTS OF TEXT BIIIIIG TEXT yes so much text so much text yes yes yes yes yes yes yes I am the milk man";
                dialogue_box.SetActive(true);
                break;
            
            case "Cow":
                dialogue.text= "Moo! (You can't milk the cow without a bucket)";
                dialogue_box.SetActive(true);
                break;
                
            default:
                dialogue.text = "";
                break;
        }

    }
    private void OnTriggerExit2D(Collider2D other) {
        dialogue_box.SetActive(false);
    }
}
