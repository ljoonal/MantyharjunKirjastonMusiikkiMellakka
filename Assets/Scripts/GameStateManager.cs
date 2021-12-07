using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/** Manages the game's state when playing. So
  * So keeps track off of current score for example.
  */
public class GameStateManager : MonoBehaviour
{
	private float time = 0;
	private float collectedInstruments = 0;
	void FixedUpdate()
	{
		time += Time.deltaTime;
	}

	public void OnInstrumentColleted(Instrument instrument)
	{
		Debug.Log("Player has collected " + instrument.instrument.ToString());
		collectedInstruments++;
	}
}
