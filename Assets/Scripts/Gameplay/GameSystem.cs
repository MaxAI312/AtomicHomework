using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Character _character;
    [SerializeField] private Transform _startShootPoint;
    
    private FireController _fireController;
    
    private void Awake()
    {
        _fireController = new FireController(_camera, _character, _startShootPoint);
    }

    private void Update()
    {
        _fireController.Update();
    }
}
