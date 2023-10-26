using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathOfExile3.Runtime
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private List<char> _chars;
        private IEnumerator Start()
        {
            char a = '0';
            var next = (char)(a + 1);

            _chars = new List<char>()
            {
                a, next
            };

            while (next != a)
            {
                next++;
                _chars.Add(next);
                
                yield return null;
            }
        }
    }
}