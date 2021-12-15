using System.Collections.Generic;
using UnityEngine;

/** Controls the snake that the player makes by collecting instruments. */
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
		// Moves the next bodypart towards the previous one if it's too far away
		foreach (var bpart in bodyParts)
		{
			float distance = Vector3.Distance(previousTransform.position, bpart.transform.position);
			if (distance > maxDistanceBetween) MovePartTo(bpart, previousTransform);
			previousTransform = bpart.transform;
		}
	}

	/** Moves bodypart closer to transform, but only the minimum distance that is required. **/
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
