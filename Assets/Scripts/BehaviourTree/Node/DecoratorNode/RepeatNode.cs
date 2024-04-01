using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatNode : DecoratorNode
{

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override NodeComponent.State OnUpdate()
    {
        NodeComponent.State childState = child.Update();
        if (childState == NodeComponent.State.SUCCESS)
        {
            // child.();    
        }
        
        return NodeComponent.State.RUNNING;
    }
}
