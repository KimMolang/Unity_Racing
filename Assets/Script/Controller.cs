using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float mouseSensitivity = 2.0f;
    public float speed = 10.0f;

    private Vector3 m_vRatationRadian   = new Vector3(0.0f, 0.0f, 0.0f);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateRotate();
        UpdateMove();
        
        
        
    }

    private void UpdateRotate()
    {
        const float MAX_DIR_X = 1.0f;
        const float MIN_DIR_X = -1.2f;

        float fHorizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;// * Time.smoothDeltaTime;
        float fVerticalRotation     = -1 * (Input.GetAxis("Mouse Y") * mouseSensitivity);

        m_vRatationRadian.y += fHorizontalRotation;// * Time.smoothDeltaTime;
        m_vRatationRadian.x += fVerticalRotation;// * Time.smoothDeltaTime;

        //if (m_vRatationRadian.x > MAX_DIR_X)
        //    m_vRatationRadian.x = MAX_DIR_X;
        //else if (m_vRatationRadian.x < MIN_DIR_X)
        //    m_vRatationRadian.x = MIN_DIR_X;

        transform.rotation = Quaternion.Euler
            ( transform.rotation.x + m_vRatationRadian.x
            , transform.rotation.y + m_vRatationRadian.y
            , 0.0f );
    }

    private void UpdateMove()
    {
        float fVerticalMovement     = Input.GetAxisRaw("Vertical");

        transform.position += transform.forward * fVerticalMovement * speed * Time.smoothDeltaTime;
    }

    void FixedUpdate()
    {
        
    }
}
