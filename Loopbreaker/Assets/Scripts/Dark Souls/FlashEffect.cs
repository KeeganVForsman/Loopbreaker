using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    public float flashDuration = 0.1f;

    private Renderer rend;
    private MaterialPropertyBlock propBlock;
    private Color originalColor;

    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
        if (rend == null)
        {
            Debug.LogWarning("FlashEffect: No Renderer found on " + name);
            return;
        }

        propBlock = new MaterialPropertyBlock();
        rend.GetPropertyBlock(propBlock);
        originalColor = rend.material.color;
    }

    public void FlashWhite()
    {
        if (rend == null) return;

        StopAllCoroutines();
        StartCoroutine(FlashCoroutine());
    }

    private System.Collections.IEnumerator FlashCoroutine()
    {
        propBlock.SetColor("_BaseColor", Color.white);
        rend.SetPropertyBlock(propBlock);

        yield return new WaitForSeconds(flashDuration);

        propBlock.SetColor("_BaseColor", Color.white);
        rend.SetPropertyBlock(propBlock);
    }
}