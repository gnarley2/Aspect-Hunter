using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool _isPressingSwitch = false;
    float _switchDelay = 0.2f;
    Animator _animator;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isPressingSwitch)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        _animator.Play("Base Layer.DoorOpenAnimClip");
    }

    void CloseDoor()
    {
        _animator.Play("Base Layer.DoorCloseAnimClip");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPressingSwitch = !_isPressingSwitch;
        }
    }
}
