using System.Collections.Generic;

namespace AboutCleanCode
{
    public static class CompositionExtension
    {
        public static void AddFeatureA(this Composer composer)
        {
            composer.AddSingleton<ISomeInterface>(new SomeImplementation());
            
            composer.AddSingleton<IServiceExtension>(new ExtensionA());
            composer.AddSingleton<IServiceExtension>(new ExtensionB());
        }
    }

    public class SomeController
    {
        private ISomeInterface mySome;
        public SomeController(Composer composer)
        {
            mySome = composer.Resolve<ISomeInterface>();
        }
    }



    

    public class Service
    {
        [ExtensionPoint]
        public IEnumerable<IServiceExtension> Extensions { get; private set; }

        public void Execute() { }
    }














    public interface ISomeInterface { }

    public class SomeImplementation : ISomeInterface { }

    public interface IServiceExtension { }

    public class ExtensionA : IServiceExtension { }
    public class ExtensionB : IServiceExtension { }
}
