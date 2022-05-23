namespace AboutCleanCode
{
    public class ExecutedTestCase
    {
        public ExecutedTestCase(string assembly, string name, double duration)
        {
            Contract.RequiresNotNullNotEmpty(assembly, nameof(assembly));
            Contract.RequiresNotNullNotEmpty(name, nameof(name));

            Assembly = assembly;
            Name = name;
            Duration = duration;
        }

        public string Assembly { get; }
        public string Name { get; }
        public double Duration { get; }
    }
}
