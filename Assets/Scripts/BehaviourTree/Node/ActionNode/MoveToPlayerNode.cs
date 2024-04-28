using UnityEngine;

public class MoveToPlayerNode : ActionNode
{
    public enum MoveToPlayerType
    {
        ToPlayer,
        AwayPlayer
    }

    public MoveToPlayerType type;
    
    public float speed;
    public float minDis = 0.5f;
    public float maxDis = 10f;

    private Vector2 destination;
    private Vector2 startPos;
    private Vector2 direction;
    
    public override void CopyNode(Node copyNode)
    {
        MoveToPlayerNode node = copyNode as MoveToPlayerNode;
        if (node)
        {
            speed = node.speed;
            type = node.type;
            minDis = node.minDis;
            maxDis = node.maxDis;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        startPos = treeComponent.transform.position;
        destination = treeComponent.player.transform.position;

        direction = (destination - startPos).normalized;
        if (type == MoveToPlayerType.AwayPlayer)
            direction = -direction;
    }

    protected override void OnStop()
    {
        movement.SetVelocityZero();
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
    {
        Move();
        if (CheckMove())
            return NodeComponent.State.SUCCESS;
        return NodeComponent.State.RUNNING;
    }
    
    void Move()
    {
        movement.SetVelocity(direction * speed);
    }

    bool CheckMove()
    {
        if (type == MoveToPlayerType.ToPlayer)
        {
            return Vector2.Distance(destination, treeComponent.transform.position) < minDis;
        }
        else if (type == MoveToPlayerType.AwayPlayer)
        {
            return Vector2.Distance(destination, treeComponent.transform.position) > maxDis;
        }

        return true;
    }
}
