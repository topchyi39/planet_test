using System;
using Player;
using UnityEngine;

namespace Character
{
    public class CharacterView : MonoBehaviour, ICharacterView
    {
        [Serializable]
        protected class AnimationParameter
        {
            public int Hash
            {
                get
                {
                    if (_hash == 0) _hash = Animator.StringToHash(parameterName);

                    return _hash;
                }
            }

            [SerializeField] private string parameterName;

            private int _hash = 0;
        }
        
        [SerializeField] private Animator animator;
        [Header("Settings")] 
        [SerializeField] private float dampTime = 0.2f;
        [Header("Animation Parameters")]
        [SerializeField] private AnimationParameter vertical;
        
        
        private void OnValidate()
        {
            animator ??= GetComponent<Animator>();
        }

        public void UpdateVerticalSmoothly(float value, float damp = -1)
        {
            if (damp < 0) damp = dampTime;
            
            animator.SetFloat(vertical.Hash, value, damp, Time.deltaTime);
        }
    }
}