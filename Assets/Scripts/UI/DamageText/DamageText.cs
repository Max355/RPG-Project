using System.Collections;
using System;
using UnityEngine;
using TMPro;

namespace RPG.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] TMP_Text _damageText =null;
    
        public void DestroyText()
        {
            Destroy(gameObject);
        }


        public void SetValue(float amount)
        {
            _damageText.text = String.Format("{0:0}", amount);
        }
}
    
}
