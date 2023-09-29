using UnityEngine;

public class DustCloud : MonoBehaviour
{
    SpriteRenderer dustTex;
    float opacity = 0f;
    bool isActive = false;
    readonly float step = 1f;
    // Start is called before the first frame update
    void Start()
    {
        dustTex = transform.gameObject.GetComponent<SpriteRenderer>();
    }

    public void ShowCloud()
    {
        isActive = true;
    }

    public void HideCloud()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (opacity < 1f)
            {
                opacity += step * Time.deltaTime;
                dustTex.material.color = new Color(1.0f, 1.0f, 1.0f, opacity);
            }
        }
        else
        {
            if (opacity > 0f)
            {
                opacity -= step * Time.deltaTime;
                dustTex.material.color = new Color(1.0f, 1.0f, 1.0f, opacity);
            }

        }
        transform.localScale = (Mathf.Abs(Mathf.Sin(8f * Time.time))) * 0.1f * Vector2.one + Vector2.one;
    }
}
