using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadImage : MonoBehaviour
{
    public RawImage rawImage;

    void Start()
    {

    }

    public void UpdateImage()
    {
        if (NativeGallery.IsMediaPickerBusy())
            return;

        PickImage(512);
    }

    private void PickImage(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                Destroy(rawImage.texture);

                rawImage.texture = texture;
            }
        });
        Debug.Log("Permission result: " + permission);
    }
}