using UnityEngine;

public class SpawnWaterCannonNode : ActionNode
{
    public GameObject prefab;
    public Vector2 offset;
    
    public override void CopyNode(Node copyNode)
    {
        SpawnWaterCannonNode node = copyNode as SpawnWaterCannonNode;
        
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        float direction = movement.GetDirectionMagnitude();
        Vector2 spawnPos = movement.GetWorldPosFromRelativePos(offset);
        if (direction == -1)
        {
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
        else
        {
            Instantiate(prefab, spawnPos, Quaternion.Euler (0f, 180f, 0f));
        }
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
    {
        return NodeComponent.State.SUCCESS;
    }
    

}
