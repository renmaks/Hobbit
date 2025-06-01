using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		var damageable = other.gameObject.GetComponent<IDamageable>();
		Debug.Log("Collision with " + other.gameObject.name);
		damageable?.Die();
	}
}
