using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
	public Transform target;
	public float maxDistanceBetween = 1f;
	private readonly List<GameObject> bodyParts = new List<GameObject>();

	public ref readonly List<GameObject> GetBodyPartsRef()
	{
		return ref bodyParts;
	}

	void FixedUpdate()
    {
		Transform previousTransform = target;
		foreach (var bpart in bodyParts)
		{
			float distance = Vector3.Distance(previousTransform.position, bpart.transform.position);
			if (distance > maxDistanceBetween) MovePartTo(bpart, previousTransform);
			previousTransform = bpart.transform;
		}
    }

	private void MovePartTo(GameObject bodyPart, Transform to)
	{
		Vector3 closeEnoughPosition = Vector3.MoveTowards(to.position, bodyPart.transform.position, maxDistanceBetween);
		bodyPart.transform.position = Vector3.MoveTowards(bodyPart.transform.position, closeEnoughPosition, maxDistanceBetween);
	}


	public void AddBodyParts(GameObject obj)
	{
		bodyParts.Insert(0, obj);
	}
}
