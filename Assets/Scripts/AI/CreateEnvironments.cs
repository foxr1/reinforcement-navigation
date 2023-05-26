using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class CreateEnvironments : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private GameObject agentPrefab;
    [SerializeField]
    private GameObject environmentPrefab;
    [SerializeField, Range(1, 100), Tooltip("Enter a square number")]
    private int noOfEnvironments;
    [SerializeField, Range(1, 10)]
    private int noOfAgents;

    private GameObject plane;
    private Bounds bounds;

    // Start is called before the first frame update
    void Start()
    {
        plane = environmentPrefab.transform.GetChild(1).gameObject;
        bounds = plane.GetComponent<MeshRenderer>().bounds;

        SetupEnvironments();

        if (noOfEnvironments > 1)
        {
            MoveCamera();
        }
    }

    public void SetupEnvironments()
    {
        float rows = Mathf.Sqrt(noOfEnvironments);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject environment = GameObject.Instantiate(environmentPrefab);
                environment.transform.position = new Vector3(i * (bounds.size.x * 1.5f), 0, j * (bounds.size.z * 1.5f));

                if (noOfAgents > 1)
                {
                    SetupAgents(environment);
                } 
            }
        }
    }

    private void MoveCamera()
    {
        float position = ((bounds.size.x * Mathf.Sqrt(noOfEnvironments)) + ((bounds.size.x / 2) * (Mathf.Sqrt(noOfEnvironments) - 1))) / 2 - (bounds.size.x / 2);

        mainCamera.transform.position = new Vector3(position, position * 2.5f, position);
    }

    private void SetupAgents(GameObject environment)
    {
        for (int i = 0; i < noOfAgents - 1; i++)
        {
            RayPerceptionSensorComponent3D[] rayPerceptions = agentPrefab.GetComponents<RayPerceptionSensorComponent3D>();
            agentPrefab.GetComponent<NewAgent>().grid = environment.GetComponentInChildren<GridWithParams>();
            agentPrefab.GetComponent<NewAgent>().shouldSpawnOwnTarget = true;
            //agentPrefab.GetComponent<NewAgent>().isRandomStartPosition = true;
            //agentPrefab.GetComponent<NewAgent>().agentNo = i.ToString();
            agentPrefab.GetComponent<NewAgent>().floorMeshRenderer = environment.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>();

            Transform randomStartPos = RandomAgentStartPos(i);

            GameObject agent = Instantiate(agentPrefab);
            agent.name = $"Agent_{i}";
            agent.transform.parent = environment.transform;
            agent.tag = "Agent";
            agent.GetComponent<NewAgent>().startPos = randomStartPos.localPosition;
            agent.GetComponent<NewAgent>().startRot = randomStartPos.localEulerAngles;
        }
    }

    private Transform RandomAgentStartPos(int agentNo)
    {
        Transform newPos = new GameObject().transform;

        Random r = new Random(agentNo);

        float randomXRange = r.Next(-190, 190);
        float randomZRange = r.Next(-190, 190);
        float[] randomXZs = new float[2];
        randomXZs[0] = randomXRange;
        randomXZs[1] = randomZRange;

        int randomIndex = r.Next(0, randomXZs.Length);
        float result = randomXZs[randomIndex];

        if (result == randomXRange)
        {
            double probability = r.NextDouble() * 1.0;

            if (probability > 0.5)
            {
                newPos.localPosition = new Vector3(randomXRange, 2.5f, 195);
                newPos.localRotation = Quaternion.Euler(new Vector3(0, -180, 0));
            }
            else
            {
                newPos.localPosition = new Vector3(randomXRange, 2.5f, -195);
                newPos.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
        else if (result == randomZRange)
        {
            double probability = r.NextDouble() * 1.0;

            if (probability > 0.5)
            {
                newPos.localPosition = new Vector3(195, 2.5f, randomZRange);
                newPos.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
            }
            else
            {
                newPos.localPosition = new Vector3(-195, 2.5f, randomZRange);
                newPos.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
        }

        return newPos;
    }
}
