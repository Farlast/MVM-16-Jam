using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Core
{
    public class SkillPool : MonoBehaviour
    {
        [SerializeField] private GameObject poolObject;
       
        void Start()
        {
            poolObject.SetActive(false);
        }

        public void Use()
        {
            poolObject.SetActive(true);
        }
        public void Release()
        {
            poolObject.SetActive(false);
            poolObject.transform.position = Vector3.zero;
        }
    }
}
