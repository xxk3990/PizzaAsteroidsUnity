using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ending : MonoBehaviour {

    // Use this for initialization
    GameObject[] pepArray;
    Camera cam;
    float camWidth;
    float camHeight;
    public GameObject pep1;
    public GameObject pep2;
    public GameObject pep3;
    GameObject pizzaLabel;
    void Start () {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2f;
        camWidth = camHeight * cam.aspect;
    }
	
	// Update is called once per frame
	void Update () {
        pepArray = GameObject.FindGameObjectsWithTag("asteroid");
        if(pepArray.Length == 0)
        {
            Instantiate(pep1, new Vector3(Random.Range(-camWidth, camWidth), Random.Range(-camHeight, camHeight)), Quaternion.identity);
            Instantiate(pep2, new Vector3(Random.Range(-camWidth, camWidth), Random.Range(-camHeight, camHeight)), Quaternion.identity);
            Instantiate(pep3, new Vector3(Random.Range(-camWidth, camWidth), Random.Range(-camHeight, camHeight)), Quaternion.identity);
            //spawn more in random locations if no asteroids left!
        }
        pizzaLabel = GameObject.FindGameObjectWithTag("GUI");
        if(pizzaLabel.GetComponent<label>().numLives == 0)
        {
            SceneManager.LoadScene("GameOver");
            //if num of lives reaches 0, load game over scene
        }

    }
}
