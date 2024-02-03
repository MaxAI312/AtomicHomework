using UnityEngine;

[CreateAssetMenu(fileName = "BulletConfig", menuName = "Project/Configs/Bullet")]
public class BulletConfig : ScriptableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private float _lifetime;
    
    public int Damage => _damage;
    public float Lifetime => _lifetime;
}
