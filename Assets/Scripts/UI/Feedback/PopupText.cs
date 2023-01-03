using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    [SerializeField]
    private float _lifetime;

    [SerializeField]
    private float _upForce;
    private Rigidbody2D _rb;

    private TextMeshProUGUI _textMesh;

    void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _textMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        _rb.AddForce(_upForce * Vector3.up, ForceMode2D.Impulse);
        TextFader fader = TextFader.FadeOut(_textMesh, _lifetime, this);
        Destroy(this.gameObject, _lifetime);
    }
}
