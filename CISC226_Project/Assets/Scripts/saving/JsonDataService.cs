using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDataService : IDataService
{
    public bool SavaData<T>(string RelativePath, T Data, bool Encrypted)
    {
        // where data will be saved
        string path = Application.persistentDataPath + RelativePath;

        try{
            // if file already exists, delete it so we can make a new one
            if (File.Exists(path)){
                    Debug.Log("Data exists, Deleting old file and writing a new one!");
                    // delete then create new file
                    File.Delete(path);
            } 
            else {
                Debug.Log("Creating file for the first time!");
            }

            // create file at path
            using FileStream stream = File.Create(path);
            // have to close or we cant write to it
            stream.Close();
            //write data to file
            File.WriteAllText(path, JsonConvert.SerializeObject(Data));
            return true;
        }
        // if anything goes wrong
        catch (Exception e){
            Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
            return false;
        }
    }
    public T LoadData<T>(string RelativePath, bool Encrypted)
    {
        throw new System.NotImplementedException();
    }

}
