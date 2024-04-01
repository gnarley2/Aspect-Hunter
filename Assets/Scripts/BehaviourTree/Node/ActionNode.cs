using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionNode : Node
{
    protected Movement movement { get => _movement ??= treeComponent.core.GetCoreComponent<Movement>(); }
    private Movement _movement;

    protected AnimatorController anim { get => _anim ??= treeComponent.core.GetCoreComponent<AnimatorController>(); }
    private AnimatorController _anim;

    protected CollisionChecker collisionChecker { get => _collisionChecker ??= treeComponent.core.GetCoreComponent<CollisionChecker>(); }
    private CollisionChecker _collisionChecker;

    public Node linkNode;

    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
        if (linkNode != null)
        {
            CopyNode(linkNode);
        }
    }

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }


}
