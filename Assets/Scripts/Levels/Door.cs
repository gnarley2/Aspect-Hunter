using UnityEngine;

public class Door : MonoBehaviour
{
    bool _hasKey = false;
    Animator _animator;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_hasKey)
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
            _hasKey = InventoryManager.Instance.Find();
            Debug.Log(_hasKey.ToString());
        }
    }
}
