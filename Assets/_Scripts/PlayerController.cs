using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


	private Rigidbody rb;
	private Renderer rend;

	public bool isGrounded;
	public bool SuperJump = false;
	public bool SuperSpeed = false;
	private bool isEquipped = false;
	private bool PowerActivated = false;
	public float PowerDuration = 10.0f;
	public float PickUpCooldown = 10.0f;
	
	public float Speed = 50.0f;
	public float SpeedModifier = 1.2f;
	public float SpeedCurrent = 50.0f;
	
	public float JumpForce = 30.0f;
	public float JumpModifier = 2.0f;
	public float JumpForceCurrent = 20.0f;
	
	public float feetDist = 1.06f;
	
	public float Threshold = -18.0f;
	public float RespawnDelay = 2.0f;
	public GameObject SpawnMarker;
	
	public bool ExpEffectActivated = false;
	public GameObject ExplosionEffect;
	private GameObject InstExpEffect;


	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		rend = GetComponent<Renderer>();
		rend.enabled = true;
	}

// Player Base Movement 
	void FixedUpdate ()
	{
		
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical) * SpeedCurrent;
			rb.AddForce (movement);

// Calls Player Respawn Coroutine
		if (transform.position.y < Threshold)
		{
			StartCoroutine(Respawn());
		}
	}

// Player Input and Results
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded == true )
		{
			rb.AddForce (new Vector3 (0, 1, 0) * JumpForceCurrent, ForceMode.Impulse);
			isGrounded = false;
		}

		if (Input.GetKeyDown (KeyCode.T) && SuperSpeed == true && PowerActivated == false)
		{
			PowerActivated = true;
			SpeedCurrent = Speed * SpeedModifier;
			isEquipped = false;
			StartCoroutine(PowerDown());
		}
		if (Input.GetKeyDown (KeyCode.T) && SuperJump == true && PowerActivated == false)
		{
			PowerActivated = true;
			JumpForceCurrent = JumpForce * JumpModifier;
			isEquipped = false;
			StartCoroutine(PowerDown());
		}

//Creates Raycast from player to set "isGrounded" boolean.
		Ray ray = new Ray(transform.position, -Vector3.up);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, feetDist))
		{
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}
	}

// Power Ups pickup, and collider states
	IEnumerator OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Speed Block"))
			{
				if (isEquipped == false)
				{
					other.gameObject.SetActive (false);
					SuperSpeed = true;
					isEquipped = true;
					yield return new WaitForSeconds (PickUpCooldown);
					other.gameObject.SetActive (true);

				}
				
			}
			else if (other.gameObject.CompareTag("Jump Block"))
			{
				if (isEquipped == false)
				{
					other.gameObject.SetActive (false);
					SuperJump = true;
					isEquipped = true;
					yield return new WaitForSeconds (PickUpCooldown);
					other.gameObject.SetActive (true);
				}
			}
		}
// handles Timer for disabling used powerups
	IEnumerator PowerDown()
		{
			Debug.Log("Outside if");
			if (PowerActivated == true && SuperSpeed == true )
			{
				Debug.Log("Inside if SS");
				yield return new WaitForSeconds (PowerDuration);
				SpeedCurrent = Speed;
				SuperSpeed = false;
				PowerActivated = false;
			}
			else if (PowerActivated == true && SuperJump == true )
			{
				Debug.Log("Inside if SJ");
				yield return new WaitForSeconds (PowerDuration);
				JumpForceCurrent = JumpForce;
				SuperJump = false;
				PowerActivated = false;
			}
		}
// Handles Player Respawn positioning and death effect		
	IEnumerator Respawn()
		{
			if (ExpEffectActivated == false )
			{
				rend.enabled = false;
				InstExpEffect = Instantiate(ExplosionEffect, transform.position, transform.rotation);
				ExpEffectActivated = true;
				Object.Destroy(InstExpEffect, 1.5f);
			}
			rb.isKinematic = true;
			yield return new WaitForSeconds (RespawnDelay);
			rb.isKinematic = false;
			rb.velocity = Vector3.zero;
			transform.position = SpawnMarker.transform.position;
			yield return new WaitForSeconds (2.0f);
			rend.enabled = true;
			ExpEffectActivated = false;
		}
}




// && Grounded == true
//Spherecast
//oncollisionenterexit
//ontrigger
/* Power List

SuperJump (Increases jump by x3)
SuperSpeed (increases speed by x2)
SuperBump (increases colision with balls by 4x)
Missle
RearShield



/* 

	Vector3 fwd = (transform.TransformDirection, Vector3.down);
		if (Physics.Raycast (transform.position, fwd, feetDist)) 
		{
			isGrounded = true;
			Debug.DrawRay(transform.position, fwd, Color.green);
		}
		else
		{
			isGrounded = false;
			Debug.DrawRay(transform.position, fwd, Color.green);
		}

*/

/*	void Update
		{
			if 
			private CoolDown = Time.time + PickUpCooldown;

		}	
*/