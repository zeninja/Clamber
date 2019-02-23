using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Hold hold; 
	public float timeBtwnSpawns = 1;
	float nextSpawnTime;

	public static float holdFallSpeed = 3f;
	Hold firstHold;
	Hold lastHold;

	List<Hold> holds;

	public  Vector2 startHoldPos;
	public  Vector2 nextHoldPos;

	[SerializeField]
	Vector2 lastHoldPos;

	void Start() {
		holds = new List<Hold>();
		nextHoldPos = startHoldPos;
	}

	void Update () {
		if(Time.time > nextSpawnTime) {
			SpawnHold();
			nextSpawnTime = Time.time + timeBtwnSpawns;
		}

		if(lastHold != null) {
			lastHoldPos = lastHold.transform.position;
			nextHoldPos = lastHoldPos + (Vector2)nextHoldVector;
		}
	}

	public Vector3 nextHoldVector;

	public static float distanceToNextHold = 2;
	public bool spreadHoldSpawns;

	void SpawnHold() {
		Hold h = Instantiate(hold, nextHoldPos, Quaternion.identity);
		h.fallSpeed = holdFallSpeed;

		Quaternion rotation = Quaternion.identity;
		
		if(spreadHoldSpawns) {
			rotation = Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward);
		}

		nextHoldVector = rotation * Vector2.up * distanceToNextHold;
		AddHoldToList(h);

		if (!hasSpawnedFirstHold) {
			firstHold = h;
			hasSpawnedFirstHold = true;
			SpawnPlayer(firstHold);
		}
	}

	public Hand hand;

	void SpawnPlayer(Hold startHold) {
		Hand h = Instantiate(hand, startHold.transform.position, Quaternion.identity);
		h.SetFirstHold(firstHold);
	}

	void AddHoldToList(Hold h) {
		holds.Add(h);
		lastHold = h;
	}

	bool hasSpawnedFirstHold;
}
