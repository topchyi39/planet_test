using Game.Levels;
using UnityEngine;
using Zenject;

namespace Allies
{
    public class AllyController : MonoBehaviour, ILevelListener
    {
        [SerializeField] private Ally[] allies;
        
        [Inject]
        private void Construct(ILevel level)
        {
            level.Register(this);
        }

        public void OnLevelPrepared()
        {
            foreach (var ally in allies)
                ally.gameObject.SetActive(false);
        }

        public void OnLevelStarted()
        {
            foreach (var ally in allies)
                ally.gameObject.SetActive(true);
        }
    }
}