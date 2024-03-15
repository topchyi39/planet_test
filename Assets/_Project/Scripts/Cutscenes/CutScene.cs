using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace Cutscenes
{
    public class CutScene : MonoBehaviour
    {
        [SerializeField] private PlayableDirector cutsceneDirector;

        private Action _callback;
        
        public void StartCutscene(Action callback)
        {
            _callback = callback;
            cutsceneDirector.Play();
        }

        public void FadeOut()
        {
            _callback?.Invoke();
        }
    }
}