using UnityEngine;
using System.Collections.Generic;

namespace MissileCommander
{
    public class Factory
    {
        List<RecyclableObject> pool = new List<RecyclableObject>();
        private readonly int _defaultPoolSize;
        private readonly RecyclableObject _prefab;

        public Factory(RecyclableObject prefab, int defaultPoolSize = 5)
        {
            this._prefab = prefab;
            this._defaultPoolSize = defaultPoolSize;
            
            Debug.Assert(this._prefab != null, "Factory : Prefab is null!");
        }

        private void CreatePool()
        {
            for (int i = 0; i < _defaultPoolSize; i++)
            {
                RecyclableObject obj = Object.Instantiate<RecyclableObject>(_prefab);
                obj.gameObject.SetActive(false);
                pool.Add(obj);
            }
        }

        public RecyclableObject Get()
        {
            if (pool.Count == 0)
            {
                CreatePool();
            }

            int lastIndex = pool.Count - 1;
            RecyclableObject obj = pool[lastIndex];
            obj.gameObject.SetActive(true);
            pool.RemoveAt(lastIndex);

            return obj;
        }

        public void ReturnToPool(RecyclableObject obj)
        {
            Debug.Assert(obj != null, "Factory : obj to return is null!");
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }
}