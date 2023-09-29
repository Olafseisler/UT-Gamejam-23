using System.Collections;
using UnityEngine;

// from https://gist.github.com/mstevenson/5103365
public class FPSDisplay : MonoBehaviour
{
    private float count;

    private IEnumerator Start()
    {
        if (!BuildConstants.isDebug) yield break;
        GUI.depth = 2;
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnGUI()
    {
        if (!BuildConstants.isDebug) return;
        Rect location = new(5, 5, 85, 25);
        string text = $"FPS: {Mathf.Round(count)}";
        Texture black = Texture2D.linearGrayTexture;
        GUI.DrawTexture(location, black, ScaleMode.StretchToFill);
        GUI.color = Color.black;
        GUI.skin.label.fontSize = 18;
        GUI.Label(location, text);
    }
}
