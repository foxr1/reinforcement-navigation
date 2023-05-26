using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.MLAgents;
using UnityEngine;

public class GoalsReached : MonoBehaviour
{
    public int goalCount;

    private List<GameObject> agents = new List<GameObject>();
    public List<int> agentNumbers = new List<int>();

    public int count = 0;
    public bool allGoalsReached = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Agent")
                agents.Add(child.gameObject);
        }
        goalCount = agents.Count();
    }

    public void AddAgentNumber(int agentNumber)
    {
        if (!agentNumbers.Contains(agentNumber))
        {
            agentNumbers.Add(agentNumber);
            count++;
        }

        if (count >= goalCount)
        {
            allGoalsReached = true;
        }
    }

    private void Update()
    {
        if (agentNumbers.Count() == 0)
        {
            allGoalsReached = false;
            count = 0;
        }
    }

    public void ResetAgentGoals()
    {
        foreach (GameObject agent in agents)
        {
            NewAgent agentProps = agent.GetComponent<NewAgent>();
            if (agentProps.reachedGoal)
            {
                agentProps.reachedGoal = false;
                agentProps.target.gameObject.SetActive(true);
                agentProps.RandomiseTargetPos();
            }
        }
    }

    public void ResetAgentBreadcrumbs()
    {
        foreach (GameObject agent in agents)
        {
            NewAgent agentProps = agent.GetComponent<NewAgent>();
            agentProps.RemoveAllBreadcrumbs();
        }
    }
}
