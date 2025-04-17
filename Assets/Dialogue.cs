using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TMP_Text dialogue;
    public GameObject dialogue_box;
    public GameObject player; // Drag the player into this in Inspector

    private string currentItem = "none";
    private string currentZone = "none";
    private bool canInteract = false;
    private bool dialogueActive = false;

    private Movement movementScript; // <-- Replace with actual name of your movement script

    void Start()
    {
        if (player != null)
        {
            movementScript = player.GetComponent<Movement>();
        }
    }

    void Update()
    {
        if (canInteract && !dialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            HandleInteraction(currentZone);
            dialogueActive = true;

            if (movementScript != null)
            {
                movementScript.enabled = false;
                Debug.Log("Movement script DISABLED.");
            }
        }
        else if (dialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            dialogue_box.SetActive(false);
            dialogueActive = false;

            if (movementScript != null)
            {
                movementScript.enabled = true;
                Debug.Log("Movement script ENABLED.");
            }
        }
    }

    void HandleInteraction(string zone)
    {
        switch (zone)
        {
            case "Bucket":
                currentItem = "bucket";
                dialogue.text = "(You have picked up the bucket)";
                break;

            case "Cow":
                if (currentItem == "bucket")
                {
                    currentItem = "milk";
                    dialogue.text = "Moo! (You have milked the cow)";
                }
                else
                {
                    dialogue.text = "(You can't milk the cow without a bucket)";
                }
                break;

            case "Milkman":
                if (currentItem == "milk")
                {
                    dialogue.text = "Thank you. I now have milk. You may proceed.";
                    currentItem = "none";
                }
                else
                {
                    dialogue.text = "I am the milkman. MORE TEXT. I need milk.";
                }
                break;

            default:
                dialogue.text = "";
                break;
        }

        dialogue_box.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentZone = other.tag;
        canInteract = true;

        if (other.tag == "Bucket")
        {
            dialogue.text = "(Press E to pick up the bucket)";
            dialogue_box.SetActive(true);
        }
        else if (other.tag == "Cow")
        {
            dialogue.text = "Moo!";
            dialogue_box.SetActive(true);
        }
        else if (other.tag == "Milkman")
        {
            dialogue.text = "(Press E to talk)";
            dialogue_box.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == currentZone)
        {
            canInteract = false;
            currentZone = "none";
            dialogue_box.SetActive(false);

            if (movementScript != null)
            {
                movementScript.enabled = true;
                Debug.Log("Movement script ENABLED on exit.");
            }

            dialogueActive = false;
        }
    }
}
