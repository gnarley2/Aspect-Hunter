using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Switch doorSwitch;
    
    Animator anim;
    private Collider2D col;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        doorSwitch.OnActivate += OpenDoor;
    }

    void OpenDoor()
    {
        anim.Play("Open");
        col.enabled = false;
    }
    
    void CloseDoor()
    {
        anim.Play("Close");
    }

}
