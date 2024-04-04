using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : CoreComponent
{
    [SerializeField] bool canBlink;
    [SerializeField] bool canFlash;
    
    public Action onAnimationTrigger;
    public Action onAnimationFinishTrigger;

    private Health health;
    private SpriteRenderer sprite;
    private Animator anim;
    
    BlinkingEffect blinkingEffect;
    FlashingEffect flashingEffect;

    protected override void Awake() 
    {
        base.Awake();
        
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        AddEffect();
    }

    void AddEffect()
    {
        if (canBlink)
        {
            if (!TryGetComponent<BlinkingEffect>(out blinkingEffect))
            {
                blinkingEffect = gameObject.AddComponent<BlinkingEffect>();
            }
        }
        if (canFlash)
        {
            if (!TryGetComponent<FlashingEffect>(out flashingEffect))
            {
                flashingEffect = gameObject.AddComponent<FlashingEffect>();
            }
        }
    }

    void Start() 
    {
        health = core.GetCoreComponent<Health>();
        health.OnTakeDamage += StartHitVFX;
    }

    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());
    }

    IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => core.GetCoreComponent<Health>() != null);
        health = core.GetCoreComponent<Health>();
        health.OnTakeDamage += StartHitVFX;
    }

    private void OnDisable()
    {
        health.OnTakeDamage -= StartHitVFX;
    }


    #region Animation

    public Animator GetAnim()
    {
        return anim;
    }
    
    public void Play(int id) 
    {
        anim.Play(id);
    }

    public void Play(string clipName)
    {
        anim.Play(clipName);
    }

    public void AnimationTrigger()
    {
        onAnimationTrigger?.Invoke();
    }

    public void AnimationFinishTrigger()
    {
        onAnimationFinishTrigger?.Invoke();
    }

    public void ToggleSprite(bool isActive)
    {
        sprite.enabled = isActive;
    }

    #endregion

    #region Effect

    public void StartHitVFX(bool needToResetPlayerPosition = false)
    {
        StartBlinking();
        StartFlashing();
    }

    public void StartBlinking()
    {
        if (blinkingEffect == null) return;

        blinkingEffect.StartBlinking();
    }

    public void StartFlashing()
    {
        if (flashingEffect == null) return;
        
        flashingEffect.StartFlashing();
    }

    
    private int _flashAmount = Shader.PropertyToID("_FlashAmount");
    private float maxFlashAmount = 1f;
    private float minFlashAmount = 0f;
    private float restLerpTime = .5f;
    

    #endregion
}
