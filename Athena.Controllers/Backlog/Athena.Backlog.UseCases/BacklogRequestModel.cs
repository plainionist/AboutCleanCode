using System;

namespace Athena.Backlog.UseCases
{
    public class BacklogRequestModel
    {
        public Team Team { get; init; }
        public DateTime? IterationStart { get; init; }
    }
}