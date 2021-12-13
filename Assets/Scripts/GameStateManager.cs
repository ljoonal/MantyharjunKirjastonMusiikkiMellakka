using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/** Manages the game's state when playing. So
  * So keeps track off of current score for example.
  */
public class GameStateManager : MonoBehaviour
{
	private float time = 0;
	public float timeLimit = 120;
	public GameObject OnLoseUI;
	public Text timeText;
	public Text instrumentsText;
	public Text nextInstrumentText;
	private List<Instrument> collectedInstruments = new List<Instrument>();
	private List<Instrument> notCollectedInstruments = new List<Instrument>();
	private Instrument nextInstrument;
	private SnakeManager snakeManager;
	private System.Random rng = new System.Random();


	void Start()
	{
		snakeManager = FindObjectOfType<SnakeManager>();
	}

	void FixedUpdate()
	{
		time += Time.deltaTime;
		timeText.text = $"Aika: {Math.Round(time)}/{Math.Round(timeLimit)}";
	}

	public void OnInstrumentStart(Instrument instrument)
	{
		notCollectedInstruments.Add(instrument);
		instrument.GetComponent<ItemPull>().enabled = false;
		UpdateInstrumentsState();
	}

	/* Picks the next instrument to collect and updates text displays. */
	private void UpdateInstrumentsState()
	{
		instrumentsText.text = $"Soittimia: {collectedInstruments.Count}/{collectedInstruments.Count + notCollectedInstruments.Count}";

		if (nextInstrument != null)
		{
			nextInstrument.GetComponent<ItemPull>().enabled = false;
		}
		int index = rng.Next(notCollectedInstruments.Count);
		nextInstrument = notCollectedInstruments[index];
		nextInstrumentText.text = "Etsi: " + nextInstrument.type.FinnishName();

		nextInstrument.GetComponent<ItemPull>().enabled = true;
	}

	/** Returns true if the instrument was added. On false the instrument couldn't be collected yet. */
	public bool OnInstrumentTrigger(Instrument instrument)
	{
		if (nextInstrument.type == instrument.type)
		{
			Debug.Log("Player has collected " + instrument.type.ToString());

			notCollectedInstruments.Remove(instrument);
			collectedInstruments.Add(instrument);
			UpdateInstrumentsState();
			snakeManager.AddBodyParts(instrument.gameObject);

			return true;
		}
		Debug.Log("Player incorrectly touched " + instrument.type.ToString());
		return false;
	}

	public void OnLose()
	{
		Time.timeScale = 0;
		AudioListener.pause = true;
		OnLoseUI.SetActive(true);
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

