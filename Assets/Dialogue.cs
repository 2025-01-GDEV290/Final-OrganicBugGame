using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public TextMeshProUGUI DialogueText;
    private int Index = 0;
    public float DialogueSpeed;
    public TMP_Text dialogue;
    public TMP_Text name;
    public GameObject dialogue_box;

    public string item = "none";
    public string zone = "none";
    public string[] dialogue_lines;
    public int dialogue_num = 0;

    public bool milkman_complete = false;
    public bool cow_complete = false;
    public bool paused = false;
    public bool writing = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   //Dialogue on interacting with zone
        if (Input.GetKeyDown("e") && !writing)
        {
            switch (zone){
                case "Bucket":
                    item = "bucket";
                    dialogue_lines = new string[1] {"(You have picked up the bucket)"};
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
                    }
                    
                    break;

                case "Milkman":
                    if (!milkman_complete)
                    {
                        if (item == "milk")
                        {
                            milkman_complete = true;
                        }
                        else
                        {
                            dialogue_lines = new string[2] {"I am the milk man", "bring me milk, bug"};
                        }
                    }
                    if (milkman_complete)
                    {
                        dialogue_lines = new string[3] {"Thank you", "I now have milk.", "You may proceed." };
                    }                 
                break;
                
                default:
                    Debug.Log("Nothing to interact with");
                    break;
            }
            if (!paused)
            {
                PauseGame();
            }
            if(paused)
            {
                if(dialogue_num == dialogue_lines.Length)
                {
                    PauseGame();
                    //dialogue_num = 0;
                } else {
                    NextSentence();
                    //dialogue.text = dialogue_lines[dialogue_num];
                    //if (dialogue_num < dialogue_lines.Length)
                    //{
                    //    dialogue_num++;
                    //}   
                }   
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

    public void PauseGame()
    {
        if (paused)
        {
            //Time.timeScale = 1;
            paused = false;
            //dialogue_box.SetActive(false);
            //zone = "none";
        } else
        {
            //Time.timeScale = 0;
            paused = true;
        }
    }

    void NextSentence()
    {
        if(Index <= dialogue_lines.Length - 1)
        {
            DialogueText.text = "";
            StartCoroutine(WriteSentence());

        } else {
            Index = 0;
            DialogueText.text = "";
            StartCoroutine(WriteSentence());
        }
    }

    IEnumerator WriteSentence()
    {
        writing = true;
        foreach(char Character in dialogue_lines[Index].ToCharArray())
        {
            DialogueText.text += Character;
            yield return new WaitForSeconds(DialogueSpeed);
        }
        Index++;
        writing = false;
    }
}