using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour {

    // Use this for initialization
    public GameObject[] pepperoni;
    public GameObject slice;
    bool colliding;
    bool immune = false; //if true, become immune for a few sec
    float timer = 0f;
    float sliceRadius;
    float pepRadius;
    float ticking; //how long the ship flashes for when hit
    bool tickState; //bool for checking if the ship is flashing or not
    void Start () {
        colliding = !colliding;
        sliceRadius = (slice.GetComponent<SpriteRenderer>().bounds.max.x - slice.GetComponent<SpriteRenderer>().bounds.min.x)/2;
        pepRadius = (pepperoni[0].GetComponent<SpriteRenderer>().bounds.max.y - pepperoni[0].GetComponent<SpriteRenderer>().bounds.min.y)/2;

    }
    
	// Update is called once per frame
	void Update () {

        BoundingCircle();

    }

    public bool BoundingCircle()
    {
        if(immune == true)
        {
            timer += Time.deltaTime;
            if (timer > 1.25f) //how long ship is immune for!
            {
                immune = false;
            }
        }
        else
        {
            timer = 0;
        }

        pepperoni = GameObject.FindGameObjectsWithTag("asteroid");
        colliding = false;
        for (int i = 0; i < pepperoni.Length; i++)
        {
            if (sliceRadius + pepRadius > (slice.transform.position - pepperoni[i].transform.position).magnitude) //if two radii added together is greater
                //than distance between them
            {
              if(immune == false)
                {
                    colliding = true;
                    immune = true;
                    GameObject.FindGameObjectWithTag("GUI").GetComponent<label>().numLives -= 1; //decrease lives when hit
                    Debug.Log("Colliding is: " + colliding);
                }
                
            }
        }
        
        if (immune)
        {
            ticking += Time.deltaTime;
            if (ticking >= 0.05) //how long it is in each state
            {
                if (tickState)
                {
                    slice.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.25f); //if ship hits asteroid, flash to 25% opacity
                    tickState = false;
                }
                else
                {
                    slice.GetComponent<SpriteRenderer>().color = Color.white;
                    tickState = true;
                }
                Debug.Log("Ticking is: " + tickState);
                ticking = 0f;

            }
        }
        else
        {
            slice.GetComponent<SpriteRenderer>().color = Color.white;
        }

        return colliding;
    }
}
