using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.UI;
using Unity.MLAgents.Policies;
using System;
using Random = UnityEngine.Random;

public class NewAgent : Agent
{
    Rigidbody rbd;
    RayPerceptionSensorComponent3D[] rayPerceptions;

    [HideInInspector]
    public Vector3 startPos;
    [HideInInspector]
    public Vector3 startRot;

    // For moving between successive goals
    private Vector3 goalStartPos;
    private Vector3 goalStartRot;
    [Header("Successive Goals")]
    public bool reachedGoal = false;
    [SerializeField] private bool waitForOtherAgents = false; // Wait for other agents to reach their destination before moving
    [SerializeField] private GoalsReached goalsReached;
    private List<Transform> listOfOtherAgents = new List<Transform>();

    [Header("Environment")]
    public GameObject target; // Target in parent environment
    [SerializeField] public GameObject targetPrefab;
    [SerializeField] public bool shouldSpawnOwnTarget = false; // Used for when there is multiple agents
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private Material agentMaterial;
    [SerializeField] public MeshRenderer floorMeshRenderer;
    [SerializeField] public GridWithParams grid;
    [SerializeField] public Transform plane;

    [Header("Agent Options")]
    [SerializeField] private bool isRandomGrid = false; // Randomise entire city with roadblocks
    [SerializeField] public bool isRandomStartPosition = false; // Should the agent start in a random position at the start of an episode
    [SerializeField] private bool resetAgentOnGoal = true; // Should the agent reset the episode when reaching the goal
    [SerializeField, Range(0, 100)] public int agentNo = 0;
    [SerializeField] public GameObject agentNoText;

    [Header("Breadcrumb Options")]
    [SerializeField] public GameObject breadcrumbPrefab;
    [SerializeField] public bool leaveBreadcrumbs = false; // Should the agent leave 'breadcrumbs' behind it when moving towards goal
    [SerializeField] private float breadcrumbPenalty = -0.005f;

    [Header("Behaviour Options")]
    [SerializeField] private bool addOtherAgentsObservations = false;
    [SerializeField] private bool useDistanceToScalePadding = false;

    [Header("Roadblock Options")]
    [SerializeField] private bool isRandomRoadblocks; // Only randomise roadblocks instead of entire city
    [SerializeField] private bool isTimedRoadblocks = false; // Randomise the roadblocks on a timer instead of at end of episode
    [SerializeField, Range(0, 60)] private float seconds = 30; // Timer for roadblocks
    private Material originalMaterial;

    [Header("Electric")]
    [SerializeField] private bool isElectric = false;
    [SerializeField] private float batteryLevel = 100;
    [SerializeField] private Transform chargingStations;
    [SerializeField] public GameObject batteryLevelText;

    [Header("GANN Comparison")]
    [SerializeField] private bool shouldSave = false;
    [SerializeField] private RLManager RLManager;
    private Vector3 lastPosition;

    private int randomSeedNo = 0;

    // For time & comparison demonstration
    private float timerSeconds = 0;
    private bool activeTimer = false;

    // Variables for comparison scene
    private float episodeTimer = 0;
    private float totalTime = 0;
    private int episodesCompleted = 0;
    [Header("Timing/Comparison Demonstration")]
    [Space(10)]
    [SerializeField] private Text lastEpisodeCompletedText;
    [SerializeField] private Text meanTimeText;
    [SerializeField] private Text episodesCompletedText;

    public override void Initialize()
    {
        rbd = GetComponent<Rigidbody>();
        rayPerceptions = GetComponents<RayPerceptionSensorComponent3D>();
        startPos = transform.localPosition;
        startRot = transform.localEulerAngles;
        lastPosition = startPos;

        originalMaterial = floorMeshRenderer.material;
        goalsReached = transform.parent.GetComponent<GoalsReached>();

        agentNoText.GetComponent<UnityEngine.UI.Text>().text = agentNo.ToString();

        foreach (Transform child in transform.parent)
        {
            if (child.tag == "Agent" && child != this.transform)
            {
                listOfOtherAgents.Add(child);
            }
        }
    }

    public override void OnEpisodeBegin()
    {
        if (isRandomStartPosition)
        {
            Transform newPos = RandomAgentStartPos();
            transform.localPosition = newPos.localPosition;
            transform.localEulerAngles = newPos.localEulerAngles;
        }
        else
        {
            // If it has reached the goal, set the position to the position it reached at the goal, otherwise reset back to beginning
            if (reachedGoal)
            {
                transform.localPosition = goalStartPos;
                transform.localEulerAngles = goalStartRot;
            }
            else
            {
                transform.localPosition = startPos;
                transform.localEulerAngles = startRot;
            }
        }

        RandomiseTargetPos();

        rbd.velocity = Vector3.zero;

        if (isRandomGrid)
        {
            // Only reset the grid if it has not reached the last goal
            if (!reachedGoal)
            {
                BuildRandomGrid();
            }
        }
        if (isRandomRoadblocks)
        {
            grid.ClearRoadblocks();
            grid.BuildRoadblocks();
        }
        if (isTimedRoadblocks)
        {
            timerSeconds = seconds;
            activeTimer = true;
        }
    }

    public void SetAgentPosition(Transform newPos)
    {
        transform.localPosition = newPos.localPosition;
        transform.localEulerAngles = newPos.localEulerAngles;
    }

    // Code to determine a suitable place for the agent to start around the edge of the city
    public static Transform RandomAgentStartPos()
    {
        Transform newPos = new GameObject().transform;

        float randomXRange = Random.Range(-190, 190);
        float randomZRange = Random.Range(-190, 190);
        float[] randomXZs = new float[2];
        randomXZs[0] = randomXRange;
        randomXZs[1] = randomZRange;

        int randomIndex = Random.Range(0, randomXZs.Length);
        float result = randomXZs[randomIndex];

        if (result == randomXRange)
        {
            float probability = Random.Range(0.0f, 1.0f);

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
            float probability = Random.Range(0.0f, 1.0f);

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

    // Find suitable location for target to spawn
    public void RandomiseTargetPos()
    {
        int gridHeight = grid.parameters.height;
        int gridWidth = grid.parameters.width;
        Vector3 gridMargin = grid.parameters.marginBetweenShapes;

        // Get random building in environment
        int randomX = Random.Range(-Mathf.FloorToInt(gridHeight / 2), gridHeight - Mathf.FloorToInt(gridHeight / 2));
        int randomZ = Random.Range(-Mathf.FloorToInt(gridHeight / 2), gridWidth - Mathf.FloorToInt(gridHeight / 2));

        if (shouldSpawnOwnTarget)
        {
            Destroy(target);
            target = GameObject.Instantiate(targetPrefab);
            target.transform.GetChild(0).GetComponentInChildren<Text>().text = agentNo.ToString();
            target.transform.parent = transform.parent;
            //target.tag = "Goal" + agentNo;

            randomX = Random.Range(-Mathf.FloorToInt(gridHeight / 2), gridHeight - Mathf.FloorToInt(gridHeight / 2));
            randomZ = Random.Range(-Mathf.FloorToInt(gridHeight / 2), gridWidth - Mathf.FloorToInt(gridHeight / 2));

            shouldSpawnOwnTarget = false;
        }

        // Position target in centre of a building so that it is always reachable despite roadblocks
        target.transform.localPosition = new Vector3(randomX * gridMargin.x * 2 - gridMargin.x, target.transform.localScale.y / 2, randomZ * gridMargin.z * 2 - gridMargin.z);
    }

    public void BuildRandomGrid()
    {
        if (randomSeedNo > 99)
        {
            randomSeedNo = 0;
        }
        else
        {
            randomSeedNo++;
        }
        grid.parameters.randomSeed = randomSeedNo;

        grid.BuildGrid();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        BehaviorParameters behaviorParameters = GetComponent<BehaviorParameters>();
        int observationSize = behaviorParameters.BrainParameters.VectorObservationSize;

        sensor.AddObservation(transform.forward);
        //sensor.AddObservation(transform.right);

        sensor.AddObservation(rbd.velocity.x);
        sensor.AddObservation(rbd.velocity.z);

        if (!isElectric || (isElectric && batteryLevel >= 20))
        {
            sensor.AddObservation(Vector3.Distance(transform.localPosition, target.transform.localPosition)); // distance to target
            sensor.AddObservation((target.transform.position - transform.position).normalized); // direction to target
        }

        //direction and distance to other agents 
        if (addOtherAgentsObservations)
        {
            if (listOfOtherAgents.Count > 0)
            {
                foreach (Transform agent in listOfOtherAgents)
                {
                    sensor.AddObservation(Vector3.Distance(transform.localPosition, agent.localPosition));
                    sensor.AddObservation((agent.position - transform.position).normalized);
                }
            }
        }

        if (useDistanceToScalePadding)
        {
            if ((observationSize - 9 - listOfOtherAgents.Count * 4) > 0)
            {
                int nullObs = observationSize - 9 - (listOfOtherAgents.Count * 4);
                for (int i = 0; i < nullObs / 4; i++)
                {
                    sensor.AddObservation(Vector3.Distance(plane.localScale, new Vector3(0, 0, 0)));
                    sensor.AddObservation((plane.localScale - transform.position).normalized);
                }
            }
        }

        if (isElectric)
        {
            sensor.AddObservation(batteryLevel);
            if (batteryLevel < 20)
            {
                sensor.AddObservation(Vector3.Distance(transform.localPosition, chargingStations.localPosition)); // distance to charging station
                sensor.AddObservation((chargingStations.localPosition - transform.localPosition).normalized); // direction to charging station
            }
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (waitForOtherAgents)
        {
            if (goalsReached.allGoalsReached && goalsReached.agentNumbers.Contains(agentNo) && waitForOtherAgents)
            {
                goalsReached.agentNumbers.Remove(agentNo);
                reachedGoal = false;
                target.gameObject.SetActive(true);
                RandomiseTargetPos();
            }

            if (!reachedGoal && !goalsReached.allGoalsReached)
            {
                AddReward(-1f / MaxStep);
                MoveAgent(actions.DiscreteActions);
            }
        } 
        else
        {
            AddReward(-1f / MaxStep);
            MoveAgent(actions.DiscreteActions);
        }

        //if (isElectric)
        //{
        //    if (!draining && (rbd.velocity.x > 5 || rbd.velocity.z > 5))
        //    {
        //        StartCoroutine(DrainBattery());
        //    }

        //    if (batteryLevel < 20)
        //    {
        //        float distanceToStation = Vector3.Distance(transform.localPosition, chargingStations.localPosition); //400 is max size of city
        //        AddReward(-distanceToStation / MaxStep);
        //    }
        //    if (batteryLevel <= 0)
        //    {
        //        SetReward(-2f);
        //        batteryLevel = 100;
        //        EndEpisode();
        //        StartCoroutine(ChangeFloorMaterial(loseMaterial));
        //    }
        //}
    }

    //private bool draining = false;
    //private IEnumerator DrainBattery()
    //{
    //    draining = true;
    //    yield return new WaitForSeconds(1f);
    //    batteryLevel -= 1f;
    //    batteryLevelText.GetComponent<UnityEngine.UI.Text>().text = $"{batteryLevel.ToString()}%";
    //    draining = false;
    //}

    public void RemoveAllBreadcrumbs()
    {
        foreach (Transform child in transform.parent)
        {
            if (child.tag == "Breadcrumb" && child.name == $"Breadcrumb{agentNo}")
            {
                Destroy(child.gameObject); 
            }
        }
    }

    private void MoveAgent(ActionSegment<int> act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action = act[0];
        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 1f;
                break;
            case 4:
                rotateDir = transform.up * -1f;
                break;
        }
        transform.Rotate(rotateDir, Time.deltaTime * 200f);
        rbd.AddForce(dirToGo * 2f, ForceMode.VelocityChange);

        //switch (action) 
        //{
        //    case 1:
        //        dirToGo = transform.forward * 1f;
        //        break;
        //    case 2:
        //        dirToGo = transform.forward * -1f;
        //        break;
        //    case 3:
        //        dirToGo = transform.right * 1f;
        //        break;
        //    case 4:
        //        dirToGo = transform.right * -1f;
        //        break;
        //}
        //rbd.AddForce(dirToGo * 2f, ForceMode.VelocityChange);
        //}

        if (leaveBreadcrumbs)
        {
            if (placingBreadcrumb == false)
            {
                StartCoroutine(PlaceBreadcrumb());
            }
        }
    }

    private bool placingBreadcrumb = false;
    IEnumerator PlaceBreadcrumb()
    {
        placingBreadcrumb = true;
        GameObject breadcrumb = Instantiate(breadcrumbPrefab);
        breadcrumb.transform.parent = transform.parent;
        breadcrumb.name = $"Breadcrumb{agentNo}";
        breadcrumb.transform.localPosition = transform.localPosition;
        yield return new WaitForSeconds(0.5f);
        placingBreadcrumb = false;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[0] = 3;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[0] = 4;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            // Check if agent is reaching their goal not another agent's
            if (other.gameObject != target.gameObject)
            {
                StartCoroutine(ChangeFloorMaterial(loseMaterial));
                reachedGoal = false;

                if (waitForOtherAgents)
                {
                    goalsReached.ResetAgentGoals();
                }

                //if (isElectric)
                //{
                //    batteryLevel = 100;
                //}

                SetReward(-1f);
                EndEpisode();
            }
            else
            {
                // If the agent is in the comaprison scene then update the scene with variables
                if (lastEpisodeCompletedText != null)
                {
                    lastEpisodeCompletedText.text = episodeTimer.ToString("0.00");
                    totalTime += episodeTimer;
                    episodesCompleted++;
                    SetText();
                }

                StartCoroutine(ChangeFloorMaterial(winMaterial));

                if (!resetAgentOnGoal)
                {
                    goalStartPos = transform.localPosition;
                    goalStartRot = transform.localEulerAngles;
                    reachedGoal = true;
                    if (waitForOtherAgents)
                    {
                        target.gameObject.SetActive(false);
                        goalsReached.AddAgentNumber(agentNo);
                    }
                }

                if (leaveBreadcrumbs)
                {
                    reachedGoal = true;
                    goalsReached.ResetAgentBreadcrumbs();
                }

                if (shouldSave)
                {
                    RLManager.numberOfGoals++;
                }
                SetReward(+2f);
                EndEpisode();
            }
        }
        if (other.CompareTag("Wall"))
        {
            StartCoroutine(ChangeFloorMaterial(loseMaterial));
            reachedGoal = false;
            CheckConditions();
            if (shouldSave)
            {
                RLManager.numberOfCollisions++;
            }

            SetReward(-1f);
            EndEpisode();
        }
        if (other.CompareTag("Agent"))
        {
            StartCoroutine(ChangeFloorMaterial(agentMaterial));
            reachedGoal = false;
            CheckConditions();

            SetReward(-1f);
            EndEpisode();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Breadcrumb"))
        {
            // If breadcrumb has been active for more than 1 second, then penalise for colliding with it
            if (other.GetComponent<Breadcrumb>().newBreadcrumb == false)
            {
                if (shouldSave)
                {
                    RLManager.numberOfCollisions++;
                }
                AddReward(breadcrumbPenalty);
            }
        }
        if (other.CompareTag("Charging"))
        {
            if (batteryLevel < 20)
            {
                batteryLevel = 100;
                SetReward(0.5f);
                StartCoroutine(ChangeFloorMaterial(winMaterial));
            }
        }
    }

    private void CheckConditions()
    {
        if (waitForOtherAgents)
        {
            goalsReached.ResetAgentGoals();
        }

        if (leaveBreadcrumbs)
        {
            RemoveAllBreadcrumbs();
        }

        //if (isElectric)
        //{
        //    batteryLevel = 100;
        //}
    }

    // Show values on scene for comparison scene
    private void SetText()
    {
        meanTimeText.text = (totalTime / episodesCompleted).ToString("0.00");
        episodesCompletedText.text = episodesCompleted.ToString();

        WriteToCSVFile.WriteToCSV.addRecord(episodesCompleted, episodeTimer, totalTime, (totalTime / episodesCompleted), "rl_results.csv");
    }

    private IEnumerator ChangeFloorMaterial(Material material)
    {
        floorMeshRenderer.material = material;

        // Episode timer was resetting to zero before being added to the scene and so small delay is added here to prevent this
        yield return new WaitForSeconds(0.1f); 
        episodeTimer = 0.1f;

        yield return new WaitForSeconds(1.5f);

        floorMeshRenderer.material = originalMaterial;
        
    }

    private void Update()
    {
        if (activeTimer)
        {
            timerSeconds -= Time.deltaTime;

            if (timerSeconds <= 0.0f)
            {
                activeTimer = false;
                TimedRoadblocks();
            }
        }
    }

    private void FixedUpdate()
    {
        if (shouldSave)
        {
            if (rbd.velocity.x > 0 || rbd.velocity.z > 0)
            {
                RLManager.totalDistanceCovered += Vector3.Distance(transform.position, lastPosition);
                lastPosition = transform.position;
            }
        }
        
        episodeTimer += Time.fixedDeltaTime;
    }

    private void TimedRoadblocks()
    {
        grid.ClearRoadblocks();
        grid.BuildRoadblocks();
        timerSeconds = seconds;
        activeTimer = true;
    }

    public void ToggleTimer(bool active)
    {
        activeTimer = active;
    }

    public void SetTimerSeconds(float newSeconds)
    {
        seconds = newSeconds;
    }

    public void SetTimerText(Text text)
    {
        text.text = seconds.ToString();
    }

    public void ForecEndEpisode()
    {
        EndEpisode();
    }
}
