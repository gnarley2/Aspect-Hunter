using UnityEngine;

public class ShootEnemyNode : ActionNode
{
    public ProjectileMovement prefab;
    public Vector2 offset;
    
    public override void CopyNode(Node copyNode)
    {
        ShootEnemyNode node = copyNode as ShootEnemyNode;
        

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
        if (PlayerCombat.Target == null) return;
        
        Vector2 direction = (PlayerCombat.Target.GetPosition() - (Vector2)treeComponent.transform.position).normalized;
             
        GameObject projectile = Instantiate(prefab.gameObject, treeComponent.transform.position + (Vector3)offset, Quaternion.identity);
        projectile.transform.up = direction;

        ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();       

        // Pass the direction to the ProjectileMovement script
        projectileMovement.Initialize(direction, IDamageable.DamagerTarget.TamedMonster, 1);
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
