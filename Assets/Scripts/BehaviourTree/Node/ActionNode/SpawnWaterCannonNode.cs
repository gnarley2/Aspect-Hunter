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
        Instantiate(prefab, (Vector2)treeComponent.transform.position - movement.GetDirectionMagnitude() * offset, Quaternion.identity);
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
