using Grpc.Core;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RLManager : MonoBehaviour
{
    public int maxStepCount = 3000000;
    public int currentStepCount = 0;
    public int resetStepCounter = 0;
    public int resetStepCount = 25000;
    [SerializeField] private CellManager cellManager;

    [Header("Save")]
    [SerializeField, Range(1, 20)] private int timeScale = 20; 
    [SerializeField] private string testName;
    [SerializeField] private GameObject cells;
    [SerializeField] public float totalDistanceCovered = 0f;
    [SerializeField] public float numberOfCollisions = 0f;
    public int numberOfGoals = 0; // Number of goals reached
    public float timeSinceStart = 0f;
    [SerializeField] private bool resetOnGridCompletion = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timeScale;
    }

    private void Update()
    {
        timeSinceStart = Time.realtimeSinceStartup;
    }

    private void FixedUpdate()
    {
        currentStepCount++;

        if (!resetOnGridCompletion)
        {
            if (resetStepCount > 0)
            {
                resetStepCounter++;

                if (resetStepCounter > resetStepCount)
                {
                    resetStepCounter = 0;
                    //CreateAgents();
                }
            }
        }

        if (resetOnGridCompletion && cellManager.numberOfCellsVisited >= 100 || currentStepCount >= maxStepCount)
        {
            SaveRun($"Assets/Tests/{testName}.txt"); 
        }
    }

    public void SaveRun(string path)
    {
        int numberOfCellsVisitedMultipleTimes = 0;
        int maxNumberOfTimesCellWasVisited = 0;
        int numberOfCellsVisited = 0;
        foreach (Cell cell in cells.GetComponentsInChildren<Cell>())
        {
            if (cell.hasVisited && cell.timesVisited > 1)
            {
                numberOfCellsVisitedMultipleTimes++;
            }

            if (cell.timesVisited > maxNumberOfTimesCellWasVisited)
            {
                maxNumberOfTimesCellWasVisited = cell.timesVisited;
            }

            if (cell.hasVisited)
            {
                numberOfCellsVisited++;
            }
        }

        File.Create(path).Close();
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine($"Distance covered: {totalDistanceCovered}");
        writer.WriteLine($"No. of collisions: {numberOfCollisions}");
        writer.WriteLine($"Cells visited: {numberOfCellsVisited}");
        writer.WriteLine($"Cells visited multiple times: {numberOfCellsVisitedMultipleTimes}");
        writer.WriteLine($"Max times one cell was visited: {maxNumberOfTimesCellWasVisited}");
        writer.WriteLine($"No. of goals reached in total: {numberOfGoals}");
        writer.WriteLine($"Time elapsed: {timeSinceStart}");
        writer.Close();
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
