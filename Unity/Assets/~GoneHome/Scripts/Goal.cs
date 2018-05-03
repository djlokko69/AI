using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace GoneHome
{
    public class Goal : MonoBehaviour
    {
        public UnityEvent onTrigger;

        // Trigger function called when other object enters
        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Player")
            {
                onTrigger.Invoke();
            }
        }
    }
}
