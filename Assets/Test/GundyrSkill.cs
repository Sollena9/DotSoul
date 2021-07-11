using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using UnityEngine;

namespace RotaryHeart.Lib
{
    [CreateAssetMenu(fileName = "GundyrSkill.asset", menuName = "GundyrSkill DB")]
    public class GundyrSkill : ScriptableObject
    {


        [System.Serializable]
        public class ArrayTest
        {
            public int[] skillOrder;
        }

        [SerializeField]
        public SkillArray skillArr;

        [System.Serializable]
        public class SkillArray : SerializableDictionaryBase<int, ArrayTest> { }

    }
}
