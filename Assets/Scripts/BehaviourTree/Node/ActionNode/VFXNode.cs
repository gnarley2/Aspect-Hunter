using UnityEngine;
using UnityEngine.Serialization;

public class VFXNode : ActionNode
{
    [SerializeField] private VFXObject vfx;
    [SerializeField] private Vector2 spawnPos;
    
    public override void CopyNode(Node copyNode)
    {
        VFXNode node = copyNode as VFXNode;
        if (node)
        {
            vfx = node.vfx;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }
    
    protected override void OnStart()
    {
        base.OnStart();

        Instantiate(vfx.gameObject, treeComponent.transform.position + (Vector3)spawnPos, Quaternion.identity);
    }

    protected override void OnStop()
    {
        base.OnStop();
    }

    protected override NodeComponent.State OnUpdate()
    {
        return NodeComponent.State.SUCCESS;
    }

    public override void DrawGizmos(GameObject selectedGameObject)
    {
        base.DrawGizmos(selectedGameObject);
        GizmosDrawer.DrawSphere(selectedGameObject.transform.position + (Vector3)spawnPos, 0.5f);
    }
}
