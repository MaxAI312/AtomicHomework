using UnityEngine;

public sealed class Character : MonoBehaviour
{
    public Character_Core Core;
    public Character_View View;
    
    private void Awake()
    {
        Core.Compose();
        View.Compose(Core);
    }
    

    private void OnEnable()
    {
        View.OnEnable();
    }

    private void OnDisable()
    {
        View.OnDisable();
    }

    private void Update()
    {
        Core.Update();
        View.Update();
    }

    private void OnDestroy()
    {
        Core.Dispose();
    }
}