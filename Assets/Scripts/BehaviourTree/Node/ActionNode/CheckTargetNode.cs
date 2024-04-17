using UnityEngine;

public class CheckTargetNode : ActionNode
{
    public override void CopyNode(Node copyNode)
    {
        CheckTargetNode node = copyNode as CheckTargetNode;
        
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
        if (PlayerCombat.Target == null) return NodeComponent.State.FAILURE;
        return NodeComponent.State.SUCCESS;
    }
    

}
