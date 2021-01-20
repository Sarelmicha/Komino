using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PersistenceDataManager : MonoBehaviour
{


    public void SaveImageToDisk(string imgUrl ,Texture2D textImg)
    {
        try
        {
            string filePath = GetVersionedPersistentDataPath() + imgUrl;
            string directory = filePath.Substring(0, filePath.LastIndexOf("/"));

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            print("filePath is " + filePath);
            print("directory name is " + directory);
            print("image format is " + textImg.format + "and filePathName is " + filePath);


            // Convert the compressed textures if needed, since we cannot save DXT compressed with EncodeTo functions
            // If not compressed save as is
            if (textImg.format == TextureFormat.DXT1)
            {
                print("im here1");
                // JPG
                Texture2D newTexture = new Texture2D(textImg.width, textImg.height, TextureFormat.RGB24, false);
                newTexture.SetPixels(textImg.GetPixels(0), 0);
                File.WriteAllBytes(filePath, newTexture.EncodeToJPG());
                Destroy(newTexture);
            }
            else if (textImg.format == TextureFormat.DXT5)
            {
                // PNG
                print("im here2");
                Texture2D newTexture = new Texture2D(textImg.width, textImg.height, TextureFormat.RGBA32, false);
                newTexture.SetPixels(textImg.GetPixels(0), 0);
                File.WriteAllBytes(filePath, newTexture.EncodeToPNG());
                Destroy(newTexture);
            }
            else if (textImg.format == TextureFormat.RGB24)
            {
                print("im here3");
                File.WriteAllBytes(filePath, textImg.EncodeToJPG());
            }
            else if (textImg.format == TextureFormat.ARGB32 || textImg.format == TextureFormat.RGBA32)
            {
                print("im here4");
                File.WriteAllBytes(filePath, textImg.EncodeToPNG());
            }           
        }
        catch (Exception e)
        {
            // Try to delete the cache dir, since it might be full
            try
            {
                Debug.LogError("error = " + e);
                CleanOldFiles("dataFiles", 0);
            }
            catch (Exception e1)
            {

                Debug.LogError("cannot clean old data files: " + e1);
            }
        }

    }

    public void SaveImagesToDisk(GameObject bundle)
    {
        CharacterBundle texturesBundle = bundle.GetComponent<CharacterBundle>();
        int i = 0;
        foreach (Texture2D texture in texturesBundle.characterUpgradesTextures)
        {
            print("num of iteration is " + i + " and name of texture is " + texture.name);
            i++;
            SaveImageToDisk(texture.name, texture);
        }      
    }

    public static (Sprite, Texture2D) LoadImageFromDisk(string imgUrl)
    {
        Debug.Log("Requesting image load from DISK: " + imgUrl);

        string filePath = GetVersionedPersistentDataPath() + imgUrl;
        byte[] bytes = File.ReadAllBytes(filePath);

        Texture2D texture = null;
        if (imgUrl.EndsWith("png"))
        {
            texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        }
        else
        {
            texture = new Texture2D(1, 1, TextureFormat.RGB24, false);
        }

        texture.LoadImage(bytes, true);
        print("texture after load is " + texture);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        return (sprite, texture);
    }



    private static string GetVersionedPersistentDataPath()
    {
        return Application.persistentDataPath + "/";
    }

    private void CleanOldFiles(string folder_uri, int days_to_keep)
    {
        print("im inside CleanOldFiles");
        string directory = GetVersionedPersistentDataPath() + folder_uri;

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        string[] dirPaths = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

        DateTime modification;
        TimeSpan delta;

        for (int i = 0; i < dirPaths.Length; i++)
        {
            modification = File.GetLastWriteTime(dirPaths[i]);

            delta = DateTime.Now - modification;

            if (delta.TotalDays > days_to_keep)
            {
                File.Delete(dirPaths[i]);
            }

        }
    }
}
