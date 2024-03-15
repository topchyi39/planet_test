using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Menu.UI
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Button startButton;

        public event Action StartPressed;
        
        private void Start()
        {
            startButton.onClick.AddListener(()=>StartPressed?.Invoke());
        }
    }
}