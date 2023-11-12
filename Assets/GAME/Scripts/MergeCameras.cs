using UnityEngine;

public class MergeCameras : MonoBehaviour
{
    public Camera player1Camera;
    public Camera player2Camera;
    public RenderTexture player1Texture;
    public RenderTexture player2Texture;
    public Material mergeMaterial; // 병합을 위한 재질

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // Player 1 카메라의 Render Texture를 화면 왼쪽에 그립니다.
        Graphics.Blit(player1Texture, dest, mergeMaterial);

        // Player 2 카메라의 Render Texture를 화면 오른쪽에 그립니다.
        Graphics.Blit(player2Texture, dest, mergeMaterial);
    }
}
