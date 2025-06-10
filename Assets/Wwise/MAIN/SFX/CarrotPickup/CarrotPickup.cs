using UnityEngine;

public class CarrotPickup : MonoBehaviour
{
    public PlayerScore CarrotPickupSFX;

    public AK.Wwise.Event CarrotOnPickup;

    public void Start()
    {
        CarrotPickupSFX.OnPickupOn = Pickup;
    }

    public void Pickup()
    {

        CarrotOnPickup.Post(gameObject);

    }
}
