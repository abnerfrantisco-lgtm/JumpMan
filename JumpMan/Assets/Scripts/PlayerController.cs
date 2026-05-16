using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movimentação")]
    private Rigidbody rb;//Fisica
    public float jumpForce = 10f;//força do pulo
    public bool isGrounded = true;//Se o personagem tocou o chão

    [Header("Animações")]
    private Animator playerAnim;

    [Header("Physics")]
    public float gravityModifier = 1f;

    [Header("Particulas")]
    public ParticleSystem explosionParticle;
    public ParticleSystem particleLeft;
    public ParticleSystem particleRight;

    [Header("Audio")]
    public AudioSource _audioSource;
    public List<AudioClip> _sounds;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(
            Vector3.down * (gravityModifier - 1) *
            Physics.gravity.magnitude, ForceMode.Acceleration);
    }

    private void OnJump(InputValue value)
    {
        if (isGrounded && value.isPressed && !SpawnManager.Instance.IsgameOver)
        {
            _audioSource.PlayOneShot(_sounds[0]);
            particleLeft.Stop();
            particleRight.Stop();
            isGrounded = true;

            playerAnim.SetTrigger("Jump_trig");

            rb.AddForce(
                Vector3.up * jumpForce, ForceMode.Impulse);

            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Groubd")&& 
            !SpawnManager.Instance.IsgameOver)
        {

            _audioSource.PlayOneShot(_sounds[0]);
            explosionParticle.Play();
            particleLeft.Play();
            particleRight.Play();
            isGrounded = true;

        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            SpawnManager.Instance.IsgameOver = true;
            Destroy(collision.gameObject);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    }

}