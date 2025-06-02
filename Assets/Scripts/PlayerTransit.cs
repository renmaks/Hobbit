using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTransit : MonoBehaviour, ITransition
{
	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
