using UnityEngine;

public class FireController
{
    private readonly Camera _camera;
    private readonly IAttackable _attackable;
    private readonly Transform _startPoint;
    
    public FireController(Camera camera, IAttackable attackable, Transform startPoint)
    {
        _camera = camera;
        _attackable = attackable;
        _startPoint = startPoint;
    }
    
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 targetPoint = hit.point;
                Vector3 direction = (targetPoint - _startPoint.position).normalized;

                Debug.DrawLine(_startPoint.position, hit.point, Color.red, duration: 2.0f);

                _attackable?.Fire(direction);
            }
        }
    }
}