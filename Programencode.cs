using System;

namespace EncodingApp
{
    public class EncodeProgram
    {
        public static void Main()
        {
            Encoder encoder = new Encoder();

            Console.WriteLine("Enter the name of sender: ");
            string sender = Console.ReadLine();
            Console.WriteLine("Enter the name of receiver: ");
            string receiver = Console.ReadLine();
            Console.WriteLine("Enter your text: ");
            string text = Console.ReadLine();
            Console.WriteLine("Select encoding method (1/2): ");
            int method = int.Parse(Console.ReadLine());

            string encodedText;
            if (method == 1)
            {
                encodedText = encoder.EncodeMethod1(sender, receiver, text).ToLower();
            }
            else
            {
                encodedText = encoder.EncodeMethod2(sender, receiver, text).ToLower();
            }

            Console.WriteLine("Enter the name of your file: ");
            string filename = Console.ReadLine();
            Console.WriteLine("Enter the file format (txt, ini): ");
            string format = Console.ReadLine();
            string filePath = $"{filename}.{format}";

            encoder.SaveToFile(filePath, encodedText);
            Console.WriteLine($"Encoded Text: {encodedText} saved to {filePath}");
        }
    }
}
