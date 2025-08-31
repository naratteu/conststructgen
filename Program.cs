using Main;

var sample = new Sample { head = 1, tail = 2 };

var type = sample switch
{
    Sample(Sample.TYPE1) => "Type1",
    Sample(Sample.TYPE2) => "Type2",
    _ => "Unknown"
};

Console.WriteLine("Sample is " + type);