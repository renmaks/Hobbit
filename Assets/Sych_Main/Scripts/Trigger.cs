using UnityEngine;
using UnityEngine.Events;

namespace Sych_scripts
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField]
        Transform[] Target_array = new Transform[0];

        [SerializeField]
        UnityEvent Activation_event = null;

        private void OnTriggerEnter(Collider other)
        {
            for (int x = 0; x < Target_array.Length; x++)
            {
                if (Target_array[x] == other.transform)
                {
                    Activation_event.Invoke();

                    break;
                }
            }
        }
    }
}
