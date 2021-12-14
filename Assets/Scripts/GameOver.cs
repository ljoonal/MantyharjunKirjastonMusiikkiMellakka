using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameOver : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(!FindObjectOfType<GameStateManager>().hasWon)
        {
			if (other.CompareTag("Chaser"))
			{
				FindObjectOfType<GameStateManager>().OnLose();
			}
		}
	}
}
