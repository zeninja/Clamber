using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour {

	public float fallSpeed;

	public Color holdColor;
	public Color flashColor;
	

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

		GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, holdColor, Time.time - grabTime);

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("KillTrigger")) {
			// Destroy(gameObject);
			Debug.Log("Game over");
			Destroy(gameObject);
			// gameObject.SetActive(false);
		}
	}

	float grabTime;

	public void GetGrabbed() {
		grabTime = Time.time;
		GetComponent<SpriteRenderer>().color = flashColor;
	}
}
