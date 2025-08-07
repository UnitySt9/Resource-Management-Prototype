using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private ResourceManager _resourceManager;
        [SerializeField] private GameObject _popupPanel;
        [SerializeField] private TextMeshProUGUI _popupText;
        [SerializeField] private Button _holdButton;

        private void Start()
        {
            if (_popupPanel != null)
                _popupPanel.SetActive(false);
            UpdateResourcePopup();
            SetupHoldButton();
        }

        private void SetupHoldButton()
        {
            if (_holdButton != null)
            {
                EventTrigger trigger = _holdButton.gameObject.GetComponent<EventTrigger>();
                if (trigger == null)
                    trigger = _holdButton.gameObject.AddComponent<EventTrigger>();
                EventTrigger.Entry pointerDown = new EventTrigger.Entry();
                pointerDown.eventID = EventTriggerType.PointerDown;
                pointerDown.callback.AddListener((data) => { ShowResourcePopup(); });
                trigger.triggers.Add(pointerDown);
                EventTrigger.Entry pointerUp = new EventTrigger.Entry();
                pointerUp.eventID = EventTriggerType.PointerUp;
                pointerUp.callback.AddListener((data) => { HideResourcePopup(); });
                trigger.triggers.Add(pointerUp);
            }
        }

        private void ShowResourcePopup()
        {
            if (_popupPanel != null)
            {
                UpdateResourcePopup();
                _popupPanel.SetActive(true);
            }
        }

        private void HideResourcePopup()
        {
            if (_popupPanel != null)
            {
                _popupPanel.SetActive(false);
            }
        }

        private void UpdateResourcePopup()
        {
            if (_resourceManager == null || _popupText == null) return;
            Dictionary<string, int> resources = _resourceManager.GetAllResources();
            string text = "Ресурсы:\n";
            foreach (var pair in resources)
            {
                text += $"{pair.Key}: {pair.Value}\n";
            }
            _popupText.text = text;
        }
    }
}