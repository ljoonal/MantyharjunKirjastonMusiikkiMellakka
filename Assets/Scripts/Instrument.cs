using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class Instrument : MonoBehaviour
{
	public InstrumentEnum instrument;

	private AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = 0.5f;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			// TODO
			audioSource.volume = 1f;
		}
	}
}
