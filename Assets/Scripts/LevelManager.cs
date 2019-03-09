using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public int numHolds = 15;
	public Hold holdPrefab;
	public Vector2 startHoldPos = Vector2.zero;
	Vector2 nextHoldPos;
	public bool spreadHoldSpawns;
	public float distanceToNextHold = 2;
	public static float HOLD_SPREAD = 45;
	public float timeBetweenHoldSpawns = .01f;
	
	void Start() {
		nextHoldPos = startHoldPos;
		levelPath = new List<Vector3>();
		levelPath.Add(startHoldPos);
		StartCoroutine(SpawnHolds());
	}

	IEnumerator SpawnHolds() {
		Vector2 nextHoldVector = Vector2.up;

		for(int i = 0; i < numHolds; i++) {
			
			Hold newHold = Instantiate(holdPrefab, nextHoldPos, Quaternion.identity);
			newHold.transform.parent = transform;

			nextHoldVector = GetNextHoldVector();
			nextHoldPos += nextHoldVector;
			
			levelPath.Add(nextHoldPos);

			yield return new WaitForSeconds(timeBetweenHoldSpawns);
		}
		DrawLevelLine();
	}

	void DrawLevelLine() {
		GameObject levelLine = new GameObject();
		LineRenderer l = levelLine.AddComponent<LineRenderer>();
		l.positionCount = levelPath.Count;
		l.SetPositions(levelPath.ToArray());
		l.startWidth = pathWidth;
		l.endWidth   = pathWidth;
	}

	Vector2 GetNextHoldVector() {
		Quaternion rotation = Quaternion.identity;

		if (spreadHoldSpawns) {
			rotation = Quaternion.AngleAxis(Random.Range(-HOLD_SPREAD, HOLD_SPREAD), Vector3.forward);
		}

		Vector2 nextHoldVector = new Vector2((rotation * Vector2.up * distanceToNextHold).x, distanceToNextHold);
		return  nextHoldVector;
	}
	
	List<Vector3> levelPath;
	public float pathWidth = .0125f;

	LineRenderer levelLine;
}
