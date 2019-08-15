using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

private Rigidbody rb;
private Renderer rend;
public float Score = 0.0f;
public float ExpRadius = 150.0f;
public float ExpPower = 100.0f;
public float Threshold = -20.0f;
private bool ExpEffectActivated = false;
private bool GoalScored = false;
public GameObject BallSpawnMarker;
public GameObject ExplosionEffect;
public GameObject SoccerBall;
private GameObject InstExpEffect;


	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rend = GetComponent<Renderer>();
		rend.enabled = true;
	}

	void FixedUpdate ()
	{
		if (transform.position.y < Threshold)
		{
			rb.velocity = Vector3.zero;
			transform.position = BallSpawnMarker.transform.position;
			rb.isKinematic = true;
			rb.isKinematic = false;
		}
		if (GoalScored == true)
		{
			GoalExplosion();
		}
	}
// Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
        	Score = Score + 1.0f;
       		Debug.Log("Score!");
       		GoalScored = true;
       	}
	}

    void GoalExplosion()
    {
    	if (ExpEffectActivated == false)
    	{
    		InstExpEffect = Instantiate(ExplosionEffect, transform.position, transform.rotation);
    		ExpEffectActivated = true;
    	}
    	Vector3 ExpPosition = transform.position;
    	Collider[] colliders = Physics.OverlapSphere(ExpPosition, ExpRadius);
       	foreach (Collider nearbyObject in colliders)
       	{
       		Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
       		if (rb != null)
       		{
       			rb.AddExplosionForce(ExpPower, ExpPosition, ExpRadius, 10.0f);
       		}
       	}
       	//SoccerBall.SetActive(false);
		Object.Destroy(InstExpEffect, 1.5f); 
		rend.enabled = false;   	
    }
}
