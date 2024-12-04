using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowAnimator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip _FadeIn;
    [SerializeField] private AnimationClip _FadeOut;

    public void WindowFadeIn()
    {
        _animator.Play(_FadeIn.name);
    }

    public void WindowFadeOut()
    {
        _animator.Play(_FadeOut.name);
    }
}
