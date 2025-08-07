using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] private List<string> _resourceTypes = new List<string>();
        private Dictionary<string, int> _resources = new Dictionary<string, int>();

        private void Awake()
        {
            foreach (var type in _resourceTypes)
            {
                if (!_resources.ContainsKey(type))
                    _resources[type] = 0;
            }
        }

        public void AddResource(string resourceName, int amount)
        {
            if (!_resources.ContainsKey(resourceName))
                _resources[resourceName] = 0;
            _resources[resourceName] += amount;
        }

        public int GetResourceAmount(string resourceName)
        {
            if (_resources.TryGetValue(resourceName, out int amount))
                return amount;
            return 0;
        }

        public Dictionary<string, int> GetAllResources()
        {
            return new Dictionary<string, int>(_resources);
        }
    }
}