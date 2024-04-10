using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    public BehaviourTree tree;
    public BehaviourTreeComponent treeComponent {get; private set;}
    

    private void Awake()
    {
        InitializeTree(tree);
    }
    
    public void InitializeTree(BehaviourTree tree)
    {
        if (tree == null) return;
        
        StartCoroutine(InitializeTreeCoroutine(tree));
    }

    IEnumerator InitializeTreeCoroutine(BehaviourTree tree)
    {
        this.tree = tree;
        InitializeTreeComponent();
        CloneTree();

        yield return null;
        
        InitializeNodeComponent();
    }

    public void InitializeTreeComponent() 
    {
        treeComponent = BehaviourTreeComponent.CreateTreeComponentFromGameObject(gameObject, GameObject.FindWithTag("Player"));
    }

    void CloneTree() 
    {
        tree = tree.Clone();
    }

    void InitializeNodeComponent()
    {
        tree.Traverse(tree.rootNode, (n) =>
        {
            n.OnInitialize(treeComponent);
        });
    }
    


    void Update()
    {
        tree.Update();
    }
}
