using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/** Chases after a snake body part, or backupTarget, whichever is closest.*/
public class ChaserController : MonoBehaviour
{
	public Transform backupTarget;
	public NavMeshAgent agent;

	private SnakeManager snakeManager;

	void Start()
	{
		snakeManager = FindObjectOfType<SnakeManager>();
	}

	void FixedUpdate()
	{
		Vector3 closestTarget = backupTarget.position;
		float closestTargetDistance = float.PositiveInfinity;

		foreach (var possibleTarget in snakeManager.GetBodyPartsRef())
		{
			float distance = Vector3.Distance(
				transform.position, possibleTarget.transform.position);
			if (distance < closestTargetDistance)
			{
				closestTarget = possibleTarget.transform.position;
				closestTargetDistance = distance;
			}
		}

		agent.SetDestination(closestTarget);
	}
}

