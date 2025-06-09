using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		var scoreable = other.gameObject.GetComponent<IScoreable>();
		scoreable?.Pickup();
		Destroy(this.transform.parent.gameObject);
	}
}
