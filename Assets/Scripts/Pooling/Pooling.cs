using System.Collections.Generic;
using UnityEngine;

public class Pooling<T> where T : MonoBehaviour, IPooling {
    public bool createMoreIfNeeded = true;

    private Transform mParent;
    private T referenceObject;

    private int count;

    private LinkedList<T> objList;

    public Pooling(int amount, T refObject, Transform parent, bool startState = false) {
        count = 0;
        objList = new LinkedList<T>();
        mParent = parent;
        referenceObject = refObject;

        for (int i = 0; i < amount; i++) {
            T obj = CreateObject();

            if (startState) {
                obj.OnGet();
            } else {
                obj.OnRelease();
            }

            objList.AddLast(obj);
        }
    }

    public T Get() {
        T obj = objList.First?.Value;
        if (obj is null && createMoreIfNeeded) {
            obj = CreateObject();
            obj.OnGet();
            return obj;
        }

        if (obj is null) return obj;

        objList.RemoveFirst();
        obj.OnGet();
        return obj;
    }

    public void Release(T obj) {
        if (obj != null) {
            obj.OnRelease();
            objList.AddLast(obj);
        }
    }

    private T CreateObject() {
        T obj = Object.Instantiate(referenceObject, mParent, false);
        obj.Init();
        obj.name = count.ToString();
        count++;
        return obj;
    }
}