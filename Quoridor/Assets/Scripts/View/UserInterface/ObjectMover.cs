using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public static class ObjectMover
{
    public static IEnumerator Move([NotNull] Transform movable, Vector3 start, Vector3 finish, float time)
    {
        void LerpConsumer(float t) => movable.localPosition = Vector3.Lerp(start, finish, t);

        return Lerp(time, LerpConsumer);
    }

    public static IEnumerator Lerp(float time, [NotNull] Action<float> tConsumer)
    {
        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime / time;
            tConsumer(t);

            yield return null;
        }

        tConsumer(1.0f);
    }
}
