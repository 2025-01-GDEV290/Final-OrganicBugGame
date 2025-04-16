using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TMP_Text dialogue;
    public GameObject dialogue_box;
    public GameObject Player; // <-- manually assign this in Inspector

    private bool playerInRange = false;
    private bool dialogueActive = false;
    private string currentDialogue = "";

    private Movement playerMovement;

    private void Start()
    {
        if (Player != null)
        {
            playerMovement = Player.GetComponent<Movement>();
            Debug.Log("Player movement component found.");
        }
        else
        {
            Debug.LogWarning("Player reference not assigned in the inspector!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed.");
        }

        if (playerInRange)
        {
            Debug.Log("Player is in range.");
        }

        if (playerInRange && !dialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Starting dialogue...");

            dialogue.text = currentDialogue;
            dialogue_box.SetActive(true);
            dialogueActive = true;

            if (playerMovement != null)
            {
                playerMovement.enabled = false;
                Debug.Log("Player movement disabled.");
            }
        }
        else if (dialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Ending dialogue...");

            dialogue_box.SetActive(false);
            dialogueActive = false;

            if (playerMovement != null)
            {
                playerMovement.enabled = true;
                Debug.Log("Player movement re-enabled.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D with: " + other.gameObject.name);

        if (other.gameObject == Player)
        {
            Debug.Log("Player entered dialogue trigger.");
            playerInRange = true;

            switch (gameObject.tag)
            {
                case "Milkman":
                    currentDialogue = "I am the milkman...";
                    break;

                case "Cow":
                    currentDialogue = "Moo! (You can't milk the cow without a bucket)";
                    break;

                default:
                    currentDialogue = "";
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D with: " + other.gameObject.name);

        if (other.gameObject == Player)
        {
            Debug.Log("Player exited dialogue trigger.");
            playerInRange = false;

            if (dialogueActive)
            {
                Debug.Log("Closing dialogue box due to player exit.");
                dialogue_box.SetActive(false);
                dialogueActive = false;

                if (playerMovement != null)
                {
                    playerMovement.enabled = true;
                    Debug.Log("Player movement re-enabled.");
                }
            }
        }
    }
}
