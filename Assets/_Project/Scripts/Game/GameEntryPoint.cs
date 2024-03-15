using SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    public class GameEntryPoint : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        
        [Inject]
        private void Construct(SceneLoader loader)
        {
            _sceneLoader = loader;
        }
        
        private void Start()
        {
            _sceneLoader.LoadStartMenu();
        }
    }
}
