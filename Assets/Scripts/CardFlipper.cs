using UnityEngine;
using System.Collections;

public class CardFlipper : MonoBehaviour 
{
    SpriteRenderer spriteRenderer;

    public AnimationCurve scaleCurve;
    public float duration = 0.5f;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlipCard(Sprite startImage, Sprite endImage)
    {
        StopCoroutine(Flip(startImage, endImage));
        StartCoroutine(Flip(startImage, endImage));
    }

    IEnumerator Flip(Sprite startImage, Sprite endImage)
    {
        spriteRenderer.sprite = startImage;

        float time = 0f;
        float originalScaleX = transform.localScale.x;
        Vector3 localScale = transform.localScale;
        while (time <= 1f)
        {
            float scale = scaleCurve.Evaluate(time) * originalScaleX;
            time += Time.deltaTime / duration;

            localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;

            if (time >= 0.5f)
            {
                spriteRenderer.sprite = endImage;
            }

            yield return new WaitForFixedUpdate();
        }

        localScale.x = originalScaleX; //back to normal size
        transform.localScale = localScale;

    }
}
