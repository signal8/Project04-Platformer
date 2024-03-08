using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoningEnemy : MonoBehaviour
{
	public float honingRange = 3.0f;
	public float honingSpeed = 5.0f;

	private GameObject player;
	private ParticleSystem ps;
	private BoxCollider2D b2d;
	private SpriteRenderer sr;
	private bool startHoning = false;
	private bool explotano = false;
	private float timer = 0.0f;
	private Vector3 lastSeen;
	private Color startColor;

	void Awake()
	{
        	player = GameObject.FindWithTag("Player");
		ps = GetComponent<ParticleSystem>();
		b2d = GetComponent<BoxCollider2D>();
		sr = GetComponent<SpriteRenderer>();

		startColor = sr.color;
	}

	void Update()
	{
		if (explotano)
		{
			sr.color = Color.Lerp(startColor, Color.clear, 
					timer * 2);
			timer += Time.deltaTime;
			if (timer >= 0.5f) Destroy(gameObject);
			return;
		}

        	if (!startHoning && Vector3.Distance(transform.position,
					player.transform.position) 
					< honingRange)
		{
			startHoning = true;
			lastSeen = player.transform.position;
		}
		else if (startHoning)
		{
			transform.position = new Vector3(
				Mathf.Lerp(transform.position.x,
				lastSeen.x, timer/honingSpeed),
				Mathf.Lerp(transform.position.y,
				lastSeen.y, timer/honingSpeed),
				Mathf.Lerp(transform.position.z,
				lastSeen.z, timer/honingSpeed));

			timer += Time.deltaTime;
			if (timer >= 1.0f)
			{
				if (Vector3.Distance(transform.position,
					player.transform.position)
					< honingRange)
					lastSeen = player.transform.position;
				else startHoning = false;
				timer = 0.0f;
			}
		}
		transform.rotation = Quaternion.identity;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = new Color(0, 0, 1, .5f);
		Gizmos.DrawSphere(transform.position, honingRange);
	}

	void Explotano()
	{
		ps.Play();
		b2d.enabled = false;
		timer = 0.0f;
		explotano = true;
	}
}
