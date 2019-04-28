using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class label : MonoBehaviour {

    // Use this for initialization
    public int numPoints;
    public int numLives;
	void Start () {
        numLives = 3;
        numPoints = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        GUI.skin.box.wordWrap = true;
        GUI.Box(new Rect(5, 0, 200, 40), "Points: " + numPoints); //number of points 
        GUI.skin.box.fontSize = 30;

        GUI.skin.box.wordWrap = true;
        GUI.Box(new Rect(5, 45, 200, 40), "Lives: " + numLives); //num lives
        GUI.skin.box.fontSize = 30;
    }
}
