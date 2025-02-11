using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BaseWindow : MonoBehaviour
    {
        private static readonly int _showTrigger = Animator.StringToHash("Show");
        private static readonly int _closeTrigger = Animator.StringToHash("Close");
        
        [SerializeField] private Animator _animator;

        public virtual void Show()
        {
            gameObject.SetActive(true);
            _animator.SetTrigger(_showTrigger);
        }
        
        public virtual void Close()
        {
            _animator.SetTrigger(_closeTrigger);
            gameObject.SetActive(false);
        }
    }
}