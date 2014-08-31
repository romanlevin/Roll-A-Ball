using UnityEngine;
using System.Collections;

public class PickupFactory : MonoBehaviour {

	public int numberOfPickups = 10;
	public float spreadRadius = 3f;
	public GameObject pickupObject;

	void DestroyStartingPickups(){
		GameObject[] startingPickups = GameObject.FindGameObjectsWithTag ("Pickup");
		
		foreach (GameObject startingPickup in startingPickups) {
			Destroy(startingPickup);
		}
	}

	Vector3[] GeneratePickupPoints(){
		Vector3[] pickupPoints = new Vector3[numberOfPickups];
		float angle;
		float angleFraction = 2f * Mathf.PI / numberOfPickups;
		for (int i = 0; i < numberOfPickups; i++) {
			angle = angleFraction * i;
			pickupPoints[i] = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin (angle)) * spreadRadius + Vector3.up;
		}
		return pickupPoints;
	}

	void GeneratePickups(){
		Vector3[] pickupPoints = GeneratePickupPoints();
		foreach (Vector3 pickupPoint in pickupPoints) {
			Instantiate(pickupObject, pickupPoint, Quaternion.identity);
		}
	}

	void Start () {
		if (numberOfPickups > 0) {
			DestroyStartingPickups ();
		}
		GeneratePickups ();
	}
}
