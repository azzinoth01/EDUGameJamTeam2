using UnityEngine;
using UnityEngine.Splines;
using System.Collections;

[RequireComponent(typeof(SplineAnimate))]
public class Enemy : Unit
{
    private SplineContainer _movePath;
    [SerializeField] private float _moveSpeed = 3f;
    private float originalSpeed;

    private SplineAnimate _animate;
    private Coroutine slowRoutine;
    private Coroutine freezeRoutine;

    private void Start()
    {
        _movePath = GameInstance.Instance.MovePaths.GetRandomMovePath();
        _animate = GetComponent<SplineAnimate>();
        _animate.Container = _movePath;

        _animate.AnimationMethod = SplineAnimate.Method.Speed;
        _animate.MaxSpeed = _moveSpeed;
        originalSpeed = _moveSpeed;

        _animate.Play();
    }

    public void Freeze(float duration)
    {
        if (freezeRoutine != null)
            StopCoroutine(freezeRoutine);

        freezeRoutine = StartCoroutine(FreezeRoutine(duration));
    }

    public void Slow(float multiplier, float duration)
    {
        if (slowRoutine != null)
            StopCoroutine(slowRoutine);

        slowRoutine = StartCoroutine(SlowRoutine(multiplier, duration));
    }

    private IEnumerator FreezeRoutine(float duration)
    {
        float prevSpeed = _animate.MaxSpeed;
        _animate.MaxSpeed = 0.01f;
        yield return new WaitForSeconds(duration);
        _animate.MaxSpeed = prevSpeed;
        freezeRoutine = null;
    }

    private IEnumerator SlowRoutine(float multiplier, float duration)
    {
        _animate.MaxSpeed = originalSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        _animate.MaxSpeed = originalSpeed;
        slowRoutine = null;
    }
}
