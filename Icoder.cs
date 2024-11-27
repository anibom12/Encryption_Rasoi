using System;

public interface Icoder
{
    string Encode(string sender, string receiver, string text, int method);
    string Decode(string encodedSender, string encodedReceiver, string encodedText, int method);
}
