using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private float PADDING_SIZE = 0.8f;

    private const int WAVE_WIDTH = 9;
    private const int WAVE_HEIGHT = 5;

    [SerializeField] private Transform _spawnRoot;
    [SerializeField] private float _baseWaveSpeed;
    [SerializeField] private EnemyData[] _enemyTypes;

    private char[,] _currentEnemyWave = new char[WAVE_WIDTH, WAVE_HEIGHT];
    private Vector3 _initialPosition;
    private Vector3 _currentOffset;
    private float _timePassed;

    #region Unity

    private void Awake()
    {
        _initialPosition = _spawnRoot.position;

        // 
        for (int r = 0; r < WAVE_WIDTH; ++r)
        {
            for(int c = 0; c < WAVE_HEIGHT; ++c)
            {
                _currentEnemyWave[r, c] = 'c';
            }
        }
        ResetWave();
    }

    private void Update()
    {
        _timePassed += Custom.GameData.GameTimeDelta;
    }

    private void FixedUpdate()
    {
        _spawnRoot.position = 8f * TrapezoidalWave(_timePassed, 1f, 5f, -1f, 0f) * Vector3.right + _initialPosition - _currentOffset;
    }

    #endregion

    private float TrapezoidalWave(float x, float amplitude, float period, float horizontal, float vertical)
    {
        return amplitude / Mathf.PI * (Mathf.Asin(Mathf.Sin((Mathf.PI / period) * x + horizontal)) + Mathf.Acos(Mathf.Cos((Mathf.PI / period) * x + horizontal))) - amplitude / 2 + vertical;
    }

    private float SqaureWave(float currentphase)
    {
        return Mathf.Sign(Mathf.Sin(currentphase * 2f * Mathf.PI));
    }

    private void ResetWave()
    {
        _timePassed = 0f;
        _spawnRoot.position = _initialPosition;
        _currentOffset = Vector3.zero;

        // Remove current spawned enemies
        int length = _spawnRoot.childCount;
        for (int i = 0; i < length; ++i)
        {
            Destroy(_spawnRoot.GetChild(i));
        }

        // TODO: Set _currentEnemyWave to equal the current wave

        // Spawn new enemy wave
        for (int r = 0; r < WAVE_WIDTH; ++r)
        {
            _currentOffset.y = 0f;

            for (int c = 0; c < WAVE_HEIGHT; ++c)
            {
                if (!IsValidSpace(r, c)) continue;

                if(TryGetEnemyData(_currentEnemyWave[r, c], out EnemyData data))
                {
                    Transform enemyInstance = Instantiate(data.Prefab, Vector3.zero, Quaternion.Euler(-90f, 0f, 0f)).transform;
                    enemyInstance.SetParent(_spawnRoot);
                    enemyInstance.localScale = Vector3.one;
                    enemyInstance.localPosition = _currentOffset * PADDING_SIZE;

                    r += data.WidthPadding - 1;
                }
                _currentOffset.y -= data.HeightPadding;
            }

            if (TryGetEnemyData(_currentEnemyWave[r, 0], out EnemyData first))
            {
                _currentOffset.x += first.WidthPadding;
            }
            else { _currentOffset.x += 1f; }
        }
        _currentOffset.y = 0f;
        _currentOffset *= PADDING_SIZE * 0.5f;
    }

    private bool IsValidSpace(int row, int column)
    {
        for (int i = 1; i <= column; ++i)
        {
            if (_currentEnemyWave[row, column - i] != default)
            {
                if (TryGetEnemyData(_currentEnemyWave[row, column], out EnemyData prvEntry))
                {
                    if (prvEntry.HeightPadding > i)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private bool TryGetEnemyData(char key, out EnemyData data)
    {
        for(int i = 0; i < _enemyTypes.Length; ++i)
        {
            if(_enemyTypes[i].Key == key)
            {
                data = _enemyTypes[i];
                return true;
            }
        }
        data = default;
        return false;
    }


    [Serializable]
    public struct EnemyData
    {
        public string DisplayName;
        public GameObject Prefab;

        public int WidthPadding;
        public int HeightPadding;

        public char Key;
    };
}
