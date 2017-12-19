using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DiceFaceLoader
{
    private static string DIR = Directory.GetCurrentDirectory() + "/Assets/Resources/Dice/Definitions";

    public static void Load()
    {
        string[] files = Directory.GetFiles(DIR, "*.json", SearchOption.AllDirectories);

        Faces = new Dictionary<string, DiceFace>();

        foreach(string file in files)
        {
            StreamReader reader = new StreamReader(file);

            string content = reader.ReadToEnd();
            FaceInfo first = JsonUtility.FromJson<FaceInfo>(content);
            if(first.Face == FaceType.Play)
            {
                PlayFace playFace = new PlayFace(first);
                Faces.Add(playFace.Name, playFace);
            }
            else if(first.Face == FaceType.Special)
            {
                SpecialFace specialFace = new SpecialFace(first);
                Faces.Add(specialFace.Name, specialFace);
            }

            reader.Close();
        }
    }

    public static void Serialise(DiceFace faceRef)
    {
        FaceInfo info = new FaceInfo(faceRef);

        string content = JsonUtility.ToJson(info, true);

        StreamWriter writer = new StreamWriter(DIR + "/" + faceRef.Name + "-" + faceRef.Level + ".json");
        writer.Write(content);
        writer.Flush();
        writer.Close();
    }

    public static DiceFace Get(string name)
    {
        if(Faces.ContainsKey(name))
        {
            return Faces[name];
        }
        return new PlayFace(1, 1, 1, FaceType.Play, Color.black, Color.magenta, "DEFAULT-1", SpriteLoader.Get("DebugCat"));
    }

    public static Dictionary<string, DiceFace> Faces
    {
        get;
        private set;
    }
}