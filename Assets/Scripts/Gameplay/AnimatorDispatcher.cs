using UnityEngine;

public class AnimatorDispatcher : MonoBehaviour
{
    public event AnimatorEventDelegate OnFireEvent;

    internal void ReceiveShoot()
    {
        OnFireEvent?.Invoke();
    }
}

public delegate void AnimatorEventDelegate();
