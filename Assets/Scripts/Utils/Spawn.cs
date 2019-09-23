using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class Spawn : MonoBehaviour
    {
        [SerializeField]
        public List<GameObject> prefabs;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnPrefabs());
        }

        IEnumerator SpawnPrefabs()
        {
            foreach(var gob in prefabs)
            {
                GameObject.Instantiate(gob, transform);
                yield return null;
            }
            yield break;
        }

    }
}