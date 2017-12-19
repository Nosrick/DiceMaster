using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SpriteLoader
{
    public static void Load()
    {
        string dir = Directory.GetCurrentDirectory() + "/Assets/Resources/Sprites";
        string[] files = Directory.GetFiles(dir, "*.png", SearchOption.AllDirectories);

        Sprites = new Dictionary<string, Sprite>();

        foreach(string file in files)
        {
            string fileName = file.Substring(file.LastIndexOf("\\") + 1);
            fileName = fileName.Substring(0, fileName.Length - 4);

            Sprite material = Resources.Load<Sprite>("Sprites/" + fileName);
            Sprites.Add(material.name, material);
        }
    }

    public static Sprite Get(string name)
    {
        if(Sprites.ContainsKey(name))
        {
            return Sprites[name];
        }

        return new Sprite();
    }

    public static Dictionary<string, Sprite> Sprites
    {
        get;
        private set;
    }
}