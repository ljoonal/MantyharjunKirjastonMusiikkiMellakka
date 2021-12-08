using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** All the implemented instruments. */
public enum InstrumentEnum
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
	public static string FinnishName(this InstrumentEnum self) => self switch
	{
		InstrumentEnum.BassDrum => "Basso Rumpu",
		InstrumentEnum.BassElectric => "Basso Kitara",
		InstrumentEnum.BongoDrum => "Bongorummut",
		InstrumentEnum.Flute => "Nokkahuilu",
		InstrumentEnum.GuitarAccoustic => "Akustinen kitara",
		InstrumentEnum.GuitarElectric => "Sähkökitara",
		InstrumentEnum.HiHat => "HiHat",
		InstrumentEnum.Piano => "Piano",
		InstrumentEnum.SnareDrum => "Virvelirumpu",
		InstrumentEnum.Tamborine => "Tamburiini",
		InstrumentEnum.Tuba => "Tuuba",
		InstrumentEnum.Violine => "Alttoviulu",
		_ => throw new ArgumentOutOfRangeException(nameof(self), $"Not expected instrument value: {self}"),
	};
}


/** A scripted instrument that the player can collect & add to the snake body. */
public class Instrument : MonoBehaviour
{
	public InstrumentEnum instrument;

	private AudioSource audioSource;

	private bool added = false;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = 0.5f;
	}

	void Update()
	{
		transform.Rotate(Vector3.up * 100 * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Chaser"))
		{
			// TODO: Game over?
		}
		if (!added && other.CompareTag("Player"))
		{
			FindObjectOfType<SnakeManager>().AddBodyParts(gameObject);
			audioSource.volume = 1f;
			added = true;
			FindObjectOfType<GameStateManager>().OnInstrumentColleted(this);
			ItemPull itemPull = GetComponent<ItemPull>();
			if (itemPull != null) Destroy(itemPull);
		}
	}
}
