using UnityEngine;

public class LayerTriggerSquare : MonoBehaviour
{

    public string layer;
    public string sortingLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.name == "LayerTrigger2")
        {
            other.gameObject.layer = LayerMask.NameToLayer(layer);
            if (other.gameObject.GetComponent<SpriteRenderer>() != null)
                other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;
            SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in srs)
            {
                sr.sortingLayerName = sortingLayer;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.name == "LayerTrigger")
        {
            other.gameObject.layer = LayerMask.NameToLayer(layer);
            if (other.gameObject.GetComponent<SpriteRenderer>() != null)
                other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;
                
            SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in srs)
            {
                sr.sortingLayerName = sortingLayer;
            }
        }
    }

}

