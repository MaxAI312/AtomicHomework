using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPool", menuName = "Project/Configs/ObjectPool")]
public class ObjectPoolConfig : ScriptableObject
{
    [SerializeField] private int _size;
    [SerializeField] private bool _shouldExpand;
    [SerializeField] private GameObject _prefab;

    public int Size => _size;
    public bool ShouldExpand => _shouldExpand;
    public GameObject Prefab => _prefab;
}
