using WordGuess.Models;

namespace WordGuess.Classes
{
    public class GetResponses
    {
        public static MessageResponse GetResponse (string value)
        {
            return new MessageResponse(){content = value};
        }
    }
}