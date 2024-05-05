using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    public string ItemName;
    public Sprite Icon;

    #if UNITY_EDITOR
    
    void OnValidate()
    {
        string path = UnityEditor.AssetDatabase.GetAssetPath(this);
        id = UnityEditor.AssetDatabase.AssetPathToGUID(path);
    }
    
    #endif
}