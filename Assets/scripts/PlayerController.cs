using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float forceFactor;
	private int count;
	private Vector3 targetScale;
	private Vector3 scaleVelocity = Vector3.zero;
	public GUIText countText;
	public GUIText winText;

	void Start() {
		count = 0;
		UpdateCount ();
		winText.text = "";
		targetScale = transform.localScale;
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		AdjustScale ();
		
		Vector3 force = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rigidbody.AddForce (force * forceFactor * Time.deltaTime);
	}

	private void UpdateCount() {
		countText.text = "Count: " + count.ToString ();
	}

	private void PickUp(GameObject pickup) {
		pickup.SetActive(false);
		count += 1;
		targetScale += Vector3.one * 0.2f;
		UpdateCount ();
		if (GameObject.FindGameObjectsWithTag ("Pickup").Length == 0) {
			winText.text = "A Winner is You!";
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Pickup") {
			PickUp(other.gameObject);
		}
	}

	private void AdjustScale() {
		if (targetScale != transform.localScale) {
			transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref scaleVelocity, 0.3f);
		}
	}
}
