using UnityEngine;

public class Destructible : MonoBehaviour
{
    public string destructibleTag = "Destructible";
    public string destructibleLayerName = "Destructible";

    private int destructibleLayer;

    void Start()
    {
        // Cache the layer number to avoid repeated lookups
        destructibleLayer = LayerMask.NameToLayer(destructibleLayerName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(destructibleTag) || other.gameObject.layer == destructibleLayer)
        {
            Destroy(other.gameObject);
            Debug.Log("Destroyed: " + other.name);
        }
    }
}