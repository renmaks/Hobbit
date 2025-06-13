using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Sych_scripts
{
    public class Get_object : MonoBehaviour
    {

        [SerializeField]
        float Distance = 1f;

        [SerializeField]
        float Radius = 1f;

        [SerializeField]
        float Throw_force = 5f;

        [SerializeField]
        LayerMask Mask = 0;

        [SerializeField]
        Transform Point_object = null;

        [SerializeField]
        bool Debug_bool = false;

        Rigidbody Active_object = null;

        bool Active_object_useGravity = false;
        bool Active_object_isKinematic = false;


        void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (Active_object == null)
                    Get_object_method();
                else
                    Put_object();

            }

            else if (Input.GetKeyDown(KeyCode.F))
            {
                if (Active_object != null)
                    Throw_object();
            }
        }


        void Get_object_method()
        {
            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, Distance, Mask, QueryTriggerInteraction.Ignore))
            {

                if (hit.transform.TryGetComponent(out Rigidbody target))
                {
                    Active_object = target;

                    Active_object_useGravity = Active_object.useGravity;
                    Active_object_isKinematic = Active_object.isKinematic;

                    Change_active_object(false);

                    Active_object.transform.SetParent(Point_object);

                    Active_object.transform.localPosition = Vector3.zero;
                }
            }

            if (Active_object == null)
            {
                if (Physics.SphereCast(ray, Radius, out hit, Distance, Mask, QueryTriggerInteraction.Ignore))
                {

                    if (hit.transform.TryGetComponent<Rigidbody>(out Rigidbody target))
                    {
                        Active_object = target;

                        Change_active_object(false);

                        Active_object.transform.SetParent(Point_object);

                        Active_object.transform.localPosition = Vector3.zero;
                    }
                }
            }
        }

        void Put_object()
        {
            Change_active_object(true);

            Active_object = null;
        }

        void Throw_object()
        {
            Change_active_object(true);

            if (Active_object_isKinematic != true)
                Active_object.AddForce(transform.forward * Throw_force);

            Active_object = null;
        }

        void Change_active_object(bool _active)
        {
            if (!_active)
            {
                Active_object.useGravity = false;
                Active_object.isKinematic = true;
            }
            else
            {
                Active_object.useGravity = Active_object_useGravity;
                Active_object.isKinematic = Active_object_isKinematic;
            }


            if(Active_object.isKinematic != true)
                Active_object.linearVelocity = Vector3.zero;

            if (Active_object.transform.TryGetComponent<Collider>(out Collider col))
            {
                col.enabled = _active;
            }

            if (_active)
            {
                Active_object.transform.SetParent(null);
            }
                
        }

        private void OnDrawGizmosSelected()
        {
            if (Debug_bool)
            {
                Gizmos.color = Color.yellow;

                Gizmos.DrawLine(transform.position, transform.position + transform.forward * Distance);

                Gizmos.DrawSphere(transform.position + transform.forward * Distance, Radius);
            }
        }
    }
}