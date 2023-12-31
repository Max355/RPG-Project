using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI healtText;
        Health health;

        void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }
        void Update()
        {
            string healthValue = String.Format("{0:0}%", health.GetPercentage());
            healtText.SetText(healthValue);
        
        }
    }
}
