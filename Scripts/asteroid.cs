using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour {

    // Use this for initialization
    public Vector3 asteroidDirection;
    public Vector3 asteroidSpeed;
    Camera cam;
    float camWidth;
    float camHeight;

	void Start () {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2f;
        camWidth = camHeight * cam.aspect;
        asteroidSpeed = new Vector3(Random.Range(-camWidth, camWidth), Random.Range(-camHeight, camHeight)) * Time.deltaTime; //setting speed of asteroids
        asteroidSpeed = Vector3.ClampMagnitude(asteroidSpeed, 0.05f); //stopping speed from getting too high
    }
	
	// Update is called once per frame
	void Update () {
        AsteroidMove();
        WrapAsteroid();
	}

    void AsteroidMove()
    {
      
        transform.position += asteroidSpeed;
       
    }
    void WrapAsteroid()
    {
        if (transform.position.x < cam.transform.position.x -camWidth / 2 - 1)
        {
            transform.position = new Vector3(cam.transform.position.x + camWidth / 2, transform.position.y, 0);
        }
        else if (transform.position.x > cam.transform.position.x + camWidth / 2 + 1)
        {
            transform.position = new Vector3(cam.transform.position.x - camWidth / 2, transform.position.y, 0);
        }

        if (transform.position.y < cam.transform.position.y - camHeight / 2 - 1)
        {
            transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + camHeight / 2, 0);
        }
        else if (transform.position.y > cam.transform.position.y + camHeight / 2 + 1)
        {
            transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - camHeight / 2, 0);
        }
    }
}
