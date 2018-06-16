using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class talkWithMe : MonoBehaviour
{

    public TextAsset dialogueFileHaventWon; //the .txt file for dialogue the monk says if the player hasn't won it
    public TextAsset dialogueFileWon; //the .txt file 
    public string[] myDialogue;
    public string[] myDialogueWon;
    int currentLine;

    //fading script for scene transition
    Fading fade;

    //how this monk talks to ScoreKeeper
    public ScoreKeeper score;
    public int myLevel = 1;

    public overworldPlayerController playerController;
    public GameObject talkToMe;
    public GameObject dialogueBox;
    public TextMesh theText;
    bool canTalk;
    bool canMeditate;

    public string levelToLoad; //write in the name of the scene to load that level with this monk

    void Start()
    {
        score = FindObjectOfType<ScoreKeeper>();
        fade = FindObjectOfType<Fading>();

        playerController = GameObject.Find("Player").GetComponent<overworldPlayerController>();
        if (dialogueFileHaventWon)
        {
            myDialogue = (dialogueFileHaventWon.text.Split(';'));
            currentLine = -1;
        }
        if (dialogueFileWon)
        {
            myDialogueWon = (dialogueFileWon.text.Split(';'));
            currentLine = -1;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
        }
        if (canTalk && Input.GetKeyDown(KeyCode.Space))
        {
            playerController.playerState = overworldPlayerController.PlayerState.Talking;


            if (currentLine < myDialogue.Length - 1)
            {
                currentLine++;
                talkToMe.SetActive(false);

                if (!score.CheckIfLevelWon(myLevel)) //if the player has not won the level, have the monk tell them to start the level
                {
                    theText.text = myDialogue[currentLine];
                }
                else
                {
                    theText.text = myDialogueWon[currentLine];
                }
            }
            else
            {
                playerController.playerState = overworldPlayerController.PlayerState.Walking;
                currentLine = -1;
                dialogueBox.SetActive(false);
                canTalk = false;
            }

            canMeditate = true;
            dialogueBox.SetActive(true);
        }
        if (canTalk == false)
        {
            talkToMe.SetActive(false);
        }
        if (!score.CheckIfLevelWon(myLevel) && canMeditate && Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(LoadNextScene());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        talkToMe.SetActive(true);
        canTalk = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        dialogueBox.SetActive(false);
        canTalk = false;
        canMeditate = false;
    }

    IEnumerator LoadNextScene()
    {
        fade.BeginFade(1);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
    }
}
