using UnityEngine;
using System.Collections;

public class SnailScript : MonoBehaviour {
	public float moveSpeed = 1f;
	private Rigidbody2D myBody;
	private Animator anim;

	public LayerMask playerLayer;

	private bool moveLeft;

	private bool canMove;
	private bool stunned;

	public Transform left_Collision, right_Collision, top_Collision, down_Collision;
	private Vector3 left_Collision_Pos, right_Collision_Pos;

	void Awake() {
		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		left_Collision_Pos = left_Collision.position;
		right_Collision_Pos = right_Collision.position;
	}//Awake


	// Use this for initialization
	void Start () {
		moveLeft = true;
		canMove = true;
	} //Start
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			if (moveLeft) {
				myBody.velocity = new Vector2 (-moveSpeed, myBody.velocity.y);
			} else {
				myBody.velocity = new Vector2 (moveSpeed, myBody.velocity.y);
			}
		}

		CheckCollision ();
	} // Update

	void CheckCollision(){

		RaycastHit2D leftHit = Physics2D.Raycast (left_Collision.position, Vector2.left, 0.1f, playerLayer);
		RaycastHit2D rightHit = Physics2D.Raycast (right_Collision.position, Vector2.right, 0.1f, playerLayer);

		Collider2D topHit = Physics2D.OverlapCircle (top_Collision.position, 0.2f, playerLayer);

		if (topHit != null) {
			if (topHit.gameObject.tag == "Player") {
				if (!stunned) {
					topHit.gameObject.GetComponent<Rigidbody2D> ().velocity =
						new Vector2 (topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);

					canMove = false;
					myBody.velocity = new Vector2 (0, 0);

					anim.Play ("Stunned");
					stunned = true;

					// BEETLE CODE HERE
				}
			}
		}

		//If we don't detect collision, do the if statement
		if (!Physics2D.Raycast (down_Collision.position, Vector2.down, 0.1f)) {

			ChangeDirection ();
		
		}
	}//CheckCollision

	void ChangeDirection(){
		moveLeft = !moveLeft;

		Vector3 tempScale = transform.localScale;

		if (moveLeft) {
			tempScale.x = Mathf.Abs (tempScale.x); // makes scale positive so that the snail faces to the left

			left_Collision.position = left_Collision_Pos;
			right_Collision.position = right_Collision_Pos;
		} else {
			tempScale.x = -Mathf.Abs (tempScale.x); // makes scale negative so that the snail faces to the right
			left_Collision.position =right_Collision_Pos;
			right_Collision.position = left_Collision_Pos;
		}

		transform.localScale = tempScale;

	}//ChangeDirection



}//class
