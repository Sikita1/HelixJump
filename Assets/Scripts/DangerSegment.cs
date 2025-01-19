using System;
using UnityEngine;

public class DangerSegment : MonoBehaviour
{
    public event Action GameOver;

    public void GameOvering() =>
        GameOver?.Invoke();
}
