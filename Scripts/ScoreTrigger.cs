using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		var scoreable = other.gameObject.GetComponent<IScoreable>();
		Debug.Log("Collision with " + other.gameObject.name);
		scoreable?.Pickup();
		Destroy(gameObject);
	}
}
