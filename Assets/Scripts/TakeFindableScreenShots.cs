using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TakeFindableScreenShots : MonoBehaviour
{
    [SerializeField] private int _width = 512;
    [SerializeField] private int _height = 512;
    [SerializeField] private Vector3 _itemPosition = new Vector3(0, 0f, 1f);

    [ContextMenu(nameof(TakeScreenShots))]
    public void TakeScreenShots()
    {
        if (Application.isPlaying) return;

        Camera camera = new GameObject("MyCamera").AddComponent<Camera>();
        camera.transform.position = new Vector3(40f, 40f, 40f);
        camera.transform.rotation = Quaternion.Euler(6f,0,0);
        camera.orthographic = false;

        List<Findable> allHiddenItems = GetComponentsInChildren<Findable>().ToList();

        foreach (Findable item in allHiddenItems)
        {
            Vector3 initialPosition = item.transform.position;
            item.transform.position = camera.transform.position + _itemPosition;

            RenderTexture rt = new RenderTexture(_width, _height, 24);
            camera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(_width, _height, TextureFormat.RGB24, false);
            camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, _width, _height), 0, 0);
            camera.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            DestroyImmediate(rt);
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName(item.name, _width, _height);
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));

            item.transform.position = initialPosition;
        }
        DestroyImmediate(camera);
    }

    public static string ScreenShotName(string name, int width, int height)
    {
        return string.Format("{0}/Resources/Sprites/{1}_{2}x{3}.png",
                             Application.dataPath, name,
                             width, height);
    }
}

