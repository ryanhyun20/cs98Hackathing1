using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // speed of rotation for turning animation
    public float turnSpeed = 20f;

    // reference to Animator component
    Animator m_Animator;
    Rigidbody m_Rigidbody;

    // variable that represents change in position
    Vector3 m_Movement;

    Quaternion m_Rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      // horizontal axis variable declaration
      float horizontal = Input.GetAxis ("Horizontal");
      // vertical axis variable declaration
      float vertical = Input.GetAxis ("Vertical");

      // initialize and normalize vector for positioning
      m_Movement.Set(horizontal, 0f, vertical);
      m_Movement.Normalize ();

      // detecting player input
      bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
      bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
      bool isWalking = hasHorizontalInput || hasVerticalInput;
      m_Animator.SetBool("IsWalking", isWalking);

      // forward vector calculation
      Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
      m_Rotation = Quaternion.LookRotation (desiredForward);
    }
    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }
}
