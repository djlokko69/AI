using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class Enemy : MonoBehaviour
    {
        public float maxHealth = 100f;
        [Header("UI")]
        public GameObject healthBarUI;
        public Vector2 healthBarOffset = new Vector2(0f, 5f);

        private Slider healthSlider;
        private float health = 100f;

        void Start()
        {
            health = maxHealth;
        }

        // Converts Enemy world position to Screen position for health bar
        Vector3 GetHealthBarPos()
        {
            Camera cam = Camera.main;
            Vector2 position = cam.WorldToScreenPoint(transform.position);
            return position + healthBarOffset;
        }

        void Update()
        {
            // If there is a health slider
            if (healthSlider)
            {
                // Update it's position in UI
                healthSlider.transform.position = GetHealthBarPos();
            }
        }

        public void SpawnHealthBar(Transform parent)
        {
            // Create new health bar at calculated position
            // and attached to the new parent
            GameObject clone = Instantiate(healthBarUI, GetHealthBarPos(), Quaternion.identity, parent);
            // Get the slider component for later use
            healthSlider = clone.GetComponent<Slider>();
        }

        public void DealDamage(float damage)
        {
            // Deal damage to enemy
            health -= damage;
            // If there is no health
            if (health <= 0)
            {
                // Kill off the GameObject
                Destroy(gameObject);
            }
        }
    }
}
