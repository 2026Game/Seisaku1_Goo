using UnityEngine;

public class KeepChildScale : MonoBehaviour
{
    public Transform child;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = child.localScale;
    }

    void LateUpdate()
    {
        Vector3 parentScale = transform.lossyScale;

        child.localScale = new Vector3(
            originalScale.x / parentScale.x,
            originalScale.y / parentScale.y,
            originalScale.z / parentScale.z
        );
    }
}
