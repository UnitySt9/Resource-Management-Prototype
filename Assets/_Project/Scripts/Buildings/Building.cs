using UnityEngine;

namespace _Project.Scripts
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private string _resourceName;
        [SerializeField] private int _resourceAmount = 0;
        [SerializeField] private int _productionPerInterval = 1;
        [SerializeField] private float _productionInterval = 2f;

        private float _timer;

        void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _productionInterval)
            {
                _resourceAmount += _productionPerInterval;
                _timer = 0f;
            }
        }

        public int CollectResource()
        {
            int collected = _resourceAmount;
            _resourceAmount = 0;
            return collected;
        }

        public string GetResourceName() => _resourceName;
        public int GetResourceAmount() => _resourceAmount;
    }
}