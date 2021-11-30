using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
	// Where the snake's "head" will be, which the first body item will follow
	public GameObject target;
	// The distance between all the snake's parts
	[Range(0f, 2f)]
	public float distanceBetween = .2f;
	// The list of the snake's parts. First element will be the head.
	private readonly List<GameObject> snakeBody = new List<GameObject>();

	void Start()
	{
		snakeBody.Add(target);
	}


	void FixedUpdate()
	{
		SnakeMovement();
	}

	void SnakeMovement()
	{
		GameObject prevPiece = null;
		foreach (var bodyPiece in snakeBody)
		{
			Vector3 currPos = bodyPiece.transform.position;
			if (prevPiece != null)
			{
				if (Vector3.Distance(prevPiece.transform.position, currPos) > distanceBetween)
				{
					bodyPiece.transform.position =
						Vector3.MoveTowards(prevPiece.transform.position, currPos, distanceBetween);
				}
			}

			prevPiece = bodyPiece;
		}
	}

	public void AddBodyParts(GameObject obj)
	{
		// Inserts new pieces after the head.
		snakeBody.Insert(1, obj);
	}
}
