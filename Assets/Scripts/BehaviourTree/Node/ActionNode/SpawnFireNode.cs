using UnityEngine;

public class SpawnFireNode : ActionNode
{
    public GameObject prefab;
    public Vector2 offset;
    
    public override void CopyNode(Node copyNode)
    {
        SpawnFireNode node = copyNode as SpawnFireNode;
        
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
    }

    protected override NodeComponent.State OnUpdate()
    {
        GameObject fire = Instantiate(prefab.gameObject, treeComponent.transform.position + (Vector3)offset, Quaternion.identity);
        return NodeComponent.State.SUCCESS;
    }
    
    public override void DrawGizmos(GameObject selectedGameObject)
    {
        base.DrawGizmos(selectedGameObject);
        GizmosDrawer.DrawSphere(selectedGameObject.transform.position + (Vector3)offset, 0.5f);
    }
}
