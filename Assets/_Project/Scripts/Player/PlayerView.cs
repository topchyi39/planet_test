using System;
using Character;
using UnityEngine;

namespace Player
{
    public interface ICharacterView
    {
        void UpdateVerticalSmoothly(float value, float damp = -1);
    }
    
    public interface IPlayerView : ICharacterView
    {
        
        void RotateYSmooth(float rotation, float delta);
    }
    
    [RequireComponent(typeof(Animator))]
    public class PlayerView : CharacterView, IPlayerView
    {
        
        [SerializeField] private AnimationParameter horizontal;

        private float rotationVelocity; 

        public void RotateYSmooth(float y, float lerpTime)
        {
            var currentAngle = transform.localRotation.eulerAngles.y;
            var smoothAngle = Mathf.LerpAngle(currentAngle, y, lerpTime);
            var rotation = Quaternion.Euler(0, smoothAngle, 0);
            transform.localRotation = rotation;
        }
    }
}