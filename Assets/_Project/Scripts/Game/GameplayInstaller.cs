using Game.Levels;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private LevelEntry levelEntry;


        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LevelEntry>().FromInstance(levelEntry);
        }
    }
}