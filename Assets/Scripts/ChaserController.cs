using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserController : MonoBehaviour
{
	public Transform target;
	public NavMeshAgent agent;

	void FixedUpdate()
	{
		agent.SetDestination(target.position);
	}
}

