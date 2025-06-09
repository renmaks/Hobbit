using UnityEngine;

namespace Sych_scripts
{
    public class Bonfire : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out I_burn _obj))
            {
                _obj.Arson();
            }
        }

    }
}