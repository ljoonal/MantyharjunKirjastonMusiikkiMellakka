using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPull : MonoBehaviour
{
	public Transform target;
	public float moveSpeed = 10;
	public float pullRange = 2;

	void Update()
    {
		float distance = Vector3.Distance(target.position, transform.position);
		if (distance <= pullRange)
		{
			transform.LookAt(target);
			transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
		}
    }
}
