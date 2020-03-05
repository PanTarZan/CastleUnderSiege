using System;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{

    [SerializeField] GameObject PointerPrefab = null;
    GameObject Pointer = null;
    Camera m_Camera;
    Vector3 m_PointerLocation;
    public MouseLook mouseLook = new MouseLook();

    void Start()
    {
        m_Camera = GetComponentInChildren<Camera>();
        mouseLook.Init(transform, m_Camera.transform);
        Pointer = Instantiate(PointerPrefab);
    }

    void Update()
    {
        RotateView();
        MovePointerOnPosition();
    }

    private void MovePointerOnPosition()
    {
        if (Physics.Raycast(transform.position, m_Camera.transform.forward, out RaycastHit hit, Mathf.Infinity))
        {
            Pointer.transform.position = hit.point;
        }
    }

    private void RotateView()
    {
        //avoids the mouse looking if the game is effectively paused
        if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;
        mouseLook.LookRotation(transform, m_Camera.transform);
    }
}
[Serializable]
public class MouseLook
{
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;


    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;

    public void Init(Transform character, Transform camera)
    {
        m_CharacterTargetRot = character.localRotation;
        m_CameraTargetRot = camera.localRotation;
    }


    public void LookRotation(Transform character, Transform camera)
    {
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);
        
            character.localRotation = m_CharacterTargetRot;
            camera.localRotation = m_CameraTargetRot;
    }

    
}

