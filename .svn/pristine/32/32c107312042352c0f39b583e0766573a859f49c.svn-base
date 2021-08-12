using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneData : Setting<SceneData>
{
    public static int sceneIndex = -1;
    public static Vector3 respawnPosition;
    public static bool isFirstPlay{ get{ return sceneIndex == -1; } }
    public static Vector3[] positions = new Vector3[]{
        new Vector3(0f,0f,0f),
    };

    public static Vector3 GetSceneFirstRespawnPosition(int index)
    {
        if(index>=0 && index<positions.Length)
        {
            return positions[index];
        }
        else 
        {
            return Vector3.zero;
        }
    }

    public static void StoreSceneInfo(int index,Vector3 position)
    {
        if(SceneManager.GetSceneByBuildIndex(index)==null) { throw new System.Exception("No such scene in index("+index+")!!"); }
        sceneIndex = index;
        respawnPosition = position;
        print("Store SceneInfo: index="+index+" position="+position);
    }

    public static void StoreSceneInfo(int index)
    {
        StoreSceneInfo(index,GetSceneFirstRespawnPosition(index));
    }

    public static void StoreSceneInfo(Vector3 position)
    {
        var scene = SceneManager.GetActiveScene();
        StoreSceneInfo(scene.buildIndex,position);
    }

    public override void Load()
    {
        if(File.Exists(STRING.Path.SceneData))
        {
            var reader = new BinaryReader(File.OpenRead(STRING.Path.SceneData));
            sceneIndex = reader.ReadInt32();
            respawnPosition.x = reader.ReadInt32();
            respawnPosition.y = reader.ReadInt32();
            respawnPosition.z = reader.ReadInt32();
            reader.Close();
        }
    }
    public override void Save()
    {
        var writer = new  BinaryWriter(Utility.UFile.OpenFile(STRING.Path.SceneData));
        writer.Write(sceneIndex);
        writer.Write(respawnPosition.x);
        writer.Write(respawnPosition.y);
        writer.Write(respawnPosition.z);
        writer.Close();
    }
}
