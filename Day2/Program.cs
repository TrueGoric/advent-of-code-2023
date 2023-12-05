using System.Text.RegularExpressions;

class Program
{
    private const string InputPath = @"input.txt";

    static void Main(string[] args)
    {
        var input = File.ReadAllLines(InputPath);
        var maximumPossibleRound = new Round(12, 13, 14);

        var sum = 0L;

        foreach (var line in input)
        {
            var game = Game.Parse(line);

            if (game.CheckIfPossible(maximumPossibleRound))
                sum += game.Id;
        }
        
        Console.WriteLine($"Sum: {sum}");
    }
}

class Game
{
    private static readonly Regex gameRegex =
        new Regex("^Game (?<Id>[0-9]+): (?<Round>[^;]+;??)+$", RegexOptions.Compiled);

    public int Id { get; set; }
    public List<Round> Rounds { get; init; }

    public bool CheckIfPossible(Round maximumPossible)
    {
        foreach (var round in Rounds)
        {
            if (!round.CheckIfPossible(maximumPossible))
                return false;
        }

        return true;
    }

    public static Game Parse(string input)
    {
        var gameMatch = gameRegex.Match(input);
        var id = int.Parse(gameMatch.Groups["Id"].Value);
        var rounds = new List<Round>();

        foreach (var capture in gameMatch.Groups["Round"].Captures.AsEnumerable())
        {
            rounds.Add(Round.Parse(capture.Value));
        }

        return new Game
        {
            Id = id,
            Rounds = rounds
        };
    }
}

record Round(uint Red, uint Green, uint Blue)
{
    private static readonly Regex roundRegex =
        new Regex(@"^\s?((?<Set>\d+ (red|green|blue)),?\s?)+;?$", RegexOptions.Compiled);

    public bool CheckIfPossible(Round maximumPossible)
        => Red <= maximumPossible.Red && Green <= maximumPossible.Green && Blue <= maximumPossible.Blue;

    public static Round Parse(string input)
    {
        var roundMatch = roundRegex.Match(input);
        uint? red = null, green = null, blue = null;

        foreach (var capture in roundMatch.Groups["Set"].Captures.AsEnumerable())
        {
            if (capture.Value.EndsWith("blue"))
            {
                blue = uint.Parse(capture.Value.Replace(" blue", ""));
            }
            else if (capture.Value.EndsWith("green"))
            {
                green = uint.Parse(capture.Value.Replace(" green", ""));
            }
            else if (capture.Value.EndsWith("red"))
            {
                red = uint.Parse(capture.Value.Replace(" red", ""));
            }
            else
            {
                Console.WriteLine($"Invalid round! \"{input}\"");
            }
        }

        return new Round(red ?? 0, green ?? 0, blue ?? 0);
    }
}