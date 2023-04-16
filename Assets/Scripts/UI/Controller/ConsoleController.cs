using System;
using System.Collections.Generic;
using TMPro;
using UI.View;
using UnityEngine;

namespace UI.Controller
{
    public class ConsoleController : MonoBehaviour
    {
        
        public static ConsoleController Singleton { get; private set; }

        [SerializeField] private ConsoleView _consoleView;
        [SerializeField] private GameObject _consoleListItemPrefab;

        private readonly List<TMP_Text> logs = new();
        
        private void Awake()
        {
            if (Singleton != null)
                throw new Exception($"Detected more than 1 instance of {nameof(ConsoleController)}");

            Singleton = this;
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.BackQuote)) return;
            
            var consoleIsActive = _consoleView.gameObject.activeSelf;
            _consoleView.gameObject.SetActive(!consoleIsActive);
        }

        public void WriteLog(string text)
        {
            Debug.Log(text);
            var logItem = Instantiate(_consoleListItemPrefab, Vector3.zero, Quaternion.identity, _consoleView.ListRoot);
            var logItemText = logItem.GetComponent<TMP_Text>();
            logItemText.SetText(text);
            logs.Add(logItemText);
        }
        
    }
}