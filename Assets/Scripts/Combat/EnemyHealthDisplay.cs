using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI healthText;
        Fighter fighter;

        void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }
        public void Update()
        {
            if (fighter.GetTarget() == null)
            {
                //GetComponent<Text>().text = "N/A";
                return;
            }
            Health health = fighter.GetTarget();
            string healthValue = String.Format("{0:0}%", health.GetPercentage());
            healthText.SetText(healthValue);
        
        }
    }
}
