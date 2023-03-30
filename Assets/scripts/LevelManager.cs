using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public float raceTime;

    private float prevTime;

    public TMP_Text timerText;

    public TMP_Text WinTimeRemaining;

    public TMP_Text racetimeAddition;

    public GameObject racetimeBox;

    private bool timerRunning = false;

    private bool levelStarted = false;
    private bool timeAdding = false;

    private bool goShaking = false;

    private Vector2 ogGoPos;
    public GameObject readysetgo;

    public AudioClip whistle;

    private AudioSource audioSource;
    private Animator animator;

    public AudioClip winSong;

    public AudioClip loseSong;

    public bool hasWon = false;
    public bool hasLost = false;

    public bool canContinue = false;

    public float deathHeight = -10f;



    // Start is called before the first frame update
    void Start()
    {

        ogGoPos = readysetgo.transform.position;

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        stopPlayerInput();

        // remove the mouse cursor
        Cursor.visible = false;

        if (GlobalVars.timeRemaining == 0) {
            racetimeBox.SetActive(false);
            timerText.text = raceTime.ToString("F1");

            Invoke("startReadySetGo", 1);
            
        } else {
            prevTime = GlobalVars.timeRemaining;

            racetimeAddition.text = "+" + raceTime.ToString("F1");
            timerText.text = prevTime.ToString("F1");

            Invoke("timeAdd", 1);
        }

    }

    void timeAdd() {
        timeAdding = true;
    }

    // Update is called once per frame
    void Update() {
        if (goShaking) {
            // shake the GO text
            Vector2 newPos = ogGoPos + new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            readysetgo.transform.position = newPos;
        }

        if (!levelStarted) {
            if (timeAdding) {
                // remove some raceTime from racetime and add it to the timer
                if (raceTime < 0.2f) {
                    // raceTime = 0;
                    // prevTime += raceTime;

                    raceTime += prevTime;
                    timerText.text = raceTime.ToString("F1");
                    racetimeAddition.text = "";

                    timeAdding = false;
                    racetimeBox.SetActive(false);

                    animator.Play("ready set go");
                    
                } else {
                    raceTime -= 0.2f;
                    prevTime += 0.2f;

                    timerText.text = prevTime.ToString("F1");
                    racetimeAddition.text = "+" + raceTime.ToString("F1");
                }
                
            }
            return;
        }

        if (timerRunning) {
            raceTime -= Time.deltaTime;
            timerText.text = raceTime.ToString("F1");

            if (raceTime <= 0) {
                raceTime = 0;
                timerText.text = "0.0";
                timerRunning = false;
                GameOver();
            }
        }

        if (canContinue) {
            // if the user presses space, go to the next level
            if (Input.GetKeyDown(KeyCode.Space)) {
                // TODO: transition?
                if (hasWon) {
                    // increment level number
                    GlobalVars.levelNumber += 1;
                    GlobalVars.timeRemaining = raceTime;
                } else if (hasLost) {
                    // reset the game
                    GlobalVars.levelNumber = -1;
                    GlobalVars.timeRemaining = 0;
                }
                SceneManager.LoadScene(0);
            }
        }
        
    }

    public void stopTimer() {
        timerRunning = false;
    }


    public void stopPlayerInput() {
        // find all "Player" objects and stop them
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
            if (p.GetComponent<Player>() != null) {
                p.GetComponent<Player>().stopInput = true;
            }
        }
    }

    public void startPlayerInput() {
        // find all "Player" objects and start them
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
            if (p.GetComponent<Player>() != null) {
                p.GetComponent<Player>().stopInput = false;
            }
        }
    }

    public void freezePlayer() {
        // find all "Player" objects and freeze them
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
            if (p.GetComponent<Player>() != null) {
                p.GetComponent<Player>().freeze();
            }
        }

    }

    public void WinLevel() {
        if (!hasWon) {
            hasWon = true;


            // stop timer
            timerRunning = false;
            GlobalVars.timeRemaining = raceTime;

            // stop player
            stopPlayerInput();

            // update the win screen
            WinTimeRemaining.text = raceTime.ToString("F1");

            // play win song
            audioSource.clip = winSong;
            audioSource.loop = false;
            audioSource.Play();
            
            // show win screen
            Invoke("showWin", 2);
        }
    }

    public void GameOver() {
        hasLost = true;

        // freeze the player
        freezePlayer();

        // white screen flash animation
        animator.Play("white screen flash");


        // play game over song
        audioSource.clip = loseSong;
        audioSource.loop = false;
        audioSource.Play();

        // show game over screen
        Invoke("showGameOver", 2);


    }

    void showWin() {
        animator.Play("GoalReached");
        canContinue = true;
    }

    void showGameOver() {
        animator.Play("TimeUp");
        canContinue = true;
    }
    
    public void OutOfTime() {

    }

    public void startReadySetGo() {
        animator.Play("ready set go");
    }


    public void StartGoShake() {
        goShaking = true;
        audioSource.PlayOneShot(whistle);
    }

    public void EndGoShake() {
        goShaking = false;
        timerRunning = true;
        levelStarted = true;
        startPlayerInput();
    }

}
