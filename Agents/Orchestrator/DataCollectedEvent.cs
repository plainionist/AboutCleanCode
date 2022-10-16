using System;

namespace AboutCleanCode.Orchestrator;

internal record DataCollectedEvent(Guid JobId, object Payload);
