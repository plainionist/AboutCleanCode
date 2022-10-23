using System;

namespace AboutCleanCode.Orchestrator;

internal record MessageProcessingFailedEvent(object Message, Exception Exception);
