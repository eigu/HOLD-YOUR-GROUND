using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private float m_speed
    {
        get { return transform.GetComponent<Enemy>().CurrentSpeed; }
    }

    private Transform m_player;
    private NavMeshAgent m_agent;

    private void Start()
    {
        m_player = FindObjectOfType<Player>().transform;
        m_agent = GetComponent<NavMeshAgent>();

        m_agent.speed = m_speed;
    }

    private void Update()
    {
        if (m_player != null) m_agent.destination = m_player.position;
    }
}
