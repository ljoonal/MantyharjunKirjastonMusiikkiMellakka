using System; // Instrument.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemPullEnum
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
public static class ItemPullData
{
    public static string FinnishName(this ItemPullEnum self) => self switch
    {
        ItemPullEnum.BassDrum => "Basso Rumpu",
        ItemPullEnum.BassElectric => "Basso Kitara",
        ItemPullEnum.BongoDrum => "Bongorummut",
        ItemPullEnum.Flute => "Nokkahuilu",
        ItemPullEnum.GuitarAccoustic => "Akustinen kitara",
        ItemPullEnum.GuitarElectric => "Sähkökitara",
        ItemPullEnum.HiHat => "HiHat",
        ItemPullEnum.Piano => "Piano",
        ItemPullEnum.SnareDrum => "Virvelirumpu",
        ItemPullEnum.Tamborine => "Tamburiini",
        ItemPullEnum.Tuba => "Tuuba",
        ItemPullEnum.Violine => "Alttoviulu",
        _ => throw new ArgumentOutOfRangeException(nameof(self), $"Not expected instrument value: {self}"),
    };
}

public class ItemPull : MonoBehaviour
{
    public InstrumentEnum instrument; // Instrument.cs
    private AudioSource audioSource; // Instrument.cs

    public Transform anchor;
    private float distance;
    public float moveSpeed = 50;
    public float pullRange = 2;
    public float attachRange = 0.2f;
    AddBodyDynamic addBody;
    private Vector3 rot = new Vector3(0, 120, 0);
    // Start is called before the first frame update
    void Start()
    {
        addBody = GetComponent<AddBodyDynamic>();
        audioSource = GetComponent<AudioSource>(); // Instrument.cs
        audioSource.volume = 0.1f; // Instrument.cs
    }

    // Update is called once per frame
    void Update()
    {
        // on every frame we rotate the object according to vector and slow down factor Time.deltaTime (dt between frames)
        transform.Rotate(rot * Time.deltaTime);

        distance = Vector3.Distance(anchor.position, transform.position);
        if (distance <= attachRange)
        {
            this.transform.position = anchor.position;
        } else if (distance <= pullRange)
        {
            transform.LookAt(anchor);
            GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
        }
        // Search for THIS object's anchor part if it exist
        string myAnchor = "Ank" + gameObject.name;
        if (GameObject.Find(myAnchor) == null)
        {
            print("Could not find " + GameObject.Find(myAnchor));
        } else
        {
            anchor = GameObject.Find(myAnchor).transform;
            this.transform.position = anchor.position;
            this.transform.parent = anchor.transform;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.volume = 1.0f; // Instrument.cs

            Debug.Log("Player has touched " + gameObject.name);
            // Generate anchor snake body part for THIS object with AddBodyDynamic();
            GameObject.Find("SnakeManager").GetComponent<AddBodyDynamic>().caller = gameObject.name;
            GetComponent<Collider>().enabled = false;
        }
    }
}
