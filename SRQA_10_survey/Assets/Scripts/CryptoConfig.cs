
using UnityEngine;
[System.Serializable]
public class CryptoConfig : MonoBehaviour
{
    private string key = "";
    private string iv = "LSL44wiKcYCPFaGvF3cfhw==";

    public  string GetKey()
    { return key; }
    public void SetKey(string key)
    { this.key = key; }
    public  string GetIv()
    { return iv; }
}
