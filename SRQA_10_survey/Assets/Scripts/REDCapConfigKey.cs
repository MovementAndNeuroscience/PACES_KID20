using UnityEngine;

public class REDCapConfigKey : MonoBehaviour
{
    //The key is always scabled
    private string apiKey = "50384I86I35452I27713FF4F957F5647";
    
    public string GetApiKey()
    { return apiKey; }
    public void SetApiKey(string apikey)
        { this.apiKey = apikey; }

    public string shiftLetters(string letters, int shiftamount)
    {
            int shift = shiftamount; // Shift each letter by amount
            string newLetters = "";

            foreach (char c in letters)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    char shifted = (char)(((c - offset + shift) % 26) + offset);
                    newLetters += shifted;
                }
                else
                {
                    newLetters += c; // Leave non-letters as is
                }
            }
            return newLetters;
    }

}
