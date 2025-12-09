using UnityEngine;
[System.Serializable]
public class ERDACredentials : MonoBehaviour
{
    private string host;
    private int port;
    private string username;
    private string password;
    private string remotePath;

    public string Host
    {
        get { return host; }
        set { host = value; }
    }
    public int Port
    {
        get { return port; }   
        set { port = value; }
    }
    public string Username
    {
        get { return username; }
        set { username = value; }
    }
    public string Password
    {
        get { return password; }
        set { password = value; }
    }
    public string RemotePath
    {
        get { return remotePath; }
        set { remotePath = value; }
    }

}
