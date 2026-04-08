using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public float speed;
	public TextMeshProUGUI countText;
	public TextMeshProUGUI winText;
	public AudioSource pickUpSound;

	// NEU: A variable for your win particles
	public ParticleSystem winParticles;

	private Rigidbody rb;
	private int count;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText();
		winText.text = "";
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Pick Up"))
		{
			other.gameObject.SetActive(false);

			if (pickUpSound != null)
			{
				pickUpSound.Play();
			}

			count = count + 1;
			SetCountText();
		}
	}

	void SetCountText()
	{
		countText.text = "Score: " + count.ToString();

		if (count >= 12)
		{
			winText.text = "You Win!";

			// NEU: Play the particles when you win!
			if (winParticles != null)
			{
				winParticles.Play();
			}
		}
	}
}