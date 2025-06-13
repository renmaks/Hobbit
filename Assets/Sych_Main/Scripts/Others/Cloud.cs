using System.Collections;
using UnityEngine;

namespace Sych_scripts
{
    public class Cloud : MonoBehaviour
    {

        [SerializeField]
        Transform Parent = null;

        [SerializeField]
        float Time_active = 2f;

        float Timer = 0;

        [SerializeField]
        ParticleSystem PS_harm = null;

        [SerializeField]
        ParticleSystem PS_activation = null;

        [SerializeField]
        float Size_activation = 0.2f;

        [SerializeField]
        float Speed_Size_change = 10.1f;

        [SerializeField]
        float Force_breaking = 3f;

        bool Active_bool = false;

        Vector3 Defaul_size = Vector3.one;

        Coroutine Anim_size_coroutine = null;


        private void OnCollisionEnter(Collision collision)
        {
            float force_other_obj = 0;

            if (collision.gameObject.TryGetComponent(out Rigidbody _body))
                force_other_obj = _body.mass * _body.linearVelocity.magnitude;

            if (force_other_obj >= Force_breaking)
            {
                Activation();
            }

                
        }

        private void Start()
        {
            Defaul_size = Parent.transform.localScale;
        }

        private void Update()
        {
            Timer -= Time.deltaTime;

            if(Timer <= 0)
            {
                Active_bool = false;

                PS_activation.Stop();
            }
        }

        [ContextMenu("Активировать")]
        public void Activation()
        {
            Active_bool = true;

            Timer = Time_active;

            if (Anim_size_coroutine != null)
                StopCoroutine(Anim_size_coroutine);

            Anim_size_coroutine = StartCoroutine(Coroutine_Anim_size());

            PS_harm.Play();

            if (!PS_activation.isPlaying)
                PS_activation.Play();
        }

        IEnumerator Coroutine_Anim_size()
        {
            Vector3 min_size = new Vector3(Defaul_size.x - Size_activation, Defaul_size.y - Size_activation, Defaul_size.z - Size_activation);

            Vector3 max_size = new Vector3(Defaul_size.x + Size_activation, Defaul_size.y + Size_activation, Defaul_size.z + Size_activation);

            bool step_max_bool = false;

            bool step_normal_bool = false;

            while (true)
            {
                yield return null;

                if (!step_max_bool && !step_normal_bool) 
                {
                    Parent.transform.localScale = Vector3.MoveTowards(Parent.transform.localScale, min_size, Speed_Size_change * Time.deltaTime);

                    if (Parent.transform.localScale == min_size)
                    {
                        step_max_bool = true;
                    }
                }
                else if(step_max_bool && !step_normal_bool)
                {
                    Parent.transform.localScale = Vector3.MoveTowards(Parent.transform.localScale, max_size, Speed_Size_change * Time.deltaTime);

                    if (Parent.transform.localScale == max_size)
                    {
                        step_max_bool = true;
                    }
                }
                else if (step_normal_bool)
                {
                    Parent.transform.localScale = Vector3.MoveTowards(Parent.transform.localScale, Defaul_size, Speed_Size_change * Time.deltaTime);

                    if (Parent.transform.localScale == Defaul_size)
                    {
                        break;
                    }
                }
            }
        }

    }
}