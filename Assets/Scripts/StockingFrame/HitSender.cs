using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSender : MonoBehaviour
{
    [SerializeField]
    public ReticleMover _reticle;

    [Header("Broadcasting on")]
    [SerializeField]
    private Vector3EventChannel _hitSendEvent;

    public void SendHit()
    {
        Vector2 position = _reticle.GetAimPosition();
        Vector3 hitPosition = new Vector3(position.x, position.y, 0f);
        _hitSendEvent.RaiseEvent(hitPosition);
    }
}
