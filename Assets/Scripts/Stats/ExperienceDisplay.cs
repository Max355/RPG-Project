using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI healtText;
        Experience experience;

        void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }
        void Update()
        {
            string healthValue = String.Format("{0:0}", experience.GetPoints());
            healtText.SetText(healthValue);
        
        }
    }
}
