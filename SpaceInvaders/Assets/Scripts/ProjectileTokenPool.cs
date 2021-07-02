using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom
{
    public class ProjectileTokenPool
    {
        private static ProjectileTokenPool s_instance;
        private const int MAX_PROJECTILE_COUNT = 5;
        private const string DEFAULT_KEY = "Default";

        private Dictionary<string, GameObject[]> _activeProjectiles;

        public static ProjectileTokenPool RequestReference()
        {
            if(s_instance == null)
            {
                s_instance = new ProjectileTokenPool();
            }
            return s_instance;
        }

        private ProjectileTokenPool()
        {
            _activeProjectiles = new Dictionary<string, GameObject[]>();
        }

        public T RequestProjectile<T>(T prefab, Vector3 position, Quaternion rotation, string key = DEFAULT_KEY) where T : ProjectileController
        {
            if (_activeProjectiles.TryGetValue(key, out GameObject[] projectiles))
            {
                for (int i = 0; i < projectiles.Length; ++i)
                {
                    if (projectiles[i] == null)
                    {
                        GameObject go = GameObject.Instantiate(prefab.gameObject, position, rotation);
                        projectiles[i] = go;
                        return go.GetComponent<T>();
                    }
                }
            }
            else
            {
                GameObject go = GameObject.Instantiate(prefab.gameObject, position, rotation);
                GameObject[] goArr = new GameObject[MAX_PROJECTILE_COUNT];
                goArr[0] = go;
                _activeProjectiles.Add(key, goArr);
                return go.GetComponent<T>();
            }
            return null;
        }
    }
}