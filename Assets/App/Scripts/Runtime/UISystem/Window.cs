using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _content;
    [SerializeField] private Animator _animator;

    public void WindowFadeIn()
    {
        _animator.Play("In");
    }

    public void WindowFadeOut()
    {
        _animator.Play("Out");
    }

    public void SetWindowActiveFalse() => _content.SetActive(false);

    public void SetWindowActiveTrue() => _content.SetActive(true);
}
