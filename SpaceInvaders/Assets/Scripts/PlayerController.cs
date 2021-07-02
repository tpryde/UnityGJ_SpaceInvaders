using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _barrelPoint;
    [SerializeField, Tooltip("This value is measured in Unity meters per frame.")] private float _movementSpeed;
    [SerializeField, Tooltip("This value is measured in scaled seconds as is assigned once on Awake.")] private float _baseCoolDown;

    private Weapon _weapon;

    private void Awake()
    {
        _weapon = new Weapon(_projectilePrefab, 4f, _baseCoolDown);
    }

    private void Update()
    {
        if(Custom.Input.GetAction())
        {
            _weapon.Fire(_barrelPoint.position, Quaternion.identity);
        }

        _weapon.UpdateWeapon(Custom.GameData.GameTimeDelta);
    }

    private void FixedUpdate()
    {
        transform.position += Custom.GameData.GameTimeDelta * _movementSpeed * Custom.Input.GetHorizontal() * Vector3.right;
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

        public static bool GetAction()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Space);
        }
    }
}