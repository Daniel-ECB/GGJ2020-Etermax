using UnityEngine;
public abstract class PoolObject : MonoBehaviour, IPooling {
    public bool IsUsing => go.activeSelf;
    private GameObject go;

    public void Init() {
        go = gameObject;
    }

    public virtual void OnGet() {
        go.SetActive(true);
    }

    public virtual void OnRelease() {
        go.SetActive(false);
    }
}
