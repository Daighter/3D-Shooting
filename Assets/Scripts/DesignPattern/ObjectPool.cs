using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern
{
    public class ObjectPool
    {
        private Stack<PooledObject> objectPool = new Stack<PooledObject>();
        private int poolSize = 100;

        public void CreatePool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                objectPool.Push(new PooledObject());
            }
        }

        public PooledObject GetPool()
        {
            if (objectPool.Count > 0)
                return objectPool.Pop();
            else
                return new PooledObject();
        }

        public void ReturnPool(PooledObject pooled)
        {
            objectPool.Push(pooled);
        }
    }

    public class PooledObject
    {
        // 생성&삭제가 빈번한 클래스
    }
}
