using UnityEngine;

namespace Sych_scripts
{
    public class PS_rain : MonoBehaviour
    {

        [SerializeField]
        float Value = 0.2f;

        private void OnParticleCollision(GameObject other)
        {
            if(other.TryGetComponent(out I_Watering interface_i))
                interface_i.Add_water(Value);
        }
    }
}