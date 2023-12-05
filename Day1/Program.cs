using System.Text.RegularExpressions;

namespace Day1;

class Program
{
    private const string InputPath = @"input.txt";
    
    static void Main(string[] args)
    {
        var input = File.ReadAllLines(InputPath);
        var sum = 0L;
        
        Span<char> buf = stackalloc char[2];

        foreach (var line in input)
        {
            var parsedLine = SubstituteArtfulDigits(line);

            buf.Clear();

            foreach (var character in parsedLine)
            {
                if (char.IsDigit(character))
                {
                    if (buf[0] is default(char))
                    {
                        buf[0] = character;
                        buf[1] = character;
                    }
                    else
                    {
                        buf[1] = character;
                    }
                }
            }

            if (buf[0] is default(char))
            {
                Console.WriteLine($"Invalid line! \"{line}\" (parsed: \"{parsedLine}\")");

                continue;
            }

            var add = buf[1] switch
            {
                default(char) => long.Parse(buf[..1]),
                _ => long.Parse(buf)
            };

            sum += add;
        }

        Console.WriteLine($"Sum: {sum}");
    }

    static string SubstituteArtfulDigits(string input)
    {
        var buffer = new char[input.Length];
        var span = input.AsSpan();

        var i = 0;
        var j = 0;
        
        while (i < span.Length)
        {
            if (span[i..].StartsWith("one"))
            {
                buffer[j++] = '1';
            }
            else if (span[i..].StartsWith("two"))
            {
                buffer[j++] = '2';
            }
            else if (span[i..].StartsWith("three"))
            {
                buffer[j++] = '3';
            }
            else if (span[i..].StartsWith("four"))
            {
                buffer[j++] = '4';
            }
            else if (span[i..].StartsWith("five"))
            {
                buffer[j++] = '5';
            }
            else if (span[i..].StartsWith("six"))
            {
                buffer[j++] = '6';
            }
            else if (span[i..].StartsWith("seven"))
            {
                buffer[j++] = '7';
            }
            else if (span[i..].StartsWith("eight"))
            {
                buffer[j++] = '8';
            }
            else if (span[i..].StartsWith("nine"))
            {
                buffer[j++] = '9';
            }
            else
            {
                buffer[j++] = span[i];
            }

            i++;
        }

        var result = buffer[..j].AsSpan().ToString()!;

        return result;
    }
}