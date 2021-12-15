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
	GuitarAcoustic,
	GuitarElectric,
	HiHat,
	Piano,
	SnareDrum,
	Tambourine,
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
		InstrumentType.GuitarAcoustic => "Akustinen kitara",
		InstrumentType.GuitarElectric => "Sähkökitara",
		InstrumentType.HiHat => "HiHat",
		InstrumentType.Piano => "Piano",
		InstrumentType.SnareDrum => "Virvelirumpu",
		InstrumentType.Tambourine => "Tamburiini",
		InstrumentType.Tuba => "Tuuba",
		InstrumentType.Violine => "Alttoviulu",
		_ => throw new ArgumentOutOfRangeException(nameof(self), $"Not expected instrument value: {self}"),
	};
	public static string InstrumentHints(this InstrumentType self) => self switch
	{
		InstrumentType.BassDrum => "rumpu, jolla on syvä ääni.",
		InstrumentType.BassElectric => "sähköinen versio akustisesta bassokitarasta.",
		InstrumentType.BongoDrum => "rumpusoitin, joka koostuu kahdesta pienestä rummusta.",
		InstrumentType.Flute => "puupuhallinsoitin, jota soitetaan puhaltamalla ”nokkaan”.",
		InstrumentType.GuitarAcoustic => "kielisoitin, jonka ääni vahvistetaan akustisesti.",
		InstrumentType.GuitarElectric => "kielisoitin, jossa on sähkömagneettiset mikrofonit.",
		InstrumentType.HiHat => "rumpusetin osa, joka koostuu kahdesta päällekkäisestä symbaalista.",
		InstrumentType.Piano => "kosketinsoitin, jossa on mustavalkoiset koskettimet.",
		InstrumentType.SnareDrum => "rumpu, jossa kalvoa vasten viritetty erityinen virvelimatto..",
		InstrumentType.Tambourine => "lyömäsoitin, joka koostuu rungosta, kalvosta ja helistimistä.",
		InstrumentType.Tuba => "matalaäänisin sinfoniaorkesterissa käytettävä vaskipuhallin.",
		InstrumentType.Violine => "neljäkielinen kielisoitin, jota soitetaan jousella.",
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
				gameObject.AddComponent<GameOver>();

				// Lessen the audio volume before switching spatialization.
				audioSource.volume = Math.Max(audioSource.volume / 2, defaultVolume);
				audioSource.spatialBlend = 0;

				StartCoroutine(OnCollectVolumeAdjust());
			}
		}
	}

	/** Gradually make the instrument volume quieter. */
	private IEnumerator OnCollectVolumeAdjust()
	{
		const float fadeOutDuration = 3f;
		float startingVolume = audioSource.volume;

		for (float t = 0f; t < fadeOutDuration; t += Time.deltaTime)
		{
			float normalizedTime = t / fadeOutDuration;
			audioSource.volume = Mathf.Lerp(startingVolume, defaultVolume / 3, normalizedTime);
			yield return null;
		}
	}
}
