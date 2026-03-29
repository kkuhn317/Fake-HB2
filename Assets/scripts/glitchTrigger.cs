using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class glitchTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // disable renderer
        //GetComponent<Renderer>().enabled = false;

        music = levelManager.GetComponent<AudioSource>();

    }

    public bool glitchMode = false;
    public bool rickrolled = false;

    private float musicTime;

    public GameObject rickroll;
    public GameObject prompt;
    public GameObject dialogue;

    public int dialogueIndex = -1;

    private int loops = 0;
    
    public GameObject levelManager;

    private AudioSource music;

    // Update is called once per frame
    void Update()
    {
        if (glitchMode) {
            if (music.time >= musicTime) {
                // skip back 0.5 seconds
                music.time = musicTime - 0.5f;
                loops++;
                if (loops >= 10) {
                    // if we've looped 10 times, rickroll the player
                    RickRoll();
                }
            }
        }

        if (rickrolled) {
            // if the player has been rickrolled, check if they press the U key
            if (Input.GetKeyDown(KeyCode.Space)) {
                nextScreen();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the player enters the trigger
        if (other.tag == "Player") {
            glitchMode = true;
            // first, freeze the entire game
            Time.timeScale = 0;

            // skip the music back over and over
            musicTime = music.time;
        }
    }

    private void RickRoll() {
        // unfreeze the game
        Time.timeScale = 1;
        glitchMode = false;

        // freeze the player
        levelManager.GetComponent<LevelManager>().freezePlayer();

        // stop the music
        music.Stop();

        // completely remove the level manager
        levelManager.SetActive(false);

        // play the rickroll
        rickroll.SetActive(true);

        // show the prompt after 10 seconds
        Invoke("ShowPrompt", 10f);
    }

    private void ShowPrompt() {
        prompt.SetActive(true);
        rickrolled = true;
    }

    public void nextScreen() {
        if (dialogueIndex >= 0) {
            dialogue.transform.GetChild(dialogueIndex).gameObject.SetActive(false);
        } else {
            // remove rickroll
            rickroll.SetActive(false);

            // play the dialogue song
            GetComponent<AudioSource>().Play();
        }

        dialogueIndex++;

        if (dialogueIndex >= dialogue.transform.childCount) {
            // if we've reached the end of the dialogue, load the main menu
            GlobalVars.levelNumber = 999;
            SceneManager.LoadScene(0);
        } else {
            // otherwise, show the next dialogue
            dialogue.transform.GetChild(dialogueIndex).gameObject.SetActive(true);
        }
    }


}
