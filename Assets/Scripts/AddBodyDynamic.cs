using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBodyDynamic : MonoBehaviour
{
    [SerializeField] GameObject AnkIP_bassDrum;
    [SerializeField] GameObject AnkIP_bassElectric;
    [SerializeField] GameObject AnkIP_bongoDrums;
    [SerializeField] GameObject AnkIP_flute;
    [SerializeField] GameObject AnkIP_guitarAcoustic;
    [SerializeField] GameObject AnkIP_guitarElectric;
    [SerializeField] GameObject AnkIP_hihat;
    [SerializeField] GameObject AnkIP_piano;
    [SerializeField] GameObject AnkIP_snareDrum;
    [SerializeField] GameObject AnkIP_tamborine;
    [SerializeField] GameObject AnkIP_tuba;
    [SerializeField] GameObject AnkIP_violine;
    SnakeManager snakeM;
    public string caller = null;
    // Start is called before the first frame update
    void Start()
    {
        snakeM = GetComponent<SnakeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (caller)
        {
            case "IP_bassDrum":
                snakeM.AddBodyParts(AnkIP_bassDrum);
                break;
            case "IP_bassElectric":
                snakeM.AddBodyParts(AnkIP_bassElectric);
                break;
            case "IP_bongoDrums":
                snakeM.AddBodyParts(AnkIP_bongoDrums);
                break;
            case "IP_flute":
                snakeM.AddBodyParts(AnkIP_flute);
                break;
            case "IP_guitarAcoustic":
                snakeM.AddBodyParts(AnkIP_guitarAcoustic);
                break;
            case "IP_guitarElectric":
                snakeM.AddBodyParts(AnkIP_guitarElectric);
                break;
            case "IP_hihat":
                snakeM.AddBodyParts(AnkIP_hihat);
                break;
            case "IP_piano":
                snakeM.AddBodyParts(AnkIP_piano);
                break;
            case "IP_snareDrum":
                snakeM.AddBodyParts(AnkIP_snareDrum);
                break;
            case "IP_tamborine":
                snakeM.AddBodyParts(AnkIP_tamborine);
                break;
            case "IP_tuba":
                snakeM.AddBodyParts(AnkIP_tuba);
                break;
            case "IP_violine":
                snakeM.AddBodyParts(AnkIP_violine);
                break;
        }
        caller = null;
    }
}
