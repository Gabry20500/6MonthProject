using UnityEngine;

public class FadeMaterial : MonoBehaviour
{
    [SerializeField] SpriteRenderer _renderer;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        _renderer.material.SetTexture("_MainTex", _renderer.sprite.texture);
    }
}
