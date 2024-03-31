using UnityEngine;
using UnityEngine.Serialization;

public class VFXNode : ActionNode
{
    [SerializeField] private GameObject vfx;
    
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
        
        //todo
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
