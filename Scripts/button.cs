using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class button : MonoBehaviour {

	public void Restart()
    {
        SceneManager.LoadScene("Asteroids"); //if play again button is clicked, restart game!
    }

}
