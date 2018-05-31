using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Spawner : MonoBehaviour
    {
        public GameObject prefab;
        public float spawnRate = 1f;

        private float timer = 0f;

        public virtual void Spawn() { }

        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= spawnRate)
            {
                Spawn();
                timer = 0f;
            }
        }
    }
}

