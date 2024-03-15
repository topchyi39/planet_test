using System;
using System.Collections.Generic;
using Cutscenes;
using SceneManagement;
using UnityEngine;
using Zenject;

namespace Game.Levels
{
    public interface ILevelListener
    {
        void OnLevelPrepared();
        void OnLevelStarted();
    }

    public interface ILevel
    {
        void Register(ILevelListener levelListener);
    }
    
    public class LevelEntry : MonoBehaviour, ILevel
    {
        [SerializeField] private CutScene cutScene;

        private List<ILevelListener> _listeners = new(10);
        private SceneLoader _loader;
        
        [Inject]
        private void Construct(SceneLoader loader)
        {
            _loader = loader;
        }
        
        private void Start()
        {
            PrepareLevel();
            if (cutScene)
            {
                cutScene.StartCutscene(StartLevel);
            }
            else
            {
                StartLevel();
            }
        }

        public void Register(ILevelListener levelListener)
        {
            if (_listeners.Contains(levelListener)) return;
            
            _listeners.Add(levelListener);
        }

        private void PrepareLevel()
        {
            foreach (var listener in _listeners)
            {
                listener.OnLevelPrepared();
            }
        }

        private void StartLevel()
        {
            _loader.FakeLoad(1f, () =>
            {
                foreach (var listener in _listeners)
                {
                    listener.OnLevelStarted();
                }
            });


        }
    }
}