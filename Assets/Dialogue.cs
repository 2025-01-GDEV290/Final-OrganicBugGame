using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    public int Index = 0;
    public float DialogueSpeed;
    public TMP_Text dialogue;
    public TMP_Text NPCname;
    public GameObject dialogue_box;
    public GameObject wall;
    public GameObject spawn;
    public GameObject family1;
    public GameObject family2;
    public GameObject family3;
    public GameObject family1_auntie;
    public GameObject family2_auntie;
    public GameObject family3_auntie;
    public AudioSource quest_complete;

    public string item = "none";
    public string zone = "none";
    public string[] dialogue_lines;
    public int dialogue_num = 0;
    public int family_gathered = 0;

    public bool milkman_complete = false;
    public bool cow_complete = false;
    public bool paused = false;
    public bool writing = false;

    public InventoryUI inventory;

    private Movement playerMovement;
    private Rigidbody2D rb;

    void Start()
    {
        playerMovement = this.GetComponent<Movement>();
        inventory = FindObjectOfType<InventoryUI>();
        rb = this.GetComponent<Rigidbody2D>();


    }

    void Update()
    {
        if (Input.GetKeyDown("e") && !writing)
        {
            switch (zone)
            {
                case "Bucket":
                    item = "bucket";
                    dialogue_lines = new string[1] { "(You have picked up the bucket)" };
                    inventory.ShowEmptyBucket();
                    break;

                case "Cow":
                    if (!cow_complete)
                    {
                        if (item == "bucket")
                        {
                            cow_complete = true;
                            item = "milk";
                        }
                        else
                        {
                            dialogue_lines = new string[1] { "(You can't milk the cow without a bucket)" };
                        }
                    }
                    if (cow_complete)
                    {
                        dialogue_lines = new string[1] { "Moo! (You have milked the cow)" };
                        item = "milk";
                        inventory.ShowMilkBucket();
                    }
                    else
                    {
                        dialogue_lines = new string[1] { "(You can't milk the cow without a bucket)" };
                    }
                    break;

                case "Milkman":
                    if (!milkman_complete)
                    {
                        if (item == "milk")
                        {
                            milkman_complete = true;
                            quest_complete.Play();
                        }
                        else
                        {
                            dialogue_lines = new string[2] { "Oh! I'm so glad you're here.", "the cow needs to be milked. Can you help?" };
                        }
                    }
                    if (milkman_complete)
                    {
                        dialogue_lines = new string[3] { "Thank you!", "By the way, Auntie needs some help.", "Please go help her." };
                        item = "none";
                        inventory.ClearInventory();
                        wall.SetActive(false);
                    }
                    break;

                case "Auntie":
                    spawn.transform.position = new Vector2(29, 13);
                    if (family_gathered == 3)
                    {
                        quest_complete.Play();
                        dialogue_lines = new string[2] { "They're all here!", "Thank you!" };
                    }
                    else
                    {
                        dialogue_lines = new string[1] { "Hello please go find my family. It's picture time!" };
                    }
                    break;

                case "Family 1":
                    dialogue_lines = new string[1] { "Hi! Auntie needs me? Ok." };
                    break;

                case "Family 2":
                    dialogue_lines = new string[1] { "Hi! I'll head over to Auntie!" };
                    break;

                case "Family 3":
                    dialogue_lines = new string[1] { "Auntie? I'm on my way!" };
                    break;

                default:
                    Debug.Log("Nothing to interact with");
                    break;
            }

            if (!paused)
            {
                PauseGame();
            }

            if (paused)
            {
                if (dialogue_num == dialogue_lines.Length)
                {
                    PauseGame();
                }
                else
                {
                    NextSentence();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DialogueText.text = "Press E";
        dialogue_box.SetActive(true);
        zone = other.tag;
        NPCname.text = zone;
        Index = 0;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        dialogue_box.SetActive(false);
        zone = "none";
    }

    public void PauseGame()
    {
        if (paused)
        {
            paused = false;
            if (playerMovement != null)
            {
                rb.velocity = new Vector2(0,0);
                playerMovement.enabled = true;
            }
        }
        else
        {
            paused = true;
            if (playerMovement != null)
            {
                rb.velocity = new Vector2(0, 0);
                playerMovement.enabled = false;
            }
        }
    }

    void NextSentence()
    {
        if (Index <= dialogue_lines.Length - 1)
        {
            DialogueText.text = "";
            StartCoroutine(WriteSentence());
        }

        if (Index >= dialogue_lines.Length)
        {
            Index = 0;
            dialogue_box.SetActive(false);

            switch (zone)
            {
                case "Family 1":
                    family1.SetActive(false);
                    family_gathered++;
                    family1_auntie.SetActive(true);
                    break;

                case "Family 2":
                    family2.SetActive(false);
                    family_gathered++;
                    family2_auntie.SetActive(true);
                    break;

                case "Family 3":
                    family3.SetActive(false);
                    family_gathered++;
                    family3_auntie.SetActive(true);
                    break;
            }

            PauseGame();
        }
    }


    IEnumerator WriteSentence()
    {
        writing = true;
        foreach (char Character in dialogue_lines[Index].ToCharArray())
        {
            DialogueText.text += Character;
            yield return new WaitForSeconds(DialogueSpeed);
        }
        Index++;
        writing = false;
    }
}
