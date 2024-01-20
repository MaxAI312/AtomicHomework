using UnityEngine;
using Object = UnityEngine.Object;

public class FireMechanics
{
    private readonly Transform _startPoint;
    private readonly BulletConfig _bulletConfig;
    private readonly Camera _camera;

    public FireMechanics(Transform startPoint, BulletConfig bulletConfig, Camera camera)
    {
        _startPoint = startPoint;
        _bulletConfig = bulletConfig;
        _camera = camera;
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
                
                Bullet instance = Object.Instantiate(_bulletConfig.Prefab, _startPoint.position, Quaternion.identity, null);
                instance.Setup(direction, _bulletConfig.LayerMask);
            }
        }
    }
}
