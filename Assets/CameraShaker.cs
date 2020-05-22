using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShaker : MonoBehaviour
{
    //partymaker
    [SerializeField] private Camera mahCamera;
    [SerializeField] private float degreeDuration = 1f;

    [SerializeField] private float minRot = 1f;
    [SerializeField] private float maxRot = 1f;
    void Start()
    {
        RecursiveTween(() => MakeRandomShake(degreeDuration));            
    }

    private void RecursiveTween(Func<Tween> generator)
    {
        var tween = generator();
        DOTween.Sequence()
            .Append(tween)
            .OnComplete(() => RecursiveTween(generator));
    }

    private Quaternion RotateQ(Quaternion startRot, float angle, Vector3 axis)
    {
        var deltaRot = Quaternion.AngleAxis(angle, axis);
        var resultRot = startRot * deltaRot;
        return resultRot;
    }

    private Quaternion MakeRot(float angle, Vector3 axis)
    {
        var startRot = mahCamera.transform.rotation;
        return RotateQ(startRot, angle, axis);
    }

    private Tween MakeRandomShake(float degreeDur)
    {
        var angle = MakeRandAngle();
        var duration = angle * degreeDur;
        var sign = Random.value > 0.5f ? 1f : -1f;

        var axis = Vector3.forward;
        var toRot = MakeRot(sign * angle, axis);
        return ShakeCamera(toRot, duration);
    }

    private float MakeRandAngle()
    {
        return  Random.Range(minRot, maxRot);
    }

    private Tween ShakeCamera(Quaternion toRot, float duration)
    {
        var startRot = mahCamera.transform.rotation;
        return DOTween.Sequence()
            .Append(RotateCamera(toRot, duration))
            .Append(RotateCamera(startRot, duration));
    }
    private Tween RotateCamera(Quaternion toRot, float duration)
    {
        var t = mahCamera.transform;
        return t.DORotateQuaternion(toRot, duration)
            .SetEase(Ease.Linear);
    }
}
