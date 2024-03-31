using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D rb {get; private set;} 
    public Vector2 faceDirection {get; private set;}

    bool canSetVelocity = true;
    float gravityScale;
    
    protected override void Awake() 
    {
        base.Awake();
        
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Start()
    {
        InitializeDirection();

        gravityScale = rb.gravityScale;
    }
    
    #region Velocity

    public void SetVelocity(Vector2 velocity, bool needToChangeFaceDirection = true) 
    {
        if (needToChangeFaceDirection)
        {
            ChangeDirection(velocity.x);
        }

        if (!canSetVelocity) return;

        rb.velocity = velocity;
    }

    public void SetVelocityX(float xVelocity, bool needToChangeFaceDirection = true)
    {
        if (needToChangeFaceDirection)
        {
            ChangeDirection(xVelocity);
        }

        if (!canSetVelocity) return;
        
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    public void SetVelocityY(float yVelocity) 
    {
        if (!canSetVelocity) return;
        
        rb.velocity = new Vector2(rb.velocity.x, yVelocity);
    }

    public void SetVelocityZero() 
    {
        rb.velocity = Vector2.zero;
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    public void AddForce(Vector2 direction, float amount)
    {
        rb.AddForce(direction * amount, ForceMode2D.Impulse);
    }

    #endregion

    #region Gravity

    public void SetGravityZero()
    {
        rb.gravityScale = 0;
    }

    public void SetGravityNormal()
    {
        rb.gravityScale = gravityScale;
    }

    public void SetGravityScale(float scale)
    {
        rb.gravityScale = scale;
    }


    #endregion

    #region Position

    public Vector2 GetPosition()
    {
        return rb.position;
    }

    public void SetPosition(Vector2 newPos)
    {
        rb.position = newPos;
    }

    #endregion

    #region Direction

    private void InitializeDirection()
    {
        faceDirection = Vector2.left;
    }

    public void ChangeDirection(float xInput)
    {
        if (xInput < 0)
        {
            if (faceDirection == Vector2.right) Flip();
            
            faceDirection = Vector2.left;
            
        }
        else if (xInput > 0)
        {
            if (faceDirection == Vector2.left) Flip();
            
            faceDirection = Vector2.right;
        }
    }

    void Flip() 
    {
        rb.transform.Rotate(0, 180, 0);
    }

    public float GetDirectionMagnitude()
    {
        return faceDirection == Vector2.left ? -1 : 1;
    }

    public Vector2 GetWorldPosFromRelativePos(Vector2 relativePos)
    {
        return new Vector2(transform.position.x - relativePos.x * GetDirectionMagnitude(), transform.position.y + relativePos.y);
    }

    #endregion
    
}
