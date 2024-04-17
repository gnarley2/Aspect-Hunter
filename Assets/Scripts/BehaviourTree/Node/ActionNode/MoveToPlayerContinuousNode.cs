using UnityEngine;

public class MoveToPlayerContinuousNode : ActionNode
{
    
    public float speed;
    
    private Vector2 direction;
    
    public override void CopyNode(Node copyNode)
    {
        MoveToPlayerContinuousNode node = copyNode as MoveToPlayerContinuousNode;
        
        if (node)
        {
            speed = node.speed;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnStop()
    {
        base.OnStop();
        movement.SetVelocityZero();
    }

    protected override NodeComponent.State OnUpdate()
    {
        Move();

        return NodeComponent.State.RUNNING;
    }
    
    void Move()
    {
        direction = (treeComponent.player.transform.position - treeComponent.transform.position).normalized;
        movement.SetVelocity(direction * speed);
    }


}
