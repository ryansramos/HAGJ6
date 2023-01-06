using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextIntro : MonoBehaviour
{
    [SerializeField]
    private InputReader _input;

    [SerializeField]
    private TextCard _card;

    [SerializeField]
    private Button _skipButton;

    [SerializeField]
    private TextBlock[] _textBlocks;

    [SerializeField]
    private float _inputBuffer;

    [SerializeField]
    private float _loadLag;

    [SerializeField]
    private InputManager _inputManager;
    private bool _isWaitingForInput;
    private bool _isSkipping;

    [SerializeField]
    private string _sceneToLoad;

    public MusicRequester _music;

    [Header("Broadcasting on: ")]
    [SerializeField]
    private StringEventChannelSO _loadRequestEvent;

    [SerializeField]
    private AudioCueEventChannelSO _sfxChannel;

    [SerializeField]
    private AudioCueSO[] _pianoNotes;

    private int _lastPlayed = 0;

    void OnEnable()
    {
        _inputManager.StartMenu();
        _input.OnProceedEvent += OnProceed;
        _isSkipping = false;
        _skipButton.onClick.AddListener(OnSkip);
        _card.Initialize();
        StartCoroutine(PlayTextIntro(_textBlocks));
    }

    void Start()
    {
        _music.Play();
    }

    void OnDisable()
    {
        _skipButton.onClick.RemoveListener(OnSkip);
        _input.OnProceedEvent -= OnProceed;
    }

    void OnProceed()
    {
        if (_isWaitingForInput && !_isSkipping)
        {
            int rand = -1;
            while (rand == _lastPlayed || rand < 0)
            {
                rand = Random.Range(0, _pianoNotes.Length);
            }
            _lastPlayed = rand;
            _sfxChannel.RaiseEvent(_pianoNotes[rand]);
            _isWaitingForInput = false;
        }
    }

    void OnSkip()
    {
        _isSkipping = true;
        OnTextIntroComplete();
    }

    IEnumerator PlayTextIntro(TextBlock[] _textBlocks)
    {
        if (_textBlocks == null || _textBlocks.Length < 1)
        {
            yield break;
        }
        
        _isWaitingForInput = false;
        for (int i = 0; i < _textBlocks.Length; i++)
        {
            _card.PlayNext(_textBlocks[i].Text);
            yield return new WaitForSeconds(_inputBuffer);
            _isWaitingForInput = true;
            while (_isWaitingForInput)
            {
                yield return null;
            }
            _isWaitingForInput = false;
        }
        _card.PlayNext("");
        yield return new WaitForSeconds(_loadLag);
        OnTextIntroComplete();
    }

    void OnTextIntroComplete()
    {
        _inputManager.StopMenu();
        _loadRequestEvent.RaiseEvent(_sceneToLoad);
    }
}
