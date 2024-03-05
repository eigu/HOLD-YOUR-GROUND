using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEventSO GameEvent;
    public UnityEvent OnEventTriggered;

    private void OnEnable()
    {
        GameEvent.AddListener(this);
    }

    private void OnDisable()
    {
        GameEvent.RemoveListener(this);
    }

    public void OnEventTrigger()
    {
        OnEventTriggered.Invoke();
    }
}
