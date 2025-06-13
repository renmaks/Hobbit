using UnityEngine;


namespace Sych_scripts
{
    public class Fragile_object : MonoBehaviour
    {
        [SerializeField]
        float Force_breaking = 3f;

        [SerializeField]
        GameObject Prefab_parts = null;

        [SerializeField]
        Rigidbody Body = null;

        [SerializeField]
        ParticleSystem PS_prefab = null;

        private void OnCollisionEnter(Collision collision)
        {
            float force_i_obj = 0;

            float force_other_obj = 0;

            force_i_obj = Body.mass * Body.linearVelocity.magnitude;

            if (collision.gameObject.TryGetComponent(out Rigidbody _body))
                force_other_obj = _body.mass * _body.linearVelocity.magnitude;

            //print(force_i_obj + "  " + force_other_obj + "  " + collision.gameObject.name);

            if ((force_i_obj + force_other_obj) >= Force_breaking)
            {
                GameObject gameObject_ = Instantiate(Prefab_parts, transform.position, transform.rotation);

                Rigidbody[] body_array = gameObject_.GetComponentsInChildren<Rigidbody>();

                for(int x = 0; x < body_array.Length; x++)
                {
                    body_array[x].linearVelocity = Body.linearVelocity;

                    body_array[x].angularVelocity = Body.angularVelocity;
                }

                Instantiate(PS_prefab, transform.position, transform.rotation);

                Destroy(gameObject);


            }
        }
    }
}