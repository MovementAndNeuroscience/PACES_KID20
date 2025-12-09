
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Renci.SshNet;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections;

[System.Serializable]
public class FromRedCapConfig
{
    public string redcap_api_key;
    public string erda_connection;
    public string decrypt_key;
}
[System.Serializable]
public class SftpCredentials
{
    public string Host;
    public int Port;
    public string Username;
    public string Password;
    public string RemotePath;
}
public class ConfigureAndEstablishConnection : MonoBehaviour

{
    public GameObject config;
    public GameObject redcapCom;
    private CryptoConfig cryptoConfig;
    private REDCapConfigKey redCapConfig;
    private REDCapN2BackKey redCapN2BackKey;
    private ERDACredentials erdaConfig;

    void Start()
    {
        cryptoConfig = config.GetComponent<CryptoConfig>();
        redCapConfig = config.GetComponent<REDCapConfigKey>();  
        redCapN2BackKey = config.GetComponent<REDCapN2BackKey>();
        erdaConfig = config.GetComponent<ERDACredentials>();
        var apikey = redCapConfig.GetApiKey();
        var unscrablekey = redCapConfig.shiftLetters(apikey, -3);
        redCapConfig.SetApiKey(unscrablekey);
        StartCoroutine(Coroutinewrapper());
    }

    public IEnumerator Coroutinewrapper()
    {
        yield return StartCoroutine(redcapCom.GetComponent<REDCapCommunicator>().GetConfig(redCapConfig.GetApiKey()));

        var content = redcapCom.GetComponent<REDCapCommunicator>().restResponse;

        content = content.Trim('[');
        content = content.Trim(']');
        var fromREDCapConfig = JsonUtility.FromJson<FromRedCapConfig>(content);
        cryptoConfig.SetKey(fromREDCapConfig.decrypt_key);
        redCapN2BackKey.SetApiKey(fromREDCapConfig.redcap_api_key);

        //enable again if the program is running on a computer and we need files on ERDA. 
        //var credentials = AesDecryption.LoadEncryptedConfig(fromREDCapConfig.erda_connection, cryptoConfig);
        //erdaConfig.Host = credentials.Host;
        //erdaConfig.Port = credentials.Port;
        //erdaConfig.Username = credentials.Username;
        //erdaConfig.Password = credentials.Password;

        //var remotePath = credentials.RemotePath;
        //remotePath = remotePath + $"data_{DateTime.UtcNow:yyyyMMdd_HHmmssfff}.csv";
        //erdaConfig.RemotePath = remotePath;

        //erdaConfig.RemotePath = credentials.RemotePath;
    }
}

public static class AesDecryption
{
    public static string Decrypt(string cipherText, CryptoConfig config)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(config.GetKey());
            aes.IV = Convert.FromBase64String(config.GetIv());

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream ms = new(Convert.FromBase64String(cipherText));
            using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader sr = new(cs);
            return sr.ReadToEnd();
        }
    }
    public static SftpCredentials LoadEncryptedConfig(string ciphertext, CryptoConfig config)
    {
        string json = Decrypt(ciphertext, config);
        return JsonUtility.FromJson<SftpCredentials>(json);
    }
  
}



