using System;
using System.Text;

namespace DecodingApp
{
    public class DecodeProgram
    {
        public static void Main()
        {
            Decoder decoder = new Decoder();

            Console.WriteLine("Enter the name of your encoded file: ");
            string encodedFilename = Console.ReadLine();
            Console.WriteLine("Enter the file format (txt, ini): ");
            string format = Console.ReadLine();
            string encodedFilePath = $"{encodedFilename}.{format}";

            string encodedContent = decoder.ReadFromFile(encodedFilePath).ToLower();
            Console.WriteLine($"Encoded Content: {encodedContent}");

            var parts = encodedContent.Split('|');
            if (parts.Length != 4)
            {
                Console.WriteLine("Invalid encoded content format.");
                return;
            }

            int method = int.Parse(parts[0]);
            string encodedSender = parts[1];
            string encodedReceiver = parts[2];
            string encodedText = parts[3];

            string decodedContent = decoder.Decode(encodedSender, encodedReceiver, encodedText, method).ToLower();

            string decodedFilename = $"decoded_{encodedFilename}.{format}";
            decoder.SaveToFile(decodedFilename, decodedContent);
            Console.WriteLine($"Decoded Text: {decodedContent} saved to {decodedFilename}");
        }
    }
}
