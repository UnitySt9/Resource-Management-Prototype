using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class BuildingLabel : MonoBehaviour
    {
        [SerializeField] private Building _building;
        [SerializeField] private TextMeshProUGUI _labelText;

        private void Update()
        {
            if (_building != null && _labelText != null)
            {
                _labelText.text = $"{_building.GetResourceName()}: {_building.GetResourceAmount()}";
            }
        }
    }
}