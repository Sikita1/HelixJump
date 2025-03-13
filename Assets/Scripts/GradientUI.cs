using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GradientUI : MonoBehaviour
{
    [SerializeField] private Color _color1;
    [SerializeField] private Color _color2;

    [SerializeField] private bool _horizontalGradient;

    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
        ApplyGradient();
    }

    private void ApplyGradient()
    {
        if (_image.sprite == null)
        {
            Debug.LogError("Спрайт не назначен на Image!");
            return;
        }

        Texture2D texture = _image.sprite.texture;
        Texture2D gradientTexture = new Texture2D(texture.width, texture.height);

        Color[] originalPixels = texture.GetPixels();
        gradientTexture.SetPixels(originalPixels);

        for (int x = 0; x < gradientTexture.width; x++)
        {
            for (int y = 0; y < gradientTexture.height; y++)
            {
                float t = _horizontalGradient ? (float)x / gradientTexture.width : (float)y / gradientTexture.height;
                Color gradientColor = Color.Lerp(_color1, _color2, t);

                Color originalColor = gradientTexture.GetPixel(x, y);
                Color finalColor = originalColor * gradientColor;
                gradientTexture.SetPixel(x, y, finalColor);
            }
        }

        gradientTexture.Apply();

        Sprite gradientSprite = Sprite.Create(gradientTexture, new Rect(0, 0, gradientTexture.width, gradientTexture.height), Vector2.zero);
        _image.sprite = gradientSprite;
    }
}