using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/** Manages the game's state when playing. So
  * So keeps track off of current score for example.
  */
public class GameStateManager : MonoBehaviour
{
	private float time = 0;
	public float timeLimit = 120;
	public GameObject OnGameOverUI;
	public GameObject OnLoseText;
	public GameObject OnWinText;
	public InputField PlayerNameInput;
	public Text timeText;
	public Text instrumentsText;
	public Text nextInstrumentText;
	public Text onPickupText;
	public GameObject chaser;
	public bool gameHasStopped = false;
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
		if (!gameHasStopped)
        {
			time += Time.deltaTime;
			timeText.text = $"Aika: {Math.Round(time)}/{Math.Round(timeLimit)}";
		}

		if (time > timeLimit)
		{
			OnLose();
		}
	}

	public void OnInstrumentStart(Instrument instrument)
	{
		notCollectedInstruments.Add(instrument);
		instrument.GetComponent<ItemPull>().enabled = false;
		UpdateInstrumentsState();

		List<Vector3> positions = notCollectedInstruments.Select((x) => x.transform.position).ToList();
		foreach (var notCollectedInstrument in notCollectedInstruments)
		{
			int index = rng.Next(positions.Count);
			notCollectedInstrument.transform.position = positions[index];
			positions.RemoveAt(index);
		}
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
		nextInstrumentText.text = "Etsi: " + nextInstrument.type.InstrumentHints();
		nextInstrument.GetComponent<ItemPull>().enabled = true;
		StartCoroutine(InstrumentHintingVolumeAdjustment());
	}

	/** Makes next instrument louder compared to other instruments for a while. */
	private IEnumerator InstrumentHintingVolumeAdjustment()
	{
		nextInstrument.SetVolume(1f);
		const float fadeOutDuration = 0.5f;
		for (float t = 0f; t < fadeOutDuration; t += Time.deltaTime)
		{
			float normalizedTime = t / fadeOutDuration;
			foreach (Instrument instrument in notCollectedInstruments)
				if (instrument != nextInstrument) instrument.SetVolume(Mathf.Lerp(instrument.defaultVolume, instrument.defaultVolume / 6, normalizedTime));
			yield return null;
		}
		yield return new WaitForSecondsRealtime(2);

		const float fadeInDuration = 0.5f;
		for (float t = 0f; t < fadeInDuration; t += Time.deltaTime)
		{
			float normalizedTime = t / fadeInDuration;
			foreach (Instrument instrument in notCollectedInstruments)
				if (instrument != nextInstrument) instrument.SetVolume(Mathf.Lerp(instrument.defaultVolume / 6, instrument.defaultVolume, normalizedTime));
				else instrument.SetVolume(Mathf.Lerp(1f, instrument.defaultVolume, normalizedTime));
			yield return null;
		}
	}

	/** Returns true if the instrument was added. On false the instrument couldn't be collected yet. */
	public bool OnInstrumentTrigger(Instrument instrument)
	{
		if (gameHasStopped) return false;
		if (nextInstrument.type == instrument.type)
		{
			onPickupText.text = nextInstrument.type.FinnishName();
			Debug.Log("Player has collected " + instrument.type.ToString());

			notCollectedInstruments.Remove(instrument);
			collectedInstruments.Add(instrument);
			if (notCollectedInstruments.Count == 0) OnWin();
			else UpdateInstrumentsState();

			snakeManager.AddBodyParts(instrument.gameObject);
			StartCoroutine(RemoveTextCounter());
			IEnumerator RemoveTextCounter()
			{
				for (; ; )
				{
					yield return new WaitForSecondsRealtime(3.0f);
					onPickupText.text = "";
				}
			}
			return true;

		}
		if (!collectedInstruments.Contains(instrument) || collectedInstruments.Count == 0)
		{
			Debug.Log("Player incorrectly touched " + instrument.type.ToString());
			onPickupText.text = "V��r� soitin!";
			StartCoroutine(RemoveTextCounter());
			IEnumerator RemoveTextCounter()
			{
				for (; ; )
				{
					yield return new WaitForSecondsRealtime(3.0f);
					onPickupText.text = "";
				}
			}
			return false;
		}
		else
        {
			return false;
        }
	}

	private int CalculateScore()
	{
		float instrumentScore = collectedInstruments.Count * 1000;
		return (int)Math.Round(instrumentScore / (timeLimit + time));
	}

	public void OnWin()
	{
		if (gameHasStopped) return;
		gameHasStopped = true;
		OnWinText.SetActive(true);
		OnGameOverUI.SetActive(true);
		chaser.GetComponent<NavMeshAgent>().isStopped = true;
	}

	public void PostToDb()
    {
		string name = PlayerNameInput.text;
		FindObjectOfType<BackendHandler>().SendDataToDB(name, CalculateScore());
		SceneManager.LoadScene(0);
    }

	public void OnLose()
	{
		if (gameHasStopped) return;
		gameHasStopped = true;
		OnLoseText.SetActive(true);
		OnGameOverUI.SetActive(true);
		chaser.GetComponent<NavMeshAgent>().isStopped = true;
	}
}

