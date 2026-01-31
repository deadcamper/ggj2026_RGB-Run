using System.Collections.Generic;
using UnityEngine;

public class TestRBGPlayer : MonoBehaviour
{
    public MeshRenderer PlayerMesh;
    public TestRotation PlayerRotator;

    void OnColorRed()
    {
        Debug.Log("RED");
        PlayerMesh.material.color = Color.red;
    }

    void OnColorGreen()
    {
        Debug.Log("Green");
        PlayerMesh.material.color = Color.green;
    }

    void OnColorBlue()
    {
        Debug.Log("Blue!");
        PlayerMesh.material.color = Color.blue;
    }

    void OnActionOne()
    {
        Debug.Log("One");
        PlayerRotator.SetRotationSpeed(8);
    }

    void OnActionTwo()
    {
        Debug.Log("Two");
        PlayerRotator.SetRotationSpeed(16);
    }

    void OnActionThree()
    {
        Debug.Log("Three!");
        PlayerRotator.SetRotationSpeed(32);
    }

    void OnActionNumber(UnityEngine.InputSystem.InputValue _) // pretty useless value
    {
        Debug.Log($"Number pressed!!");
    }
}
