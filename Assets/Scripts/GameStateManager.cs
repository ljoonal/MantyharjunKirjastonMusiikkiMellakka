using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/** Manages the game's state when playing. So
  * So keeps track off of current score for example.
  */
public class GameStateManager : MonoBehaviour
{
	private float time = 0;
	//private float remainingtime = 60;
	public InputField TimeCounter;
	private float collectedInstruments = 0;
	void FixedUpdate()
	{
		time += Time.deltaTime;
		//remainingtime = remainingtime - time;
		TimeCounter.text = time.ToString();
	}

	public void OnInstrumentColleted(Instrument instrument)
	{
		Debug.Log("Player has collected " + instrument.instrument.ToString());
		collectedInstruments++;
	}
}

