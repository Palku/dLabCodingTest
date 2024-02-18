using System.Text;

namespace CustomPeopleInput.Test;

public class PeopleInputTests
{
    [Fact]
    public void InitialSmokeTest()
    {
        var input = new PeopleInput();
        var dataStream
            = new MemoryStream(
                "P|Carl Gustaf|Bernadotte\nT|0768-101801|08-101801\nA|Drottningholms slott|Stockholm|10001\nF|Victoria|1977\nA|Haga Slott|Stockholm|10002\nF|Carl Philip|1979\nT|0768-101802|08-101802\nP|Barack|Obama\nA|1600 Pennsylvania Avenue|Washington, D.C"u8
                    .ToArray());
        var x = input.Process(dataStream).Count();
        //More or less only testing that refactoring doesn't change the node count. 38 is what I think I want.
        Assert.Equal(38, x);
    }
}