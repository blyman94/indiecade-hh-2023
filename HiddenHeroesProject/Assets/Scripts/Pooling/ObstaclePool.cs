using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObstaclePool : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private Transform _parentTransform;
    public ObjectPool<GameObject> Pool;

    #region MonoBehaviour Methods
    private void Awake()
    {
        Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool,
            OnReturnedToPool, OnDestroyPoolObject, true, 50, 50);
    }
    #endregion

    private GameObject CreatePooledItem()
    {
        return Instantiate(_obstaclePrefab, _parentTransform);
    }

    private void OnTakeFromPool(GameObject poolObject)
    {
        poolObject.SetActive(true);
        poolObject.transform.parent = null;
    }

    private void OnReturnedToPool(GameObject poolObject)
    {
        poolObject.SetActive(false);
        poolObject.transform.parent = _parentTransform;
        poolObject.transform.position = _parentTransform.position;
    }

    private void OnDestroyPoolObject(GameObject poolObject)
    {
        Destroy(poolObject);
    }
}
