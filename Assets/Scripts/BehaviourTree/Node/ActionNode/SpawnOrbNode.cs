using UnityEngine;

public class SpawnOrbNode : ActionNode
{
    public GameObject prefab;
    public float offset;
    
    public override void CopyNode(Node copyNode)
    {
        SpawnOrbNode node = copyNode as SpawnOrbNode;
        
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        SpawnOrb();
    }

    void SpawnOrb()
    {
        Instantiate(prefab, treeComponent.player.transform.position, Quaternion.identity);
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
