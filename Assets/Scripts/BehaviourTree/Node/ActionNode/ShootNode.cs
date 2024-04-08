using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class ShootNode : ActionNode
{
    [SerializeField] private GameObject prefab;


    public override void CopyNode(Node copyNode)
    {
        ShootNode node = copyNode as ShootNode;

        if (node)
        {
            prefab = node.prefab;
        }
    }

    public override void OnInitialize(BehaviourTreeComponent component)
    {
        base.OnInitialize(component);
    }

    protected override void OnStart()
    {
        base.OnStart();

        
        Shoot();
    }

    void Shoot()
    {
        // Vector2 direction = (treeComponent.player.transform.position -)
        // Instantiate()
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