using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

namespace GoneHome
{
    public class GameManager : MonoBehaviour
    {
        // Switches to next level when called
        public void NextEpisodes()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }

        // Restart elements in the level (without destroying)
        public void RestartLevel()
        {
            // Grab all FollowEnemy game objects from scene
            FollowEnemy[] followEnemies = FindObjectsOfType<FollowEnemy>();
            // Loop through each one
            foreach (var enemy in followEnemies)
            {
                // Reset Enemy
                enemy.Reset();
            }

            // Grab all PatrolEnemy game objects from scene
            PatrolEnemy[] patrolEnemies = FindObjectsOfType<PatrolEnemy>();
            // Loop through each one
            foreach (var enemy in patrolEnemies)
            {
                // Reset Enemy
                enemy.Reset();
            }

            // Get the player from the scene
            Player player = FindObjectOfType<Player>();
            // Reset the player
            player.Reset();
        }
    }
}
