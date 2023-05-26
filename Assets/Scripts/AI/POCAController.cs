using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class POCAController : MonoBehaviour
{
    [Header("Max Environment Steps")] public int MaxEnvironmentSteps = 25000;

    private SimpleMultiAgentGroup agentGroup = new SimpleMultiAgentGroup();
    private int resetTimer;
    public bool collabGoalReached = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform.parent)
        {
            if (child.tag == "Agent")
            {
                agentGroup.RegisterAgent(child.GetComponent<NewAgent>());
            }
        }
    }

    void FixedUpdate()
    {
        resetTimer += 1;
        if (resetTimer >= MaxEnvironmentSteps && MaxEnvironmentSteps > 0)
        {
            agentGroup.GroupEpisodeInterrupted();
            resetTimer = 0;
        }
    }

    public void ObstacleCollided()
    {
        collabGoalReached = false;
        agentGroup.SetGroupReward(-1f);
        agentGroup.EndGroupEpisode();
    }

    public void GoalReached()
    {
        collabGoalReached = true;
        agentGroup.SetGroupReward(+2f);
        agentGroup.EndGroupEpisode();
    }
}
