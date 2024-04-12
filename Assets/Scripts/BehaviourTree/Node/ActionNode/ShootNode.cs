using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class ShootNode : ActionNode
{
    public ProjectileMovement prefab;
    public Vector2 offset;
    
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
        Vector2 direction = (treeComponent.player.transform.position - treeComponent.transform.position).normalized;
             
         GameObject projectile = Instantiate(prefab.gameObject, treeComponent.transform.position + (Vector3)offset, Quaternion.identity);
         projectile.transform.up = direction;
         
         ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();       
        
         // Pass the direction to the ProjectileMovement script
         projectileMovement.Initialize(direction, IDamageable.DamagerTarget.Enemy, 1);
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
        GizmosDrawer.DrawSphere(selectedGameObject.transform.position + (Vector3)offset, 0.5f);
    }
}