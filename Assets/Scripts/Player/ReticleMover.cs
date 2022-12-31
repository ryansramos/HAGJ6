using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleMover : MonoBehaviour
{
    [SerializeField]
    private InputReader _input;
    private Transform _transform;
    private Vector2 _aimPosition = new Vector2();

    void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
    }

    void OnEnable()
    {
        _input.OnAimEvent += OnAim;
    }

    void OnDisable()
    {
        _input.OnAimEvent -= OnAim;
    }

    void OnAim(Vector2 input)
    {
        float adjustedX = input.x / Screen.width;
        float adjustedY = input.y / Screen.height;
        Cursor.visible = !IsInsideViewport(adjustedX, adjustedY);
        float inputX = Mathf.Clamp(adjustedX, 0f, 1f);
        float inputY = Mathf.Clamp(adjustedY, 0f, 1f);
        _aimPosition = new Vector2(inputX, inputY);
    }

    bool IsInsideViewport(float x, float y)
    {
        bool isXInRange = x > 0 && x < 1;
        bool isYInRange = y > 0 && y < 1;
        return isXInRange && isYInRange;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 reticlePosition = new Vector3(_aimPosition.x, _aimPosition.y, 0f);
        Vector3 adjustedReticlePosition = Camera.main.ViewportToWorldPoint(reticlePosition);
        _transform.position = new Vector3(adjustedReticlePosition.x, adjustedReticlePosition.y, 0f);
    }

    public Vector2 GetAimPosition()
    {
        return _aimPosition;
    }
}
