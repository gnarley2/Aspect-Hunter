using UnityEngine;

public class SpawnOrbNode : ActionNode
{
    public GameObject prefab;
    public Vector2 offset = Vector2.zero;
    public float waitTime = 0f;
    
    private Vector2 spawnPos;
    private float startTime = 0f;
    
    public override void CopyNode(Node copyNode)
    {
        SpawnOrbNode node = copyNode as SpawnOrbNode;
        if (node)
        {
            prefab = node.prefab;
            offset = node.offset;
            waitTime = node.waitTime;
        }
        
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        spawnPos = treeComponent.player.transform.position + (Vector3)offset;
        startTime = Time.time;
    }

    void SpawnOrb()
    {
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
    {
        if (startTime + waitTime <= Time.time)
        {
            SpawnOrb();
            return NodeComponent.State.SUCCESS;
        }
        return NodeComponent.State.RUNNING;
    }
    

}
