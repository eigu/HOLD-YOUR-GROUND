using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEventSO : ScriptableObject
{
    private List<GameEventListener> m_listeners = new List<GameEventListener>();

    public void TriggerEvent()
    {
        for (int i = m_listeners.Count - 1; i >= 0; i--)
        {
            m_listeners[i].OnEventTrigger();
        }
    }

    public void AddListener(GameEventListener listener)
    {
        m_listeners.Add(listener);
    }

    public void RemoveListener(GameEventListener listener)
    {
        m_listeners.Remove(listener);
    }
}
