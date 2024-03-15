using System;
using System.Threading.Tasks;
using SceneManagement.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    
    
    public class SceneLoader
    {
        private event Action SceneStartLoad;
        private event Action SceneEndLoad;

        private const float LoadLimit = 0.88f;
        private const float Delay = 0.1f;
        private const float MinLoadDuration = 1f;

        public SceneLoader(SceneLoaderPresenter presenter)
        {
            SceneStartLoad += presenter.ShowLoadScreen;
            SceneEndLoad += presenter.HideLoadScreen;
            presenter.HideLoadScreenImmediately();
        }
        
        public void LoadLevel(int level)
        {
            LoadScene(level + 1, true);
        }

        public void LoadStartMenu()
        {
            LoadScene(1, true);
        }

        private async void LoadScene(int sceneIndex, bool active)
        {
            SceneStartLoad?.Invoke();

            var asyncOperation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            
            asyncOperation.allowSceneActivation = false;
            var time = 0f;
            
            while(asyncOperation.progress < LoadLimit || time < MinLoadDuration)
            {
                await Task.Delay(TimeSpan.FromSeconds(Delay));
                time += Delay;
            }
            
            asyncOperation.allowSceneActivation = true;

            while (!asyncOperation.isDone )
            {
                await Task.Delay(TimeSpan.FromSeconds(Delay));
                time += Delay;
            }
            
            UnloadActiveScene();

            if(active)
            {
                var scene = SceneManager.GetSceneByBuildIndex(sceneIndex);
                if (scene.isLoaded) SceneManager.SetActiveScene(scene);
            }

            SceneEndLoad?.Invoke();
        }

        private void UnloadActiveScene()
        {
            var activeScene = SceneManager.GetActiveScene();
            if (!activeScene.isLoaded) return;

            SceneManager.UnloadSceneAsync(activeScene);
        }

        public async void FakeLoad(float duration, Action action)
        {
            SceneStartLoad?.Invoke();
            await Task.Delay(TimeSpan.FromSeconds(duration));
            action?.Invoke();
            SceneEndLoad?.Invoke();
        
        }
    }
}