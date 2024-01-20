using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Bullet", fileName = "BulletConfig")]
public class BulletConfig : ScriptableObject
{
    public Bullet Prefab;
    public LayerMask LayerMask;
}