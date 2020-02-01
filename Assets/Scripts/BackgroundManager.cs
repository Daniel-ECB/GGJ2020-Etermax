using UnityEngine;

public class BackgroundManager : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private float boundaryLeft = default;
    [SerializeField] private float boundaryRight = default;
    [SerializeField] private float speed = default;

    [Header("References")]
    [SerializeField] private Transform nebule = default;

    private void Update() {
        nebule.Translate(new Vector3(Time.deltaTime * speed, 0));

        if (nebule.position.x >= boundaryRight) {
            nebule.position = new Vector3(boundaryLeft, 0);
        }
    }
}