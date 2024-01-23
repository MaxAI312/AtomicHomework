using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private Character _character;
    
    public void Update()
    {

        Vector3 direction = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("GetKeyDown");
            direction.z = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("GetKeyDown");

            direction.x = -1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("GetKeyDown");
            direction.z = -1f;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("GetKeyDown");
            direction.x = 1f;
        }

        _character.MovementDirection.Value = direction;
    }
}
