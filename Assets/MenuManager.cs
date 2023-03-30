using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject levelMenu;

    public Button editorButton;

    public AudioClip creakSound;

    public TMP_Text levelName;
    public TMP_Text levelDescription;

    public GameObject levels;

    public TMP_Text timePool;

    // Start is called before the first frame update
    void Start()
    {

        // make the mouse visible
        Cursor.visible = true;

        if (GlobalVars.levelNumber > 10) {
            // game won
            editorButton.interactable = true;
            editorButton.gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        } else if (GlobalVars.levelNumber > -1) {
            mainMenu.SetActive(false);
            levelMenu.SetActive(true);
            updateLevelMenu();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        GlobalVars.levelNumber = 0;
        GlobalVars.timeRemaining = 0;
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
        updateLevelMenu();
    }

    public void backToMenu()
    {
        GlobalVars.levelNumber = -1;
        GlobalVars.timeRemaining = 0;
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void quitGame()
    {
        print("quit game");
        Application.Quit();
    }

    public void updateLevelMenu() {
        levelName.text = "Next Up: " + levelNames[GlobalVars.levelNumber];
        levelDescription.text = levelDescriptions[GlobalVars.levelNumber];

        timePool.text = GlobalVars.timeRemaining.ToString("F1");

        // flash the level that is next up
        GameObject nextup = levels.transform.GetChild(GlobalVars.levelNumber).gameObject;
        nextup.GetComponent<Animator>().Play("levelflash");

        // for the levels after, play levelhide animation
        for (int i = GlobalVars.levelNumber + 1; i < levels.transform.childCount; i++) {
            GameObject level = levels.transform.GetChild(i).gameObject;
            level.GetComponent<Animator>().Play("levelhide");
        }
    }

    // Level Menu buttons
    public void nextLevel() {
        // go to the level with the scene number of GlobalVars.levelNumber + 1 (because menu is 0)
        SceneManager.LoadScene(GlobalVars.levelNumber + 1);
    }

    public void onEditorScreenclick() {
        // play creak sound
        AudioSource audioSource = Camera.main.gameObject.GetComponent<AudioSource>();
        audioSource.volume = 1f;
        audioSource.PlayOneShot(creakSound);
        audioSource.PlayOneShot(creakSound);
        audioSource.PlayOneShot(creakSound);
        Invoke("quitGame", 0.75f);
    }


    private string[] levelNames = {"Warm-Up Race", "Food Race", "Outside Race", "AMOGUS RACE"};

    private string[] levelDescriptions = {"Warm-Up Race returns, but this time better looking than ever! Your remaining time carries over to the next level, so make sure to go as quickly as possible!",
    "Eat all the burgers you see in this calorie-filled level! As you eat more, you will get fatter and slower, but it might be worth it...",
    "No more are we rolling through colorful abstract levels. This time, we are rolling through the outside world! Watch out for the cars!",
    "In this sussy race, you will figure out who the IMPOSTER is! hopefully it's not you... sussy baka"};
}
