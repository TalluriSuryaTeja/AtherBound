using UnityEngine;

public class PlayerAbilityHandler : MonoBehaviour
{
    [Tooltip("The amount of mana a magic attack costs.")]
    public float magicManaCost = 10f;

    private AetherboundPlayerController _playerController;
    private ManaManager _manaManager;
    private MagicCasting _magicCasting;
    private Animator _animator;
    private int _animIDMagic;

    private void Awake()
    {
        _playerController = GetComponent<AetherboundPlayerController>();
        _manaManager = GetComponent<ManaManager>();
        _magicCasting = GetComponent<MagicCasting>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _animIDMagic = Animator.StringToHash("Magic");
    }

    private void OnEnable()
    {
        if (_playerController != null)
        {
            _playerController.OnMagicCast += HandleMagicCast;
        }
    }

    private void OnDisable()
    {
        if (_playerController != null)
        {
            _playerController.OnMagicCast -= HandleMagicCast;
        }
    }

    private void HandleMagicCast()
    {
        if (_manaManager != null && _manaManager.UseMana(magicManaCost))
        {
            if (_animator != null)
            {
                _animator.SetTrigger(_animIDMagic);
            }
            _magicCasting?.CastMagic();
        }
        else
        {
            Debug.Log("Not enough mana to cast magic!");
        }
    }
}
