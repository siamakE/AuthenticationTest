using ServiceStack;

namespace UnitTestDemo.ServiceModel;

[Route("/hello")]
[Route("/hello/{Name}")]
public class Hello : IReturn<HelloResponse>
{
    public string Name { get; set; }
}

public class HelloResponse
{
    public string Result { get; set; }
}

[Route("/secured")]
public class Secured : IReturn<SecuredResponse>
{
    public string Name { get; set; }
}

public class SecuredResponse
{
    public string Result { get; set; }

    public ResponseStatus ResponseStatus { get; set; }
}