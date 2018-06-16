using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {


    protected Animator _animator;
    private GameObject target;

    void Awake()
    {
        _animator = GetComponent<Animator>();

    }

    public void PlaySpin()
    {
        _animator.Play(Animator.StringToHash("Spin"));
    }

    public void PlayZoomIn()
    {
        _animator.Play(Animator.StringToHash("ZoomIn"));
    }

    public void PlayZoomOut()
    {
        _animator.Play(Animator.StringToHash("ZoomOut"));
    }

    public void DisableCrosshair()
    {
        gameObject.SetActive(false);
    }

    public void AssignTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.5f);
        }
        

    }


}
