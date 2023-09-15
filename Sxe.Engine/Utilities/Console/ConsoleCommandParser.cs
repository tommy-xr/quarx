using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.Utilities.Console
{
    /// <summary>
    /// This class encapsulates the functionality of parsing a console string
    /// It is in a class (as opposed to a function of ConsoleComponent) to easily
    /// allow different strategies for parsing, as well as to easily unit test
    /// </summary>
    public class ConsoleCommandParser
    {
        List<string> stringList = new List<string>();

        /// <summary>
        /// Tokenizes a string
        /// </summary>
        public virtual string[] ConsoleTokenize(string command)
        {
            stringList.Clear();

            //Loop through, and extract all quoted items
            while(command.Contains("\""))
            {
                int quoteIndex = command.IndexOf('"');

                //Extract everything before the quote, and split it
                string before = command.Substring(0, quoteIndex);

                //Split the before part, and add all items to the list
                before = before.Trim();
                if (before.Length > 0)
                {
                    string[] tokens = before.Split(' ');

                    //Add all tokens to string list
                    for (int i = 0; i < tokens.Length; i++)
                        stringList.Add(tokens[i]);
                }

                //Now, see if there is another quotation
                int nextQuoteIndex = command.IndexOf('"', quoteIndex+1);

                //Take the substring between quote index and next quote index, and add to list
                if (nextQuoteIndex >= 0 && nextQuoteIndex < command.Length)
                {
                    string subString = command.Substring(quoteIndex + 1, nextQuoteIndex - quoteIndex - 1);
                    stringList.Add(subString);

                    command = command.Substring(nextQuoteIndex + 1, command.Length - nextQuoteIndex - 1);
                }
                else
                {
                    command = command.Substring(quoteIndex, command.Length - quoteIndex);
                }

            }

            string[] remainingTokens = command.Split(' ');
            for (int i = 0; i < remainingTokens.Length; i++)
                stringList.Add(remainingTokens[i]);

            return stringList.ToArray();
        }
    }
}
