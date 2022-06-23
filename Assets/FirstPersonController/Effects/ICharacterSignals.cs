using System;
using UniRx;
using UnityEngine;

public interface ICharacterSignals
{
    /// <summary>
    ///     The stride length of the character.
    /// </summary>
    float StrideLength { get; }

    /// <summary>
    ///     A "Is the character running?" stream.
    /// </summary>

    /// <summary>
    ///     A stream with the vectors the character has moved.
    /// </summary>
    IObservable<Vector3> Moved { get; }


    /// <summary>
    ///     A stream with stepped events. Triggered when the camera has moved one stride length.
    /// </summary>
    IObservable<Unit> Stepped { get; }
}
