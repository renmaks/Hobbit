using UnityEngine;

public class PlayerScore : MonoBehaviour, IScoreable
{
	public void Pickup()
	{
		Debug.Log("Собран овощ, зачислены очки");
	}
}
