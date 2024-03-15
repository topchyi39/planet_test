using UnityEngine;

namespace Environment
{
    [ExecuteAlways]
    public class UpPlacement : MonoBehaviour
    {
        [SerializeField] private float offset;
        
        
        [ContextMenu("Place")]
        private void Place()
        {
            if (Physics.Raycast(transform.position, -transform.position, out var hit))
            {
                transform.up = hit.normal;
                transform.position = hit.point + hit.normal * offset;
            }
        }
        
    }
}