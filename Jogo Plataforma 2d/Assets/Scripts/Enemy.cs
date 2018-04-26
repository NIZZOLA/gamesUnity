using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed;					// velocidade
	Rigidbody2D rb;				
	bool facingRight = false;			// é o lado para o qual a face deve estar
	bool noChao = false;				// marca se está no chão
	Transform groundCheck;

	public float jumpForce = 700;		// força para pulo

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		groundCheck = transform.Find ("EnemyGroundCheck");

	}
	
	// Update is called once per frame
	void Update () {
		// faz a checagem se está no chão
		noChao = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		Debug.Log ("chao:" + noChao);
		// caso não esteja, vai mudar a velocidade de lado
		if (!noChao)
			speed *= -1;

		//Debug.Log (speed);
	}

	void FixedUpdate()
	{
		rb.velocity = new Vector2 (speed, rb.velocity.y);

		if (speed > 0 && !facingRight)
			Flip ();
		else if (speed < 0 && facingRight)
			Flip ();
		
	}

	void Flip()
	{
		// inverte a face do inimigo
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
}
