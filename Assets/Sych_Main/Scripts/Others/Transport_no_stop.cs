using UnityEngine;

namespace Sych_scripts
{
    public class Transport_no_stop : MonoBehaviour
    {

        [SerializeField]
        float Speed = 1000f;

        [SerializeField]
        float Speed_rotation = 100f;

        [SerializeField]
        Rigidbody Body = null;

        [SerializeField]
        Camera Cam = null;

        [SerializeField]
        Transform Mesh_character = null;

        [SerializeField]
        ParticleSystem PS_soplo = null;

        [field: SerializeField]
        public Transform Point_sit { get; private set; } = null;

        [SerializeField]
        bool Active_bool = false;


        public void Activaty(bool _active)
        {
            Active_bool = _active;
            Cam.gameObject.SetActive (_active);
            Mesh_character.gameObject.SetActive (_active);

            Body.useGravity = !_active;

            Body.freezeRotation = _active;

            if (_active)
                PS_soplo.Play();
            else
                PS_soplo.Stop();
        }

        private void Update()
        {
            if (Active_bool)
            {
                //Body.AddForce(transform.forward * Speed * Time.deltaTime, ForceMode.Impulse);

                //transform.position = transform.position + transform.forward * Speed * Time.deltaTime;

                Body.Move(transform.position + transform.forward * Speed, transform.rotation);

                if (Input.GetKey(KeyCode.A))
                {
                    //Body.AddTorque(Vector3.down * Speed_rotation * Time.deltaTime, ForceMode.Impulse);
                    transform.Rotate(Vector3.down * Speed_rotation * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    //Body.AddTorque(Vector3.up * Speed_rotation * Time.deltaTime, ForceMode.Impulse);
                    transform.Rotate(Vector3.up * Speed_rotation * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.W))
                {
                    //Body.AddTorque(Vector3.right * Speed_rotation * Time.deltaTime, ForceMode.Impulse);
                    transform.Rotate(Vector3.left * Speed_rotation * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    //Body.AddTorque(Vector3.left * Speed_rotation * Time.deltaTime, ForceMode.Impulse);
                    transform.Rotate(Vector3.right * Speed_rotation * Time.deltaTime);
                }
            }
        }
    }
}