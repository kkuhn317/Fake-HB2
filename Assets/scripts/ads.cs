using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ads : MonoBehaviour
{

    public GameObject ad;

    int adShowing = -1;

    public List<Sprite> adImages;
    public List<Sprite> adCloseImages;

    public List<int> adShowTimes;

    private LevelManager levelManager;

    public bool canClose = false;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = transform.parent.GetComponent<LevelManager>();

        for (int i = 0; i < adShowTimes.Count; i++) {
            StartCoroutine(InvokeRealtime(showAd, adShowTimes[i]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if canClose and space is pressed, close ad
        if (canClose && Input.GetKeyDown(KeyCode.Space)) {
            closeAd();
        }
    }

    public void showAd() {
        adShowing += 1;

        ad.SetActive(true);
        ad.GetComponent<Image>().sprite = adImages[adShowing];

        canClose = false;

        Time.timeScale = 0;

        StartCoroutine(InvokeRealtime(showCloseAd, 5.0f));
    }

    public void showCloseAd() {
        ad.GetComponent<Image>().sprite = adCloseImages[adShowing];
        canClose = true;
    }

    public void closeAd() {
        ad.SetActive(false);
        Time.timeScale = 1;
        canClose = false;
    }

    
    public IEnumerator InvokeRealtime( System.Action action, float Delay)
    {
        yield return new WaitForSecondsRealtime(Delay);
        if (action != null)
            action();
    }
}
