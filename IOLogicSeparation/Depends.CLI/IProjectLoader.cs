namespace Depends.CLI
{
    internal interface IProjectLoader
    {
        VsProject LoadProject(string path);
    }
}