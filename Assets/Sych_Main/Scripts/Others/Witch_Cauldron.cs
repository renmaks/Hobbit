using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

namespace Sych_scripts
{
    public class Witch_Cauldron : MonoBehaviour
    {
        List<Id_merge_obj> Component_enter_obj_list = new List<Id_merge_obj>();

        [SerializeField]
        int Max_enter_object = 3;

        [SerializeField]
        Transform Spit_out_point = null;

        [SerializeField]
        float Spit_out_force = 10f;

        [SerializeField]
        float Time_spit_out = 1f;

        [SerializeField]
        ParticleSystem PS_spit_out = null;

        [SerializeField]
        ParticleSystem PS_enter_object = null;

        [SerializeField]
        Recipe_class[] Recipes_array = new Recipe_class[0];

        [SerializeField]
        Animator Anim = null;

        Id_merge_obj Fin_object = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.isTrigger == false)
            {
                if (other.gameObject.TryGetComponent(out Id_merge_obj _merge))
                {
                    Component_enter_obj_list.Add(_merge);

                    other.gameObject.SetActive(false);

                    PS_enter_object.Play();

                    Checking_Recipe();
                }
                else
                {
                    StartCoroutine(Coroutine_Spit_one_out(other.gameObject));
                }


                //if (other.tag == "Player")
                //{
                //    Spit_all_out();
                //}
                //else if (other.GetComponent<PlayerController>())
                //{
                //    Spit_all_out();
                //}
            }
        }


        void Checking_Recipe()
        {
            bool end_bool = false;

            for (int x = 0; x < Recipes_array.Length; x++)
            {
                int check_value = 0;

                bool[] check_recipe_array = new bool[Recipes_array[x].Recipe_objs.Length];

                bool[] check_component_array = new bool[Component_enter_obj_list.Count];

                for (int enter_obj_id = 0; enter_obj_id < Component_enter_obj_list.Count; enter_obj_id++)
                { 

                    for (int y = 0; y < Recipes_array[x].Recipe_objs.Length; y++)
                    {

                        //if (PrefabUtility.GetCorrespondingObjectFromSource(Component_enter_obj_list[enter_obj_id]) == Recipes_array[x].Recipe_objs[y])

                        if (Component_enter_obj_list[enter_obj_id].Id == Recipes_array[x].Recipe_objs[y].Id && check_recipe_array[y] == false && check_component_array[enter_obj_id] == false)
                        {
                            check_recipe_array[y] = true;
                            check_component_array[enter_obj_id] = true;

                            check_value++;

                            if (check_value >= Recipes_array[x].Recipe_objs.Length)
                            {
                                end_bool = true;
                                Fin_object = Recipes_array[x].Result_prefab;
                                Anim.Play("Craft");
                                Reset_method();
                                break;
                            }
                        }
                    }
                }

                if (end_bool)
                    break;
            }

            if (Max_enter_object <= Component_enter_obj_list.Count)
            {
                if (end_bool)
                    Reset_method();
                else
                    Spit_all_out();
            }

        }

        public void End_craft()
        {
            StartCoroutine(Coroutine_Spit_one_out(Instantiate(Fin_object.gameObject, Spit_out_point.position, Quaternion.identity)));
        }

        IEnumerator Coroutine_Spit_one_out(GameObject _obj)
        {
            if (_obj.TryGetComponent(out Rigidbody _body))
            {
                _obj.SetActive(false);

                PS_spit_out.Play();

                yield return new WaitForSeconds(Time_spit_out);

                _obj.SetActive(true);

                _obj.transform.position = Spit_out_point.position;

                _body.linearVelocity = Vector3.zero;

                Spit_out_point.localRotation = Quaternion.Euler(new Vector3(Spit_out_point.localEulerAngles.x, Random.Range(-360f, 360f), Spit_out_point.localEulerAngles.z));

                _body.AddForce(Spit_out_point.forward * Spit_out_force, ForceMode.VelocityChange);
            }
        }

        //void Spit_one_out(GameObject _obj)
        //{
        //    if (_obj.TryGetComponent(out Rigidbody _body))
        //    {
        //        _obj.SetActive(true);

        //        _obj.transform.position = Spit_out_point.position;

        //        _body.linearVelocity = Vector3.zero;

        //        Spit_out_point.localRotation = Quaternion.Euler(new Vector3(Spit_out_point.localEulerAngles.x, Random.Range(-360f, 360f), Spit_out_point.localEulerAngles.z));

        //        _body.AddForce(Spit_out_point.forward * Spit_out_force, ForceMode.VelocityChange);

        //        PS_spit_out.Play();
        //    }
        //}

        void Spit_all_out()
        {
            StartCoroutine(Coroutine_Spit_out());
        }

        IEnumerator Coroutine_Spit_out()
        {
            List<Id_merge_obj> active_list = new List<Id_merge_obj> (Component_enter_obj_list);
            
            Component_enter_obj_list.Clear();

            for (int x = 0; x < active_list.Count; ++x)
            {
                yield return new WaitForSeconds(Time_spit_out);

                StartCoroutine(Coroutine_Spit_one_out(active_list[x].gameObject));
            }

            
        }

        void Reset_method()
        {
            for(int x = 0; x < Component_enter_obj_list.Count; ++x)
            {
                Destroy(Component_enter_obj_list[x]);
            }

            Component_enter_obj_list.Clear();
        }


        [System.Serializable]
        class Recipe_class
        {
            public Id_merge_obj[] Recipe_objs = new Id_merge_obj[0];

            public Id_merge_obj Result_prefab = null;
        }
    }
}