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

        Vector2 spawnPosition = (Vector2)treeComponent.transform.position + offset;
        Quaternion rotation;

        // Check if the player is on the left or right side
        if (treeComponent.transform.position.x < GameObject.FindGameObjectWithTag("Player").transform.position.x)
        {
            // Player is on the right side, rotate the prefab to the left
            rotation = Quaternion.Euler(0f, 0f, 180f);
            Instantiate(prefab, spawnPosition, rotation);
        }
        if (treeComponent.transform.position.x > GameObject.FindGameObjectWithTag("Player").transform.position.x)
        {
            // Player is on the left side, rotate the prefab to the right
            rotation = Quaternion.Euler(0f, 0f, -180f);
            Instantiate(prefab, spawnPosition, rotation);
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
