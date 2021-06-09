using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public float speed = 5f;

	private Rigidbody2D myBody;
	private Animator anim;

	public Transform groundCheckPosition;
	public LayerMask groundLayer;

	private bool isGrounded;
	private bool jumped;
	private float jumpPower = 5f;


	void Awake() {
		//print ("in awake");
		//Get us the component that is attached to a game object
		//who carries this script (PlayerMovement), which is the player
		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

	}
		
	// Use this for initialization
	void Start () {
		//print ("in start");
		anim = GetComponent<Animator>();
		myBody = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		//print ("in update");
		CheckIfGrounded();
		PlayerJump ();

		/*
		//Raycast will draw a ray at groundCheckPosition.position, in Vector2.down direction
		//and have a length of 0.5f
		if (Physics2D.Raycast (groundCheckPosition.position, Vector2.down, groundLayer)) {
			print ("Collided with ground");	
		}
		*/
	}

	//good for physics and stuff
	void FixedUpdate(){
		PlayerWalk();
	}


	void PlayerWalk(){
		float hort = Input.GetAxisRaw ("Horizontal");
		//if h>0, move right (speed positive), if h<0 move left(speed negative)

		//print ("Value is " + hort);

		if (hort > 0) {
			myBody.velocity = new Vector2 (speed, myBody.velocity.y);
			ChangeDirection (1);

		} else if (hort < 0) {
			myBody.velocity = new Vector2 (-speed, myBody.velocity.y);
			ChangeDirection (-1);

		} else {
			myBody.velocity = new Vector2 (0f, myBody.velocity.y); //this makes sure the player stops immediately
		}
			
		anim.SetInteger ("Speed", Mathf.Abs((int)myBody.velocity.x));

	}
	void ChangeDirection(int direction){
		Vector3 tempScale = transform.localScale; //localscale is the current scale which is loaded into tempscale
		tempScale.x = direction; //editing tempscale's x value
		transform.localScale = tempScale; //then loading the modified tempscale back into localscale
	}

	void CheckIfGrounded(){
		isGrounded = Physics2D.Raycast (groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);	

		if (isGrounded) {
			if (jumped) {
				jumped = false;

				anim.SetBool ("Jump", false);
			}
		}
	}

	void PlayerJump(){
		if (isGrounded) {
			if (Input.GetKey (KeyCode.Space)) {
				jumped = true;
				myBody.velocity = new Vector2 (myBody.velocity.x, jumpPower);
				anim.SetBool ("Jump", true);
			}
		}
	}


/*
	//if player and ground has trigger unchecked, it will use this and collide
	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Ground") {
			print ("Collided with the ground");
		}
	}

	//if only one player/ground has trigger unchecked, it will register the collision
	//but is no longer solid and it will pass through
	void OnTriggerEnter2D(Collider2D target){
		//if (target.tag == "Ground") {
		//	print ("collided with tag");
		//}
	}
*/
} //class












