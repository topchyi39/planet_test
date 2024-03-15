using Player.Input;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Input.Input input;
        [SerializeField] private PlayerCharacter player;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Input.Input>().FromInstance(input);
            Container.BindInterfacesTo<PlayerCharacter>().FromInstance(player);
        }
    }
}