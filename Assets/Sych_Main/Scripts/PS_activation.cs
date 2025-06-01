using UnityEngine;

namespace Sych_scripts
{
    public class PS_activation : MonoBehaviour
    {
        [SerializeField]
        ParticleSystem PS = null;


        public void Activation()
        {
            PS.Play();
        }
    }
}