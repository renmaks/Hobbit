using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static ak.wwise.core;

namespace Sych_scripts
{
    public class Sit_transport : MonoBehaviour
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
        Rigidbody Body = null;

        Transport_no_stop Active_transport = null;

        [SerializeField]
        Transform Parent = null;

        [SerializeField]
        UnityEvent Sit_event = new UnityEvent();

        [SerializeField]
        UnityEvent Get_out_event = new UnityEvent();

        [SerializeField]
        bool Debug_bool = false;



        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Active_transport == null)
                    Get_on_transport();
                else
                    Get_out_of_transport();

            }

        }


        void Get_on_transport()
        {
            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, Distance, Mask, QueryTriggerInteraction.Ignore))
            {

                if (hit.transform.TryGetComponent(out Transport_no_stop _target))
                {
                    Active_transport = _target;

                    Sit(_target);
                }
            }

            if (Active_transport == null)
            {
                if (Physics.SphereCast(ray, Radius, out hit, Distance, Mask, QueryTriggerInteraction.Ignore))
                {

                    if (hit.transform.TryGetComponent(out Transport_no_stop _target))
                    {
                        Sit(_target);
                    }
                }
            }
        }

        void Sit(Transport_no_stop _transport)
        {
            //Parent.SetParent(_transport.Point_sit);

            //Parent.position = _transport.Point_sit.position;

            //Parent.rotation = _transport.Point_sit.rotation;

            Active_transport = _transport;

            //Body.isKinematic = true;
            //Body.useGravity = false;
            //_transport.Joint.connectedBody = Body;

            //Destroy(Body);

            Active_transport.Activaty(true);

            Sit_event.Invoke();
        }

        void Get_out_of_transport()
        {
            //transform.SetParent(null);
            //Body.isKinematic = false;
            //Body.useGravity = true;

            Active_transport.Activaty(false);

            Parent.position = Active_transport.Point_sit.position;

            Parent.rotation = Active_transport.Point_sit.rotation;

            //Body = Parent.AddComponent<Rigidbody>();

            Active_transport = null;

            Get_out_event.Invoke();
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