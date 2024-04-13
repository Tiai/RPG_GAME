using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Data;

public class FileDataHandler
{
    private string dataDirectionPath = "";
    private string dataFileName = "";

    private bool isEncrypt = false;
    private string key = "iamkey";

    public FileDataHandler(string _dataDirectionPath, string _dataFileName, bool _isEncrypt)
    {
        this.dataDirectionPath = _dataDirectionPath;
        this.dataFileName = _dataFileName;
        this.isEncrypt = _isEncrypt;
    }

    public void Save(GameData _data)
    {
        string fullPath = Path.Combine(this.dataDirectionPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(_data, true);

            if (isEncrypt)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }

            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error saving" + fullPath + "\n" + e);
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirectionPath, dataFileName);

        GameData loadData = null;

        if(File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (isEncrypt)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading" + fullPath + "\n" + e);
            }
        }

        return loadData;
    }

    public void Delete()
    {
        string fullPath = Path.Combine(dataDirectionPath, dataFileName);

        if(File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    private string EncryptDecrypt(string _data)
    {
        string modifiedData = "";

        for(int i = 0; i < _data.Length; ++i)
        {
            modifiedData += (char)(_data[i] ^ key[i % key.Length]);
        }

        return modifiedData;
    }
}
