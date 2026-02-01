using R3;
using SevenSegmentDisplay;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

public class TestObstacleSpawner : MonoBehaviour
{
    private event Action OnDebugAction;

    [SerializeField] private SevenSegmentDisplay.SevenSegmentDisplay obstaclePrefab;

    [SerializeField] private Digits digits;

    private SevenSegmentDisplay.SevenSegmentDisplay testInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        testInstance = Instantiate(obstaclePrefab);

        testInstance.Set(true, digits);

        InputSystem.onEvent
            .Where(e => e.HasButtonPress())
            .Call(eventPtr =>
            {
                foreach (var key in eventPtr.GetAllButtonPresses().OfType<KeyControl>())
                {
                    if (key.keyCode == Key.Space)
                    {
                        testInstance.Set(true, digits);
                    }
                }
            });
    }


    // Update is called once per frame
    void Update()
    {
        
    }

}
