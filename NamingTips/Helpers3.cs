
namespace Naming
{
    public static class MockFactory
    {
        public static IVolumeDocument CreateVolumeDocument(bool withVolume) => null;
        public static IVolumeDocument CreateVolumeDocument(bool withVolume, string name, string hashCode) => null;
        public static IVolumeDocument CreateVolumeDocument(bool withVolume, string name) => null;
        public static ISegmentation CreateSegmentation(IVolumeDocument doc) => null;
        public static IVolumePresentation CreateVolumePresentation() => null;
        public static T CreatePresentation<T>(IVolumeDocument volumeDocument) where T : IPresentation => default(T);
    }

    public interface IPresentation { }

    public interface IVolumePresentation { }

    public interface ISegmentation { }

    public interface IVolumeDocument { }
}
