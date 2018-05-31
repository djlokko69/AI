using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemySpawner : Spawner
    {
        public Transform path;
        [Header("UI")]
        public Transform heathBarParent;

        private Transform start;
        private Transform end;

        void GetPath()
        {
            if(path != null)
            {
                start = path.FindChild("Start");
                end = path.FindChild("End");
            }
        }

        // Use this for initialization
        void Start()
        {
            GetPath();
        }

        public override void Spawn()
        {
            // Create a new instance of a prefab
            GameObject clone = Instantiate(prefab, start.position, start.rotation);
            // set clone under spanwer as child
            clone.transform.SetParent(transform);
            // set agent's target to end transform
            AIAgent agent = clone.GetComponent<AIAgent>();
            agent.target = end;
            // Spawn a health bar
            Enemy enemy = clone.GetComponent<Enemy>();
            enemy.SpawnHealthBar(heathBarParent);
        }
    }
}
