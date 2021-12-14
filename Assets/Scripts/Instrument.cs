using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** All the implemented instruments. */
public enum InstrumentType
{
	BassDrum,
	BassElectric,
	BongoDrum,
	Flute,
	GuitarAccoustic,
	GuitarElectric,
	HiHat,
	Piano,
	SnareDrum,
	Tamborine,
	Tuba,
	Violine
}

/** Data that relates to the implemented instruments. */
public static class InstrumentData
{
	public static string FinnishName(this InstrumentType self) => self switch
	{
		InstrumentType.BassDrum => "Basso Rumpu",
		InstrumentType.BassElectric => "Basso Kitara",
		InstrumentType.BongoDrum => "Bongorummut",
		InstrumentType.Flute => "Nokkahuilu",
		InstrumentType.GuitarAccoustic => "Akustinen kitara",
		InstrumentType.GuitarElectric => "Sähkökitara",
		InstrumentType.HiHat => "HiHat",
		InstrumentType.Piano => "Piano",
		InstrumentType.SnareDrum => "Virvelirumpu",
		InstrumentType.Tamborine => "Tamburiini",
		InstrumentType.Tuba => "Tuuba",
		InstrumentType.Violine => "Alttoviulu",
		_ => throw new ArgumentOutOfRangeException(nameof(self), $"Not expected instrument value: {self}"),
	};
}


/** A scripted instrument that the player can collect & add to the snake body. */
public class Instrument : MonoBehaviour
{
	public InstrumentType type;

	private AudioSource audioSource;

	private GameStateManager gameStateManager;

	public float defaultVolume = 0.3f;

	void Start()
	{
		gameStateManager = FindObjectOfType<GameStateManager>();
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = defaultVolume;
		audioSource.minDistance = 1f;
		audioSource.maxDistance = 500f;
		gameStateManager.OnInstrumentStart(this);
	}

	public void SetVolume(float volume)
	{
		audioSource.volume = volume;
	}

	void Update()
	{
		transform.Rotate(Vector3.up * 100 * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (gameStateManager.OnInstrumentTrigger(this))
			{
				audioSource.volume = 0.2f;
				audioSource.spatialBlend = 0;
				gameObject.AddComponent<GameOver>();
			}
		}
	}
}
