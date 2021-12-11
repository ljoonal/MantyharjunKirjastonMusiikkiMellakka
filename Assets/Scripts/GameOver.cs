using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameOver : MonoBehaviour
{
	public GameObject UIObject;
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Chaser"))
		{
			Time.timeScale = 0;
			AudioListener.pause = true;
			UIObject.SetActive(true);
			StartCoroutine(GameOverCounter());
			IEnumerator GameOverCounter()
			{
				for (; ; )
				{
					yield return new WaitForSecondsRealtime(5.0f);
					Time.timeScale = 1;
					AudioListener.pause = false;
					FindObjectOfType<GlobalStateManager>().Scoreboard();
				}
			}
		}
	}
}
