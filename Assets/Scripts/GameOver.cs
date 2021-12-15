using UnityEngine;



public class GameOver : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Chaser"))
		{
			FindObjectOfType<GameStateManager>().OnLose();
		}
	}
}
