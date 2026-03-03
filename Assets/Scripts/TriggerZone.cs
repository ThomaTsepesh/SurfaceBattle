using UnityEngine;

namespace SB
{
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField] private WorldTooltip _tooltip;
        
        private void OnTriggerEnter(Collider foreign)
        {
            if (foreign.CompareTag("Player"))
                _tooltip.Show();
        }

        private void OnTriggerExit(Collider foreign)
        {
            if (foreign.CompareTag("Player"))
                _tooltip.Hide();
        }
    }
}