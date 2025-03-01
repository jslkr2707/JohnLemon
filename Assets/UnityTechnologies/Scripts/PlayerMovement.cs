using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Animator m_animator;
    Rigidbody m_rigidbody;
    AudioSource m_AudioSource;

    float multiplier;
    public float sprint;
    public float maxSprint = 100f;
    
    public Slider healthSlider;
    Vector3 m_movement;
    Quaternion m_rotation = Quaternion.identity;
    public GameObject nameText;
    
    public float turnSpeed = 20f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        sprint = maxSprint;
        nameText = GameObject.Find("PlayerName");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        m_movement.Set(horizontal, 0f, vertical);
        m_movement.Normalize();
        
        bool isWalking = !Mathf.Approximately (horizontal, 0f) || !Mathf.Approximately (vertical, 0f);
        
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                sprint -= Mathf.Min(maxSprint / 5f * Time.deltaTime, sprint);
            }
        } else
        {
            m_AudioSource.Stop();
        }
        
        if (!Input.GetKey(KeyCode.LeftShift)) sprint += Mathf.Min(maxSprint / 10f * Time.deltaTime, maxSprint - sprint);
        multiplier = Input.GetKey(KeyCode.LeftShift) && sprint > 0f ? 1.5f : 1f;
        
        m_animator.SetBool("IsWalking", isWalking);
        m_animator.speed = multiplier;
        
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_movement * multiplier, turnSpeed * Time.deltaTime, 0f);
        m_rotation = Quaternion.LookRotation(desiredForward);
        
        healthSlider.value = sprint / maxSprint;
        nameText.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.6f);
    }
    
    void OnAnimatorMove()
    {
        m_rigidbody.MovePosition(m_rigidbody.position + m_movement * m_animator.deltaPosition.magnitude * multiplier);
        m_rigidbody.MoveRotation(m_rotation);
    }
}
