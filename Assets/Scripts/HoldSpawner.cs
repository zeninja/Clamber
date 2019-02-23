using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldSpawner : MonoBehaviour {


	public int numHolds = 15;
	public Hold holdPrefab;

	public Vector2 startHoldPos = Vector2.zero;

	Vector2 lastHoldPos;
	Vector2 nextHoldPos;

	public float holdDistance;
	public bool keepHoldsOnScreen = true;
	
	void Start() {
		nextHoldPos = startHoldPos;
		StartCoroutine(SpawnHolds());

	}


	IEnumerator SpawnHolds() {
		Vector2 nextHoldVector = Vector2.up;

		for(int i = 0; i < numHolds; i++) {
			Hold newHold = Instantiate(holdPrefab, nextHoldPos, Quaternion.identity);

			nextHoldVector = GetNextHoldVector();

			if(keepHoldsOnScreen) {
				while (Mathf.Abs(nextHoldPos.x + nextHoldVector.x) > ScreenInfo.w) {
					nextHoldVector = GetNextHoldVector();
					yield return null;
				}
			}

			nextHoldPos += nextHoldVector;
			// yield return new WaitForSeconds(.1f);
			yield return null;

		}
	}

	public bool spreadHoldSpawns;
	public float distanceToNextHold = 2;

	Vector2 GetNextHoldVector() {
		Quaternion rotation = Quaternion.identity;

		if(spreadHoldSpawns) {
			rotation = Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward);
		}

		Vector2 nextHoldVector = rotation * Vector2.up * distanceToNextHold;
		return  nextHoldVector;
	}
}
