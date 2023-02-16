using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform player;
    public Transform player2;
    public Vector3 offset;
    public float scale = 1;
    public int camChoice = 1;
    public GameObject keybindList;
    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.K))
        {
            if (keybindList.activeSelf == false)
            {
                keybindList.SetActive(true);
            }
            else if (keybindList.activeSelf == true)
            {
                keybindList.SetActive(false);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            camChoice = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            camChoice = 2;
            print("choice 2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            camChoice = 3;
            print("choice 3");
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            scale -= Input.GetAxis("Mouse ScrollWheel");
        }
        scale = Mathf.Clamp(scale,0.05f, 7.5f);
        Vector3 pos1 = player.position;
        Vector3 pos2 = player2.position;
        Vector3 campos = new Vector3(0, 0, 0);
        if (camChoice == 1)
        {
            campos = (pos1 + pos2) / 2;
        }
        else if (camChoice == 2)
        {
            campos = pos1;
        }
        else if (camChoice == 3) 
        {
            campos = pos2;
        }
        transform.position = campos + offset * scale;
        // if pressing r key restart scene+
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.mute = !audio.mute;
        }
    }
}
