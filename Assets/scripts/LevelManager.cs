using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public float time;

    public TMP_Text timerText;

    public TMP_Text WinTimeRemaining;

    private bool timerRunning = false;

    private AudioSource audioSource;
    private Animator animator;

    public AudioClip winSong;

    public bool hasWon = false;
    public bool canContinue = false;

    public float deathHeight = -10f;



    // Start is called before the first frame update
    void Start()
    {
        // TODO: Ready set go
        timerRunning = true;

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        time += GlobalVars.timeRemaining;

    }

    // Update is called once per frame
    void Update() {

        if (timerRunning) {
            time -= Time.deltaTime;
            timerText.text = time.ToString("F1");
        }

        if (canContinue) {
            // if the user presses space, go to the next level
            if (Input.GetKeyDown(KeyCode.Space)) {
                // TODO: transition?
                // increment level number
                GlobalVars.levelNumber += 1;
                GlobalVars.timeRemaining = time;
                SceneManager.LoadScene(0);
            }
        }
        
    }


    public void stopPlayer() {
        //GameObject player = GameObject.FindWithTag("Player");


        // find all "Player" objects and stop them
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
            if (p.GetComponent<Player>() != null) {
                p.GetComponent<Player>().stopInput = true;
            }
        }

    }

    public void WinLevel() {
        if (!hasWon) {
            hasWon = true;


            // stop timer
            timerRunning = false;
            GlobalVars.timeRemaining = time;

            // stop player
            stopPlayer();

            // update the win screen
            WinTimeRemaining.text = time.ToString("F1");

            // play win song
            audioSource.clip = winSong;
            audioSource.loop = false;
            audioSource.Play();
            
            // show win screen
            Invoke("showWin", 2);
        }
    }

    void showWin() {
        animator.Play("GoalReached");
        canContinue = true;
    }



    

    public void OutOfTime() {

    }

}
