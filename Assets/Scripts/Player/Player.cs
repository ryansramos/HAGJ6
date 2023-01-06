using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private InputReader _input;

    [SerializeField]
    public Hammer 
        LHammer,
        RHammer;

    [SerializeField]
    private Vector3
        LStart,
        RStart;

    private IEnumerator
        _lCoroutine,
        _rCoroutine;
    
    [SerializeField]
    public RageManager rageManager;

    [SerializeField]
    public ReticleMover reticle;
    private bool _isReadingInput;

    void OnEnable()
    {
        _input.OnPrimaryEvent += OnPrimary;
        _input.OnSecondaryEvent += OnSecondary;
        _input.OnResetPrimaryEvent += OnResetPrimary;
        _input.OnResetSecondaryEvent += OnResetSecondary;
        _input.OnInteractEvent += OnInteract;
        _input.OnRageEvent += OnRage;
        LHammer.MoveToTargetEvent.AddListener(OnMoveToTarget);
        RHammer.MoveToTargetEvent.AddListener(OnMoveToTarget);
        LHammer.ReturnToPlayerEvent.AddListener(OnReturnToPlayer);
        RHammer.ReturnToPlayerEvent.AddListener(OnReturnToPlayer);

    }
    void OnDisable()
    {
        _input.OnPrimaryEvent -= OnPrimary;
        _input.OnSecondaryEvent -= OnSecondary;
        _input.OnResetPrimaryEvent -= OnResetPrimary;
        _input.OnResetSecondaryEvent -= OnResetSecondary;
        _input.OnInteractEvent -= OnInteract;
        _input.OnRageEvent -= OnRage;

        LHammer.MoveToTargetEvent.RemoveListener(OnMoveToTarget);
        RHammer.MoveToTargetEvent.RemoveListener(OnMoveToTarget);
        LHammer.ReturnToPlayerEvent.RemoveListener(OnReturnToPlayer);
        RHammer.ReturnToPlayerEvent.RemoveListener(OnReturnToPlayer);
    }

    public void OnGameStart()
    {
        _isReadingInput = true;
        LHammer.OnGameStart();
        RHammer.OnGameStart();
    }

    public void OnGameOver()
    {
        _isReadingInput = false;
        LHammer.OnGameOver();
        RHammer.OnGameOver();
    }

    void OnPrimary()
    {
        if (_isReadingInput)
        {
            LHammer.Swing();
        }
    }

    void OnSecondary()
    {
        if(_isReadingInput)
        {
            RHammer.Swing();
        }
    }

    void OnMoveToTarget(Hammer hammer, float duration)
    {
        if (hammer == LHammer)
        {
            if (_lCoroutine != null)
            {
                StopCoroutine(_lCoroutine);
            }
            _lCoroutine = MoveToPosition(hammer.transform, reticle.GetAimPosition(), duration);
            StartCoroutine(_lCoroutine);
        }
        else if (hammer == RHammer)
        {
            if (_rCoroutine != null)
            {
                StopCoroutine(_rCoroutine);
            }
            _rCoroutine = MoveToPosition(hammer.transform, reticle.GetAimPosition(), duration);
            StartCoroutine(_rCoroutine);
        }
    }

    IEnumerator MoveToPosition(Transform t, Vector2 position, float duration)
    {
        t.parent = null;
        float timer = 0f;
        Vector3 targetPosition = Camera.main.ViewportToWorldPoint(new Vector3(position.x, position.y, 0f));
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, 0f);
        while (timer < duration)
        {
            Vector3 newPosition = Vector3.Lerp(t.position, targetPosition, timer / duration);
            t.position = newPosition;
            timer += Time.deltaTime;
            yield return null;
        }
        Vector3 endPosition = Vector3.Lerp(t.position, targetPosition, 1f);
        t.position = endPosition;
    }

    IEnumerator ReturnToPlayer(Hammer hammer, float duration)
    {
        hammer.transform.parent = gameObject.transform;
        Vector3 start = Vector3.zero;
        if (hammer == LHammer)
        {
            start = LStart;
        }
        else if (hammer == RHammer)
        {
            start = RStart;
        }

        Transform t = hammer.gameObject.transform;

        float timer = 0f;
        while (timer < duration)
        {
            Vector3 newPosition = Vector3.Lerp(t.position, gameObject.transform.position + start, timer / duration);
            t.position = newPosition;
            timer += Time.deltaTime;
            yield return null;
        }
        Vector3 endPosition = gameObject.transform.position + start;
        t.position = endPosition;
    }

    void OnReturnToPlayer(Hammer hammer, float duration)
    {
        if (hammer == LHammer)
        {
            if (_lCoroutine != null)
            {
                StopCoroutine(_lCoroutine);
            }
            _lCoroutine = ReturnToPlayer(hammer, duration);
            StartCoroutine(_lCoroutine);
        }
        else if (hammer == RHammer)
        {
            if (_rCoroutine != null)
            {
                StopCoroutine(_rCoroutine);
            }
            _rCoroutine = ReturnToPlayer(hammer, duration);
            StartCoroutine(_rCoroutine);
        }
    }


    void OnResetPrimary()
    {
        if (_isReadingInput)
        {   
            LHammer.TryResetCooldown();
        }
    }

    void OnResetSecondary()
    {
        if (_isReadingInput)
        {
            RHammer.TryResetCooldown();
        }
    }

    void OnRage()
    {
        if (_isReadingInput)
        {
            rageManager.RageInput();
        }
    }

    void OnInteract()
    {
    }
}
