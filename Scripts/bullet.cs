using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
    //bullet speed
    //direction of bullet
    //timer
    public float bulletSpeed = 8f; //setting bullet speed to 8
    public Vector3 bulletDirection;
    public float timer;
    public float timerMax;
    public GameObject[] pepArray;
    float asteroidRadius; //radius of asteroids
    float bulletRadius; //radius of bullets
    public GameObject pepSmall; //first small pepperoni
    public GameObject pepSmall2; //second small pepperoni
    public GameObject pepSmall3; //third
    Vector3 prevPos; //previous position 


    // Use this for initialization
    void Start () {
        timer = 0f;
        timerMax = 2f;
        pepArray = GameObject.FindGameObjectsWithTag("asteroid"); //accessing all asteroid game objects, I added a tag of asteroid to all of them!
        bulletRadius = (gameObject.GetComponent<SpriteRenderer>().bounds.max.x - gameObject.GetComponent<SpriteRenderer>().bounds.min.x) / 2;
        
        //setting size of both radii!
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        timer += Time.deltaTime;
        if (timer > timerMax)
        {
            Destroy(gameObject); //destroy bullets if they are on the screen for too long!
        }

        BulletCollision();

	}

    private void Move()
    {
        transform.position += bulletDirection.normalized * bulletSpeed * Time.deltaTime;
        transform.up = bulletDirection.normalized;
        //bullet movement script, making it rotate!
    }
    public void BulletCollision()
    {
        pepArray = GameObject.FindGameObjectsWithTag("asteroid");

        for (int i = 0; i < pepArray.Length; i++)
        {
            asteroidRadius = (pepArray[i].GetComponent<SpriteRenderer>().bounds.max.x - pepArray[i].GetComponent<SpriteRenderer>().bounds.min.x) / 2;
            if (asteroidRadius + bulletRadius > (gameObject.transform.position - pepArray[i].transform.position).magnitude)
            {
                Debug.Log("collision hit");
                prevPos = pepArray[i].transform.position; //storing the position of the bigger asteroid so I can use it to place smaller ones
                
                if(pepArray[i].GetComponent<SpriteRenderer>().sprite.name == "pepperoni")
                {
                    Instantiate(pepSmall, prevPos, Quaternion.identity); //load small version of first piece of pepperoni
                    Instantiate(pepSmall, prevPos, Quaternion.identity); //load another
                    GameObject.FindGameObjectWithTag("GUI").GetComponent<label>().numPoints += 20; //increase points by 20
                }
                else if (pepArray[i].GetComponent<SpriteRenderer>().sprite.name == "pep2")
                {
                    Instantiate(pepSmall2, prevPos, Quaternion.identity); //load small version of second piece of pepperoni
                    Instantiate(pepSmall2, prevPos, Quaternion.identity); //load another
                    GameObject.FindGameObjectWithTag("GUI").GetComponent<label>().numPoints += 20; //increase points by 20
                }
               else if (pepArray[i].GetComponent<SpriteRenderer>().sprite.name == "pep3")
                {
                    Instantiate(pepSmall3, prevPos, Quaternion.identity); //load small version of third piece of pepperoni
                    Instantiate(pepSmall3, prevPos, Quaternion.identity); //load another
                    GameObject.FindGameObjectWithTag("GUI").GetComponent<label>().numPoints += 20; //increase points by 20
                }
                else
                {
                   
                    GameObject.FindGameObjectWithTag("GUI").GetComponent<label>().numPoints += 50; //add 50 for any other condition, in this case it was only
                    //when smaller ones are hit
                }

                
               
                Destroy(pepArray[i]); //destroy asteroids

                Destroy(gameObject); //destroy bullet if colliding

                //colliding = false;
            }
        }
    }
}
