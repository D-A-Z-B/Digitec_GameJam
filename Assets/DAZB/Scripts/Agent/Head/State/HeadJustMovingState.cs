using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeadJustMovingState : HeadState
{
    public HeadJustMovingState(Head head, HeadStateMachine stateMachine, string animBoolName) : base(head, stateMachine, animBoolName)
    {
    }

    private readonly int WaveDistanceFromCenterHash = Shader.PropertyToID("_WaveDistanceFromCenter");
    private Coroutine shockWaveRoutine;
    private Coroutine cameraRoutine;

    public override void Enter()
    {
        base.Enter();
        head.StartCoroutine(JustMovingRoutine());
        shockWaveRoutine = head.StartCoroutine(StartShockWaveRoutine());
        cameraRoutine = head.StartCoroutine(StartCameraRoutine());
        head.player.InputReader.PlayerInputDisable();
    }

    private IEnumerator StartCameraRoutine()
    {
        float currentTime = 0;
        float totalTime = 5f;
         while (currentTime / totalTime <= 1) {
            if (currentTime < 0.3f) {
                float value = Mathf.Lerp(5, 2.5f, currentTime / totalTime);
                Camera.main.orthographicSize = value;
                yield return null;
                currentTime += Time.unscaledDeltaTime;
            }
            else {
                float value = Mathf.Lerp(4.8f, 2.5f, currentTime / totalTime);
                Camera.main.orthographicSize = value;
                yield return null;
                currentTime += Time.deltaTime;
            }
        }
    }

    private IEnumerator EndCameraRoutine()
    {
        float currentTime = 0;
        float totalTime = 0.3f;
        while (currentTime < totalTime)
        {
            float value = Mathf.Lerp(Camera.main.orthographicSize, 5f, currentTime / totalTime);
            Camera.main.orthographicSize = value;
            yield return null;
            currentTime += Time.deltaTime;
        }
        Camera.main.orthographicSize = 5;
    }

    private IEnumerator JustMovingRoutine()
    {
        float elapsedTime = 0;
        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(1, 0.1f, elapsedTime / 0.5f);
            yield return null;
        }
        Time.timeScale = 0.1f; // Ensure time scale is set to 0.1 after the loop
        yield return new WaitForFixedUpdate();
        yield return new WaitUntil(() => Mouse.current.leftButton.wasPressedThisFrame);
        stateMachine.ChangeState(HeadStateEnum.Moving);
    }

    private IEnumerator StartShockWaveRoutine() {
        Debug.Log("ShockWaveRoutine started");
        float currentTime = 0;
        float totalTime = 5f;
        Material mat = head.ShockWave.GetComponent<SpriteRenderer>().material;

        if (mat == null) {
            Debug.LogError("Material is null. Please check the ShockWave GameObject and ensure it has a SpriteRenderer with a Material.");
            yield break;
        }
        mat.SetFloat(WaveDistanceFromCenterHash, -0.1f);
        while (currentTime / totalTime <= 1) {
            if (currentTime < 0.3f) {
                float value = Mathf.Lerp(-0.1f, 1, currentTime / totalTime);
                mat.SetFloat(WaveDistanceFromCenterHash, value);
                yield return null;
                currentTime += Time.unscaledDeltaTime * 150;
            }
            else {
                float value = Mathf.Lerp(-0.1f, 1, currentTime / totalTime);
                mat.SetFloat(WaveDistanceFromCenterHash, value);
                yield return null;
                currentTime += Time.deltaTime;
            }
        }
    }


    private IEnumerator EndShockWaveRoutine()
    {
        Debug.Log("EndShockWaveRoutine started");
        float currentTime = 0;
        float totalTime = 0.3f;
        Material mat = head.ShockWave.GetComponent<SpriteRenderer>().material;

        if (mat == null)
        {
            Debug.LogError("Material is null. Please check the ShockWave GameObject and ensure it has a SpriteRenderer with a Material.");
            yield break;
        }

        float startValue = mat.GetFloat(WaveDistanceFromCenterHash);
        while (currentTime < totalTime)
        {
            float value = Mathf.Lerp(startValue, -0.1f, currentTime / totalTime);
            mat.SetFloat(WaveDistanceFromCenterHash, value);
            yield return null;
            currentTime += Time.deltaTime;
        }
        mat.SetFloat(WaveDistanceFromCenterHash, -0.1f); // Ensure final value is set
    }

    public override void Exit()
    {
        if (shockWaveRoutine != null)
        {
            head.StopCoroutine(shockWaveRoutine);
        }
        if (cameraRoutine != null) {
            head.StopCoroutine(cameraRoutine);
        }
        head.StartCoroutine(EndCameraRoutine());
        head.StartCoroutine(EndShockWaveRoutine());
        Time.timeScale = 1f;
        head.player.InputReader.PlayerInputEnable();
        base.Exit();
    }
}
