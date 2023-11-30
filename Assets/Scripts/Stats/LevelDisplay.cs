using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI healtText;
        BaseStats baseStats;

        void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }
        void Update()
        {
            string healthValue = String.Format("{0:0}", baseStats.GetLevel());
            healtText.SetText(healthValue);
        
        }
    }
}
