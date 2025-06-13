using UnityEngine;

namespace Sych_scripts
{
    public class Sprout : MonoBehaviour, I_Watering
    {
        [SerializeField]
        GameObject[] Phase_array = new GameObject[4];

        [SerializeField]
        GameObject[] Fin_array = new GameObject[0];

        [SerializeField]
        float Water_value = 10f;

        [SerializeField]
        ParticleSystem PS_up_phase = null;

        float Active_value_water = 0f;

        int Id_phase = 0;
        private void Start()
        {
            for(int x = 1; x < Phase_array.Length; x++)
            {
                Phase_array[x].SetActive(false);
            }

            for (int x = 0; x < Fin_array.Length; x++)
            {
                Fin_array[x].SetActive(false);
            }
        }

        public void Add_water()
        {
            Active_value_water += 0.1f;

            Update_method();
        }

        public void Add_water(float _value)
        {
            Active_value_water += _value;

            Update_method();
        }

        void Update_method()
        {
            float phase_value = Water_value / (float)Phase_array.Length;

            if (Id_phase < Phase_array.Length)
                if (phase_value * (Id_phase + 1) <= Active_value_water)
                {
                    PS_up_phase.Play();


                    Phase_array[Id_phase].gameObject.SetActive(false);

                    Id_phase++;

                    if (Id_phase == Phase_array.Length)
                    {
                        int id_fin = Random.Range(0, Fin_array.Length);

                        Fin_array[id_fin].SetActive(true);
                    }
                    else
                        Phase_array[Id_phase].gameObject.SetActive(true);
                }
        }
    }
}