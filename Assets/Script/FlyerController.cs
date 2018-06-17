using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlyerController : MonoBehaviour {

    [Header("Ratate And Move")]
    [SerializeField] [Range(1.0f, 10.0f)] private float m_fMouseSensitivity = 8.0f;
    [SerializeField] [Range(0.0f, 80.0f)] private float m_fMoveSpeed        = 30.0f;
    [SerializeField] [Range(0.0f, 80.0f)] private float m_fTurnSpeed        = 30.0f;


    private Vector3 m_vRatationRadian   = new Vector3(0.0f, 0.0f, 0.0f);

    // Other Component
    private Rigidbody m_comRigidBody;


    private void Awake()
    {
        m_comRigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        m_comRigidBody.isKinematic = false;
    }

    private void OnDisable()
    {
        m_comRigidBody.isKinematic = true;
    }

    private void Start ()
    {
		// In Network
	}

    private void Update ()
    {

    }

    private void FixedUpdate()
    {
        UpdateRotate();
        UpdateMove();
    }

    private void UpdateRotate()
    {
        const float MAX_DIR_X = 70.0f;
        const float MIN_DIR_X = -70.0f;

        float fHorizontalRotation = Input.GetAxis("Mouse X") * m_fMouseSensitivity;
        float fVerticalRotation     = -1 * (Input.GetAxis("Mouse Y") * m_fMouseSensitivity);

        m_vRatationRadian.y += fHorizontalRotation * m_fTurnSpeed * Time.smoothDeltaTime;
        m_vRatationRadian.x += fVerticalRotation * m_fTurnSpeed * Time.smoothDeltaTime;

        if (m_vRatationRadian.x > MAX_DIR_X)
            m_vRatationRadian.x = MAX_DIR_X;
        else if (m_vRatationRadian.x < MIN_DIR_X)
            m_vRatationRadian.x = MIN_DIR_X;

        transform.rotation = Quaternion.Euler
            ( transform.rotation.x + m_vRatationRadian.x
            , transform.rotation.y + m_vRatationRadian.y
            , 0.0f );

        //m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);

        //Debug.Log(m_vRatationRadian.x);
    }

    private void UpdateMove()
    {
        float fVerticalMovement     = Input.GetAxisRaw("Vertical");

        transform.position += transform.forward * fVerticalMovement * m_fMoveSpeed * Time.smoothDeltaTime;
        //Vector3 vMovement = transform.forward * fVerticalMovement * m_fMoveSpeed * Time.fixedDeltaTime;
        //m_comRigidBody.MovePosition(m_comRigidBody.position + vMovement);
    }
}
