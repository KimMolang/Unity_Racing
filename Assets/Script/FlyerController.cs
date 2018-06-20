using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlyerController : MonoBehaviour {

    [Header("Ratate And Move")]
    [SerializeField] [Range(1.0f, 10.0f)] private float m_fMouseSensitivity = 8.0f;
    [SerializeField] [Range(0.0f, 80.0f)] private float m_fMoveSpeed        = 20.0f;
    [SerializeField] [Range(0.0f, 80.0f)] private float m_fTurnSpeed        = 30.0f;


    private Vector3 ratationRadian   = new Vector3(0.0f, 0.0f, 0.0f);

    // Other Component
    private Rigidbody rigidBody;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

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

        ratationRadian.y += fHorizontalRotation * m_fTurnSpeed * Time.smoothDeltaTime;
        ratationRadian.x += 0;//fVerticalRotation * m_fTurnSpeed * Time.smoothDeltaTime; // (test)

        if (ratationRadian.x > MAX_DIR_X)
            ratationRadian.x = MAX_DIR_X;
        else if (ratationRadian.x < MIN_DIR_X)
            ratationRadian.x = MIN_DIR_X;

        transform.rotation = Quaternion.Euler
            ( transform.rotation.x + ratationRadian.x
            , transform.rotation.y + ratationRadian.y
            , 0.0f );

        //m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);

        //Debug.Log(ratationRadian.x);
    }

    private void UpdateMove()
    {
        float fVerticalMovement     = Input.GetAxisRaw("Vertical");

        transform.position += transform.forward * fVerticalMovement * m_fMoveSpeed * Time.smoothDeltaTime;
        //Vector3 vMovement = transform.forward * fVerticalMovement * m_fMoveSpeed * Time.fixedDeltaTime;
        //rigidBody.MovePosition(rigidBody.position + vMovement);
    }
}
