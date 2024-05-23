using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 _movement;

    // Animations and states
    Animator _anim;
    string _currState;
    const string PLAYER_IDLE = "MC_Idle_Anim";
    const string PLAYER_WALK_LEFT = "MC_L_Walk_Anim";
    const string PLAYER_WALK_RIGHT = "MC_R_Walk_Anim";
    const string PLAYER_WALK_UP = "MC_L_Walk_Anim";
    const string PLAYER_WALK_DOWN = "MC_R_Walk_Anim";


    const string IDLE = "idle [new]";
    const string WALK_LEFT = "Walking Left";
    const string WALK_Right = "Walking Right";


    // Update is called once per frame
    void Start()
    {
        GameManager.Instance.SetPlayer(transform);
        _anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        // Input
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (_movement.x > 0)
        {
            ChangeAnimationState(WALK_Right);
        }
        else if (_movement.x < 0)
        {
            ChangeAnimationState(WALK_LEFT);
        }
        else if (_movement.y > 0)
        {
            ChangeAnimationState(WALK_Right);
        }
        else if (_movement.y < 0)
        {
            ChangeAnimationState(WALK_LEFT);
        }
        else
        {
            ChangeAnimationState(IDLE);
        }

        rb.MovePosition(rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
    }

    void ChangeAnimationState(string newState)
    {
        // Stop animation interruption
        if (_currState == newState)
        {
            return;
        }

        // Play new animation
        _anim.Play(newState);

        // Update current state
        _currState = newState;
    }


}
