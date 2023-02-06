namespace Athena.Build.CodeGen;

using System;
using System.IO;
using Mono.TextTemplating;

class Program
{
    public static int Main(string[] args)
    {
        try
        {
            var template = args[0];
            var output = args[1];

            Console.WriteLine($"Generating '{template}' -> '{output}'");

            var generator = new TemplateGenerator();
            generator.AddParameter(null, null, "featuresFolder", Path.GetDirectoryName(output));
            var success = generator.ProcessTemplateAsync(template, output).Result;

            if (!success)
            {
                foreach(var error in generator.Errors)
                {
                    Console.Error.WriteLine(error);
                }
            }

            return success ? 0 : 1;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
            return 1;
        }
    }
}
