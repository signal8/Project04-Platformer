using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameObject bullet;
	public float bulletSpeed = 1.0f;
	public float bulletCooldown = 1.0f;
	public float resetTimer = 1.0f;

	private bool controllable = true;
	private float bulletTimer = 0.0f;
	private BoxCollider2D b2d;
	private Player p;
	private Controller2D c2d;
	private Animator anim;
	private SpriteRenderer sr;
	private Fades blind;
	
	private AudioSource jump;
	private AudioSource land;
	private AudioSource shoot;
	private AudioSource step;
	private AudioSource die;
	private bool playedStep = false;
	private bool playedLand = false;
	private bool STOP_UPDATING = false;

	// Start is called before the first frame update
	void Start()
	{
		b2d = GetComponent<BoxCollider2D>();
		p = GetComponent<Player>();
		c2d = GetComponent<Controller2D>();
		anim = GetComponentInChildren<Animator>();
		sr = GetComponentInChildren<SpriteRenderer>();

		jump = GameObject.Find("Jump").GetComponent<AudioSource>();
		land = GameObject.Find("Land").GetComponent<AudioSource>();
		shoot = GameObject.Find("Shoot").GetComponent<AudioSource>();
		step = GameObject.Find("Step").GetComponent<AudioSource>();
		die = GameObject.Find("Die").GetComponent<AudioSource>();

		blind = GameObject.Find("Blind").GetComponent<Fades>();
	}

	// Update is called once per frame
	void Update()
	{
		if (STOP_UPDATING) return;
		if (!controllable) 
		{
			p.SetDirectionalInput(Vector2.zero);
			resetTimer -= Time.deltaTime;
			if (resetTimer <= 0.0f)
			{
				blind.ResetPosition();
				blind.isDone = false;
				STOP_UPDATING = true;
			}
			return;
		}

		//if (bulletTimer > 0) bulletTimer -= Time.deltaTime;

        	Vector2 dirInput = Vector2.zero;
		dirInput.x = Input.GetAxis("Horizontal");
		dirInput.y = Input.GetAxis("Vertical");
		p.SetDirectionalInput(dirInput);

		if (dirInput.x == 0) 
		{
			step.Stop();
			playedStep = false;
			anim.SetBool("isRunning", false);
		}
		else
		{
			if (!c2d.collisions.below) step.Stop();
			else if (!step.isPlaying) step.Play();
			if (!playedStep)
			{
				step.Play();
				playedStep = true;
			}
			anim.SetBool("isRunning", true);
			if (dirInput.x < 0) sr.flipX = true;
			else sr.flipX = false;
		}


		if (Input.GetButtonDown("Fire1"))
		{
			jump.Play();
			p.OnJumpInputDown();
		}
		else if (Input.GetButtonUp("Fire1")) p.OnJumpInputUp();


		/*if (Input.GetButtonDown("Fire2")) 
		{
			FireBullet(sr.flipX);
			anim.SetBool("isGunning", true);
		}
		else if (Input.GetButtonUp("Fire2")) 
			anim.SetBool("isGunning", false);*/
	

		if (c2d.collisions.below) 
		{
			if (!playedLand)
			{
				land.Play();
				playedLand = true;
			}
			anim.SetBool("isJumping", false);
		}
		else 
		{
			playedLand = false;
			anim.SetBool("isJumping", true);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Enemy"))
		{
			Pause();
		}
	}

	void FireBullet(bool flip)
	{
		if (bulletTimer <= 0)
		{
			shoot.Play();
			bulletTimer = bulletCooldown;
			GameObject b = Instantiate(bullet, transform.position, 
					Quaternion.identity);
			Rigidbody2D brb = b.GetComponent<Rigidbody2D>();
			if (flip) brb.velocity = Vector3.left * bulletSpeed;
			else brb.velocity = Vector3.right * bulletSpeed;
		}
	}

	void Play()
	{
		controllable = true;
		anim.SetBool("isDead", false);
		b2d.size = new Vector2(0.6f, 0.8f);
	}

	void Pause()
	{
		die.Play();
		controllable = false;
		anim.SetBool("isDead", true);
		b2d.size = new Vector2(0.1f, 0.1f);
	}
}
