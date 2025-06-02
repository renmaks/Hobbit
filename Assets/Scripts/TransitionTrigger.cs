using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
	[Header("Загружаемая сцена")]
	[SerializeField] private string sceneToLoad;
	private void OnTriggerEnter(Collider other)
	{
		var transition = other.gameObject.GetComponent<ITransition>();
		transition?.LoadScene(sceneToLoad);
	}
}
