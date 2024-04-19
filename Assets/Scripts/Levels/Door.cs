using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Switch doorSwitch;
    Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
        doorSwitch.OnActivate += OpenDoor;
    }

    void OpenDoor()
    {
        anim.Play("Open");
    }
    
    void CloseDoor()
    {
        anim.Play("Close");
    }

}
