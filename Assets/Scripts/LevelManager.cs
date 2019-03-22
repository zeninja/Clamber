using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public int numHolds = 15;
    public Hold holdPrefab;
    public Vector2 startHoldPos = Vector2.zero;
    Vector2 nextHoldPos;
    // public bool spreadHoldSpawns;
    public Extensions.Property HOLD_SPAWN_DIST;
    public float LARGE_HOLD_DIST = 2;
    public float SMALL_HOLD_DIST = 1f;
    public float SMALL_HOLD_SPREAD = 90;
    public static float HOLD_SPREAD = 45;
    public float timeBetweenHoldSpawns = .01f;

    public bool drawLevelLine;
	public bool drawOffsetLines;

    void Start()
    {
        nextHoldPos = startHoldPos;
        levelPath = new List<Vector3>();
        levelPath.Add(startHoldPos);
        StartCoroutine(SpawnHolds());
    }

    IEnumerator SpawnHolds()
    {
        Vector2 nextHoldVector = Vector2.up;

        for (int i = 0; i < numHolds; i++)
        {

            Hold newHold = Instantiate(holdPrefab, nextHoldPos, Quaternion.identity);
            newHold.transform.parent = transform;
            largeHolds.Add(newHold);

            // nextHoldVector = GetEvenHeightVector(HOLD_SPREAD, LARGE_HOLD_DIST);
            nextHoldVector = GetRandomNextHoldVector(HOLD_SPREAD, HOLD_SPAWN_DIST);
            nextHoldPos += nextHoldVector;

            levelPath.Add(nextHoldPos);

            yield return new WaitForSeconds(timeBetweenHoldSpawns);
        }

        if (drawLevelLine)
        {
            DrawLevelLine();
        }

        yield return StartCoroutine(SpawnSmallHolds(largeHolds, SMALL_HOLD_SPREAD, SMALL_HOLD_DIST, .15f));  // small holds

    }

    List<Hold> largeHolds = new List<Hold>();
    List<Hold> medHolds = new List<Hold>();
    List<Hold> smallHolds = new List<Hold>();


    IEnumerator SpawnMedHolds(List<Hold> holds, float spread, float distance, float scale)
    {

        for (int i = 0; i < holds.Count; i++)
        {
            Hold startHold = holds[i];
            Vector2 nextHoldVector = GetRandomNextHoldVector(spread, HOLD_SPAWN_DIST);
            nextHoldPos = (Vector2)startHold.transform.position + nextHoldVector;

            Hold newHold = Instantiate(holdPrefab, nextHoldPos, Quaternion.identity);
            newHold.transform.localScale = Vector3.one * scale;

            newHold.AddLine(startHold);
            medHolds.Add(newHold);

            yield return new WaitForSeconds(timeBetweenHoldSpawns);
        }
    }

    IEnumerator SpawnSmallHolds(List<Hold> holds, float spread, float distance, float scale)
    {

        for (int i = 0; i < holds.Count; i++)
        {
            Hold startHold = holds[i];
            Vector2 nextHoldVector = GetRandomNextHoldVector(spread, distance);
            nextHoldPos = (Vector2)startHold.transform.position + nextHoldVector;

            Hold newHold = Instantiate(holdPrefab, nextHoldPos, Quaternion.identity);
            newHold.transform.localScale = Vector3.one * scale;

            newHold.transform.parent = startHold.transform;
            
            smallHolds.Add(newHold);

            if (drawOffsetLines)
            {
                newHold.AddLine(startHold, true);
            }

            yield return new WaitForSeconds(timeBetweenHoldSpawns);
        }
    }

    Vector2 GetEvenHeightVector(float spread, float dist)
    {
        Quaternion rotation = Quaternion.identity;
        rotation = Quaternion.AngleAxis(Random.Range(-spread, spread), Vector3.forward);

        Vector2 nextHoldVector = new Vector2((rotation * Vector2.up * dist).x, dist);
        return nextHoldVector;
    }

     Vector2 GetRandomNextHoldVector(float spread, float dist)
    {
        Quaternion rotation = Quaternion.identity;
        rotation = Quaternion.AngleAxis(Random.Range(-spread, spread), Vector3.forward);

        Vector2 nextHoldVector = rotation * Vector2.up * dist;
        return nextHoldVector;
    }

    List<float> distsBtwnHolds = new List<float>();

    Vector2 GetRandomNextHoldVector(float spread, Extensions.Property distRange)
    {
        Quaternion rotation = Quaternion.identity;
        rotation = Quaternion.AngleAxis(Random.Range(-spread, spread), Vector3.forward);

        Vector2 nextHoldVector = rotation * Vector2.up * Random.Range(distRange.start, distRange.end);
        return nextHoldVector;
    }


    LineRenderer line;
    List<Vector3> levelPath;
    public float levelPathWidth = .0125f;


    void DrawLevelLine()
    {
        GameObject levelLine = new GameObject("Level Line");
        line = levelLine.AddComponent<LineRenderer>();
        line.positionCount = levelPath.Count;
        line.SetPositions(levelPath.ToArray());
        line.startWidth = levelPathWidth;
        line.endWidth = levelPathWidth;
    }

    void Update() {
        UpdateHolds();
    }

    void UpdateHolds() {
        for(int i = 0; i < levelPath.Count; i++) {
            levelPath[i] = new Vector3(levelPath[i].x, (float)i * LARGE_HOLD_DIST + startHoldPos.y);
        }

        for(int i = 0; i < largeHolds.Count; i++) {
            largeHolds[i].transform.position = levelPath[i];
        }
    }
}
