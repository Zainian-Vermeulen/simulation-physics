using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   /// <summary>
   /// Singleton of the player for the camera
   /// </summary>
  
    #region Singleton

        public static PlayerManager Instance;

        private void Awake()
        {
            Instance = this;
        }

    #endregion

    public GameObject Player;
}
