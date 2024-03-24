using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public bool getconfused = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        // 最初使agent不活动
        agent.enabled = false;
    }

    void Update()
    {
        // 玩家存在且agent未被混乱或停止
        if (player != null && !getconfused)
        {
            // 检查agent是否在摄像机视野内
            if (IsAgentVisibleToCamera() && !agent.enabled)
            {
                // 激活agent并设置目标为玩家位置
                agent.enabled = true;
                agent.SetDestination(player.position);
            }
            else if (agent.enabled)
            {
                Debug.Log(player.position);
                // 如果agent已激活，则继续追逐玩家
                agent.SetDestination(player.position);
            }
        }
    }

    bool IsAgentVisibleToCamera()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null) // 确保Renderer组件存在
        {
            Bounds bounds = renderer.bounds;
            return GeometryUtility.TestPlanesAABB(planes, bounds);
        }
        else
        {
            Debug.LogWarning("Renderer not found on " + gameObject.name);
            return false;
        }
    }
}
