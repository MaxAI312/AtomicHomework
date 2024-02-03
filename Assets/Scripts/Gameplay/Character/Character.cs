using Homework3;
using UnityEngine;

public sealed class Character : MonoBehaviour
{
    public Character_Core Core;
    public Character_View View;

    public void Construct(ObjectPool objectPool)
    {
        Core.Construct(objectPool);
    }
    
    public void Start()
    {
        Core.Compose();
        View.Compose(Core);
        
        Core.OnEnable();
        View.OnEnable();
    }

    private void Update()
    {
        Core.Update();
        View.Update();
    }

    private void OnDestroy()
    {
        Core.OnDisable();
        View.OnDisable();
        
        Core.Dispose();
    }
}