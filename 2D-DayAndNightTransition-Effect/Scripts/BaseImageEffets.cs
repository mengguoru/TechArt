using UnityEngine;
[ExecuteInEditMode]
public class BaseImageEffets : MonoBehaviour {
	public Material mat;

	void OnRenderImage(RenderTexture src,RenderTexture dst)
	{
		Graphics.Blit(src,dst,mat);
	}
}
