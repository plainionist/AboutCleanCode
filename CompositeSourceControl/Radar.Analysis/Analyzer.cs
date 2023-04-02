using Radar.Entities;
using Radar.SourceControl;

namespace Radar.Analysis;

internal class Analyzer
{
    private readonly ISourceControl mySourceControl;

    public Analyzer(ISourceControl sourceControl)
    {
        Contract.RequiresNotNull(sourceControl, nameof(sourceControl));

        mySourceControl = sourceControl;
    }

    public Report Analyze(CodeBase codeBase)
    {
        // TODO:
        // - fetch history
        // - analyze source code
        // - compute measures

        return null;
    }
}
