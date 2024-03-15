using SceneManagement.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SceneManagement
{
    public class SceneManagementInstaller : MonoInstaller
    {
        [SerializeField] private SceneFader sceneFader;

        public override void InstallBindings()
        {
            var presenter = new SceneLoaderPresenter(sceneFader);
            var sceneLoader = new SceneLoader(presenter);

            Container.BindInterfacesAndSelfTo<SceneLoader>().FromInstance(sceneLoader).AsSingle();
        }
    }
}