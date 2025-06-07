using System.Collections;
using UnityEngine;

namespace Sych_scripts
{
    public class Bomb : MonoBehaviour, I_burn
    {
        [SerializeField]
        float Time = 2f;

        [SerializeField]
        float Radius_explosion = 8f;

        [SerializeField]
        float Force_explosion = 600f;

        [SerializeField]
        ParticleSystem PS_fuse_burning = null;

        [SerializeField]
        ParticleSystem Prefab_PS_explosion = null;

        [SerializeField]
        bool Gizmos_bool = false;

        bool Active_bool = false;

        public void Activation()
        {
            if (!Active_bool)
            {
                Active_bool = true;

                PS_fuse_burning.Play();

                StartCoroutine(Time_burn());
            }
        }

        public void Arson()
        {
            Activation();
        }

        IEnumerator Time_burn()
        {
            yield return new WaitForSeconds(Time);

            Explosion();

            Destroy(gameObject);
        }

        void Explosion()
        {
            ParticleSystem ps = Instantiate(Prefab_PS_explosion, transform.position, Quaternion.identity);

            ps.transform.localScale = Vector3.one * Radius_explosion;

            foreach (ParticleSystem ps_active in ps.GetComponentsInChildren<ParticleSystem>())
            {
                ps_active.transform.localScale = Vector3.one * Radius_explosion;
            }



            Collider[] objects = Physics.OverlapSphere(transform.position, Radius_explosion);

            if (objects.Length > 0)
            {
                for (int i = 0; i < objects.Length; i++)
                {
                    if (objects[i].TryGetComponent(out Rigidbody rb))
                    {
                        //rb.AddExplosionForce(Force_explosion, transform.position, Radius_explosion);

                        Vector3 forceDirection = rb.transform.position - transform.position;
                        float distanceModifier = 1 - (Mathf.Clamp(forceDirection.magnitude, 0, Radius_explosion) / Radius_explosion);

                        Vector2 forcePosition = objects[i].ClosestPoint(transform.position);
                        rb.AddForceAtPosition((forceDirection * Force_explosion) * distanceModifier, forcePosition, ForceMode.Force);
                    }
                }
            }
        }


        private void OnDrawGizmosSelected()
        {
            if (Gizmos_bool)
            {
                Gizmos.color = new Color(1f, 1f, 1f, 0.2f);

                Gizmos.DrawSphere(transform.position, Radius_explosion);
            }
        }
    }
}