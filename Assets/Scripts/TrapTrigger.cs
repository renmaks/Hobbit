using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		var damageable = other.gameObject.GetComponent<IDamageable>();
		damageable?.Die();
	}
}
