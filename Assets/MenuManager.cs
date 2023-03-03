using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject levelMenu;

    public TMP_Text levelName;
    public TMP_Text levelDescription;


    // Start is called before the first frame update
    void Start()
    {
        if (GlobalVars.levelNumber > -1) {
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
        Application.Quit();
    }

    public void updateLevelMenu() {
        levelName.text = "Next Up: " + levelNames[GlobalVars.levelNumber];
        levelDescription.text = levelDescriptions[GlobalVars.levelNumber];
    }


    public string[] levelNames = {"Warm-Up Returns Race", "Speed Race", "Food Race", "Outside Race", "AMOGUS RACE"};

    public string[] levelDescriptions = {"Warm-Up Race returns, but this time better looking than ever! Your remaining time carries over to the next level, so make sure to go as quickly as possible!",
    "In this race, you will encounter boost panels that speed you very quickly. Make sure to use them to your advantage!",
    "Eat all the burgers you see in this calorie-filled level! As you eat more, you will get fatter and slower, but it might be worth it...",
    "No more are we rolling through colorful abstract levels. This time, we are rolling through the outside world! Watch out for the cars!",
    "In this sussy race, you will figure out who the IMPOSTER is! hopefully it's not you... sussy baka"};
}
