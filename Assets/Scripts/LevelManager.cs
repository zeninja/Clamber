using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public int numHolds = 15;
	public Hold holdPrefab;
	public Vector2 startHoldPos = Vector2.zero;
	Vector2 nextHoldPos;
	
	void Start() {
		nextHoldPos = startHoldPos;
		StartCoroutine(SpawnHolds());
	}

	IEnumerator SpawnHolds() {
		Vector2 nextHoldVector = Vector2.up;

		for(int i = 0; i < numHolds; i++) {
			Hold newHold = Instantiate(holdPrefab, nextHoldPos, Quaternion.identity);
			nextHoldVector = GetNextHoldVector();
			nextHoldPos += nextHoldVector;
			yield return null;

		}
	}

	public bool spreadHoldSpawns;
	public float distanceToNextHold = 2;
  
	public static float HOLD_SPREAD = 45;

	Vector2 GetNextHoldVector() {
		Quaternion rotation = Quaternion.identity;

		if (spreadHoldSpawns) {
			rotation = Quaternion.AngleAxis(Random.Range(-HOLD_SPREAD, HOLD_SPREAD), Vector3.forward);
		}

		Vector2 nextHoldVector = new Vector2((rotation * Vector2.up * distanceToNextHold).x, distanceToNextHold);
		return  nextHoldVector;
	}
}
