using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour, IScoreable
{
	//Wwise
	public Action OnPickupOn;
    public void Pickup()
	{
        OnPickupOn?.Invoke();

        Debug.Log("Собран овощ, зачислены очки");
	}
}
