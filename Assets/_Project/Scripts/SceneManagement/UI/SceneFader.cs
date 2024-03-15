using DG.Tweening;
using UI;
using UnityEngine;

namespace SceneManagement.UI
{
    public class SceneFader : MonoBehaviour, IViewFader
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasGroup canvasGroup;
        [Header("Fade Settings")]
        [SerializeField] private float duration;
        [Header("References")] 
        [SerializeField] private Transform planetIcon;
        
        private Sequence _fadeSequence;
        private Sequence _planetIconSequence;
        
        public void FadeIn()
        {
            if(_fadeSequence is { active: true }) _fadeSequence.Kill();

            _fadeSequence = DOTween.Sequence();
            _fadeSequence.AppendCallback(() => canvas.enabled = true);
            _fadeSequence.AppendCallback(StartPlanetAnimation);
            _fadeSequence.Append(canvasGroup.DOFade(1, duration));
            _fadeSequence.AppendCallback(() => canvasGroup.blocksRaycasts = true);
        }

        public void FadeOut(bool immediately = false)
        {
            if(_fadeSequence is { active: true }) _fadeSequence.Kill();
            
            if (immediately)
            {
                canvas.enabled = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0;
                return;
            }
            
            _fadeSequence = DOTween.Sequence();

            _fadeSequence.AppendCallback(() => canvasGroup.blocksRaycasts = false);
            _fadeSequence.Append(canvasGroup.DOFade(0, duration));
            _fadeSequence.AppendCallback(() => canvas.enabled = false);
            _fadeSequence.AppendCallback(StopPlanetAnimation);
        }

        private void StartPlanetAnimation()
        {
            if(_planetIconSequence is { active: true }) _planetIconSequence.Kill();

            _planetIconSequence = DOTween.Sequence();
            _planetIconSequence.Append(planetIcon.DOScale(Vector3.one * 1.1f, 0.2f));
            _planetIconSequence.Append(planetIcon.DOScale(Vector3.one, 0.1f));
            _planetIconSequence.SetLoops(-1);
        }

        private void StopPlanetAnimation()
        {
            if(_planetIconSequence is { active: true }) _planetIconSequence.Kill();
            
            planetIcon.localScale = Vector3.one;
        }
    }
}