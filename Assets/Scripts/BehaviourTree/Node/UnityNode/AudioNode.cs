using UnityEngine;

[NodeAttribute("Unity/Audio")]
public class AudioNode : ActionNode
{
    public AudioClip clip;
    
    public override void CopyNode(Node copyNode)
    {
        AudioNode node = copyNode as AudioNode;
        if (node)
        {
            clip = node.clip;
        }
    }
    
    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }
    
    protected override void OnStart()
    {
        base.OnStart();
        SoundManager.Instance.PlayOneShot(clip);
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
