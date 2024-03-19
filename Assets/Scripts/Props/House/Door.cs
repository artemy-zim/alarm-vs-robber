using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int _isOpen = Animator.StringToHash("isOpen");

    private void OnEnable()
    {
        if(_animator == null)
        {
            enabled = false;
            throw new ArgumentNullException(nameof(_animator));
        }
    }

    public void Open()
    {
        _animator.SetBool(_isOpen, true);
    }

    public void Close()
    {
        _animator.SetBool(_isOpen, false);    
    }
}
