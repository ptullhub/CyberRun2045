using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.1f;
    [SerializeField] private Renderer layerRenderer;

    private float offset;
    private bool isScrolling = true;

    private void Start()
    {
        GameManager.GameInstance.onGameOver.AddListener(StopScrolling);
    }
    void Update()
    {
        if (isScrolling)
        {
            offset += Time.deltaTime * scrollSpeed;
            layerRenderer.material.mainTextureOffset = new Vector2(offset, 0f);
        }

        
    }

    public void StopScrolling()
    {
        isScrolling = false;
    }

    public void ChangeScrollSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }
}
