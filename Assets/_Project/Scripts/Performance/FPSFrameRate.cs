using UnityEngine;

namespace Performance
{
    public class FPSFrameRate : MonoBehaviour
    {
        [SerializeField] private bool showFrameRate = true;
        [SerializeField] private bool unlockFrameRate = true;
        
        private float _currentFrameRate;
        
        private const int FontSize = 54;

        private void Awake()
        {
            if (unlockFrameRate)
            {
                Application.targetFrameRate = 999;
            }
        }

        private void Update()
        {
            if (Time.frameCount % 5 == 0) _currentFrameRate = 1f / Time.deltaTime;
        }

        private void OnGUI()
        {
            if (!showFrameRate) return;
            var fpsLabel = $"FPS: {_currentFrameRate:F}";
            var style = new GUIStyle
            {
                fontSize = FontSize,
                onNormal =
                {
                    textColor = Color.white
                }
            };

            GUI.Label(new Rect(0, 0, 150, 50), fpsLabel, style);
        }
    }
}