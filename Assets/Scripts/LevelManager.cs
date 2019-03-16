using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public int numHolds = 15;
    public Hold holdPrefab;
    public Vector2 startHoldPos = Vector2.zero;
    Vector2 nextHoldPos;
    public bool spreadHoldSpawns;
    public float LONG_DIST = 2;
    public float MED_DIST = 1;
    public float SHORT_DIST = .5f;
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

            nextHoldVector = GetEvenHeightVector(HOLD_SPREAD, LONG_DIST);
            nextHoldPos += nextHoldVector;

            levelPath.Add(nextHoldPos);

            yield return new WaitForSeconds(timeBetweenHoldSpawns);
        }

        if (drawLevelLine)
        {
            DrawLevelLine();
        }

        // yield return StartCoroutine(SpawnMedHolds(largeHolds, 180, MED_DIST, .5f));	// med holds
        yield return StartCoroutine(SpawnSmallHolds(largeHolds, 180, MED_DIST, .15f));  // med holds
                                                                                        // yield return StartCoroutine(SpawnSmallHolds(largeHolds, 180, MED_DIST, .15f));	// med holds
                                                                                        // yield return StartCoroutine(SpawnSmallHolds(largeHolds, 180, LONG_DIST, .15f));	// med holds


    }

    List<Hold> largeHolds = new List<Hold>();
    List<Hold> medHolds = new List<Hold>();
    List<Hold> smallHolds = new List<Hold>();



    IEnumerator SpawnMedHolds(List<Hold> holds, float spread, float distance, float scale)
    {

        for (int i = 0; i < holds.Count; i++)
        {
            Hold startHold = holds[i];
            Vector2 nextHoldVector = GetRandomNextHoldVector(spread, distance);
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

            if (drawOffsetLines)
            {
                newHold.AddLine(startHold);
            }
            smallHolds.Add(newHold);

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
}
