using UnityEngine;

/** Pulls the attached gameObject towards the target with a given speed, when the target is within range. */
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
