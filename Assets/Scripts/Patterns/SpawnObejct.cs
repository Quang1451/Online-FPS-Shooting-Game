using UnityEngine;
using UnityEngine.Pool;

public class SpawnObejct<T> where T : MonoBehaviour
{
    private T Prefabs;

    public ObjectPool<T> Pool;

    public virtual void Initalize(T prefab)
    {
        Prefabs = prefab;
        Pool = new ObjectPool<T>(CreateObject, OnTakeObjectFormPool, OnReturnObjectToPool, OnDestroyObject, false, 10, 100);
    }

    public virtual T CreateObject()
    {
        T instance = MonoBehaviour.Instantiate(Prefabs);
        return instance;
    }

    public virtual void OnTakeObjectFormPool(T instance)
    {
        instance.gameObject.SetActive(true);
    }

    public virtual void OnReturnObjectToPool(T instance)
    {
        instance.gameObject.SetActive(false);
    }

    public virtual void OnDestroyObject(T instance)
    {
        MonoBehaviour.Destroy(instance.gameObject);
    }

    public T GetObject()
    {
        return Pool.Get();
    }

    public void ReleaseObject(T instance)
    {
        Pool.Release(instance);
    }
}
