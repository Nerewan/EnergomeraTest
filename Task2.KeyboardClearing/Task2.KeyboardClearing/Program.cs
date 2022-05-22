var inputs = new (string Expected, string Actual)[]
{
    ( "iamgoodcoder", "aimgoodrodec" ),
    ( "notsocool", "notsocool" ),
    ( "iamgoodcoder", "aimgcorooded" ),
};

foreach(var item in inputs)
{
    Console.WriteLine("-----------------------");
    Console.WriteLine($"Входные данные:\n\t{item.Expected}\n\t{item.Actual}");
    Console.WriteLine();
    var validationResult = ValidateStrings(item.Expected, item.Actual);
    Console.WriteLine($"Выходные данные:\n\t{validationResult.Count}\n\t{string.Join("\n\t", validationResult.Pairs.Select(e => $"{e.Key} {e.Value}"))}");


    Console.WriteLine();
}


Response ValidateStrings(string expected, string actual)
{
    var result = new Response();

    if(expected.Equals(actual))
        return result;    

    for(var i = 0; i < expected.Length; i++)
    {
        var expChar = expected[i];
        var actChar = actual[i];

        if(expChar.Equals(actChar) && result.Pairs.ContainsKey(expChar))
        {
            result.Count = -1;
            result.Pairs.Clear();

            return result;
        }

        if(!expChar.Equals(actChar))
        {
            if(result.Pairs.ContainsKey(expChar) && !result.Pairs[expChar].Equals(actChar))
            {
                result.Count = -1;
                result.Pairs.Clear();

                return result;
            }

            if(!result.Pairs.ContainsKey(expChar) && !result.Pairs.ContainsKey(actChar) && !result.Pairs.ContainsValue(actChar))
            {
                result.Pairs.Add(expChar, actChar);
                result.Count++;
            }
        }
    }

    return result;
}


class Response
{
    public int Count { get; set; }
    public Dictionary<char, char> Pairs { get; set; } = new Dictionary<char, char>();
}