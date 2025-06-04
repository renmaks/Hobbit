
using UnityEngine;

namespace Sych_scripts
{
    public class WeaK_mask : MonoBehaviour
    {

        [SerializeField]
        SkinnedMeshRenderer Mesh = null;

        [SerializeField]
        Transform This = null;

        [SerializeField]
        Transform[] Points_array = new Transform[0];

        [SerializeField]
        int Active_id_point = 0;

        Transform Target = null;


        private void Start()
        {
            if(Target == null)
                Target = FindFirstObjectByType<PlayerController>().transform;

            if (Target == null)
                Target = GameObject.FindGameObjectWithTag("Player").transform;

            This.position = Points_array[Active_id_point].position;
            This.rotation = Points_array[Active_id_point].rotation;
        }

        private void OnBecameInvisible()
        {
            Teleport();
        }

        private void Update()
        {
            Eyes_rotation();
        }

        void Eyes_rotation()
        {
            Vector3 target_pos = Target.position;
            target_pos.y = 0;

            Vector3 my_pos = This.position;
            my_pos.y = 0;

            Vector3 target_direction = target_pos - my_pos;

            float angle = Vector3.Angle(target_direction, This.forward);

            float value = angle / 90f;
            
            value = Mathf.Clamp(value, 0f, 1f) * 100f;

            Vector3 enemy_direction_right = This.transform.InverseTransformPoint(Target.transform.position);

            if (angle <= 90f)
            {
                if (enemy_direction_right.x > 0)
                {
                    Mesh.SetBlendShapeWeight(1, value);
                    Mesh.SetBlendShapeWeight(0, 0);
                }
                else
                {
                    Mesh.SetBlendShapeWeight(0, value);
                    Mesh.SetBlendShapeWeight(1, 0);
                }
            }
            else
            {
                Mesh.SetBlendShapeWeight(0, 0);
                Mesh.SetBlendShapeWeight(1, 0);
            }

        }


        void Teleport()
        {
            if (Points_array.Length > 1)
            {
                while (true)
                {
                    int id = Random.Range(0, Points_array.Length);

                    if (Active_id_point != id)
                    {
                        This.position = Points_array[id].position;
                        This.rotation = Points_array[id].rotation;

                        Active_id_point = id;

                        break;
                    }
                }
            }

        }
    }
}