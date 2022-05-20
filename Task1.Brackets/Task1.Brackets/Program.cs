var items = new List<(string text, bool expResult)>() 
{ 
    ("(((){}[]]]])(", false), 
    ("(){}[][][]()", true), 
    ("({[]})()[]", true) 
};

foreach(var item in items)
{
    try
    {
        Console.WriteLine($"{item.text}:\n\tExpected: {item.expResult}\n\tActual: {ValidateBrackets(item.text)}");
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}



bool ValidateBrackets(string text)
{
    if (text == null)
        throw new ArgumentNullException($"{nameof(text)} should not be NULL");

    if(text.Length == 0)
        return true;

    var bracketsPairs = new Dictionary<char, char>()
    {
        {'(', ')'},
        {'{', '}'},
        {'[', ']'}
    };
    var stack = new Stack<char>();

    foreach(var c in text)
    {
        if(bracketsPairs.ContainsValue(c) && (stack.Count == 0 || !bracketsPairs[stack.Pop()].Equals(c)))
            return false;

        if(bracketsPairs.ContainsKey(c))
            stack.Push(c);
    }

    return true;
}