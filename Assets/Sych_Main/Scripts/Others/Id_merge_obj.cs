using UnityEngine;

namespace Sych_scripts
{
    [DisallowMultipleComponent]
    public class Id_merge_obj : MonoBehaviour
    {

        [field: SerializeField]
        public int Id { get; private set; } = 0;

    }
}