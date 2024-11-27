using System;
using System.IO;
using System.Linq;
using System.Text;

public class Decoder : Icoder
{
    private readonly char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    private int CalculateShift(string shift)
    {
        return shift.ToUpper().Where(c => alphabet.Contains(c)).Sum(c => Array.IndexOf(alphabet, c)) % alphabet.Length;
    }

    private string ShiftText(string text, int shift)
    {
        StringBuilder result = new StringBuilder();
        foreach (char c in text.ToUpper())
        {
            if (alphabet.Contains(c))
            {
                int indexShift = (Array.IndexOf(alphabet, c) + shift) % alphabet.Length;
                if (indexShift < 0) indexShift += alphabet.Length;
                result.Append(alphabet[indexShift]);
            }
            else
            {
                result.Append(c);
            }
        }
        return result.ToString();
    }

    public string DecodeMethod1(string encodedSender, string encodedReceiver, string encodedText)
    {
        int senderShift = CalculateShift(encodedSender);
        int receiverShift = CalculateShift(encodedReceiver);
        int totalShift = (senderShift + receiverShift) % alphabet.Length;
        string decodedSender = ShiftText(encodedSender, -totalShift);
        string decodedReceiver = ShiftText(encodedReceiver, -totalShift);
        string decodedText = ShiftText(encodedText, -totalShift);
        return $"{decodedSender}|{decodedReceiver}|{decodedText}";
    }

    public string DecodeMethod2(string encodedSender, string encodedReceiver, string encodedText)
    {
        int senderShift = CalculateShift(encodedSender);
        int receiverShift = CalculateShift(encodedReceiver);

        if (receiverShift == 0)
        {
            throw new ArgumentException("Receiver shift should not be zero to avoid division by zero.");
        }

        int totalShift = (senderShift - receiverShift) * (senderShift / receiverShift);
        string decodedSender = ShiftText(encodedSender, -totalShift);
        string decodedReceiver = ShiftText(encodedReceiver, -totalShift);
        string decodedText = ShiftText(encodedText, -totalShift);
        return $"{decodedSender}|{decodedReceiver}|{decodedText}";
    }

    public string Encode(string sender, string receiver, string text, int method)
    {
        throw new NotImplementedException("Use specific encoding methods instead.");
    }

    public string Decode(string encodedSender, string encodedReceiver, string encodedText, int method)
    {
        if (method == 1)
        {
            return DecodeMethod1(encodedSender, encodedReceiver, encodedText);
        }
        else
        {
            return DecodeMethod2(encodedSender, encodedReceiver, encodedText);
        }
    }

    public string ReadFromFile(string filename)
    {
        string extension = Path.GetExtension(filename);
        if (extension == ".txt")
        {
            return File.ReadAllText(filename);
        }
        else if (extension == ".ini")
        {
            string[] lines = File.ReadAllLines(filename);
            return lines.FirstOrDefault(line => line.StartsWith("value="))?.Split('=')[1];
        }
        return string.Empty;
    }

    public void SaveToFile(string filename, string text)
    {
        string extension = Path.GetExtension(filename);
        if (extension == ".txt")
        {
            File.WriteAllText(filename, text);
        }
        else if (extension == ".ini")
        {
            File.WriteAllText(filename, $"[Text]\nvalue={text}");
        }
    }
}
