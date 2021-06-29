using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("This value is measured in Unity meters per frame.")] private float _playerMovementSpeed;

    private void FixedUpdate()
    {
        transform.position += Time.deltaTime * _playerMovementSpeed * Custom.Input.GetHorizontal() * Vector3.right;
    }
};

namespace Custom
{
    public class Input
    {
        public static float GetHorizontal()
        {
            float left = (UnityEngine.Input.GetKey(KeyCode.A) || UnityEngine.Input.GetKey(KeyCode.LeftArrow)) ? -1f : 0f;
            float right = (UnityEngine.Input.GetKey(KeyCode.D) || UnityEngine.Input.GetKey(KeyCode.RightArrow)) ? 1f : 0f;

            return left + right;
        }
    };

}