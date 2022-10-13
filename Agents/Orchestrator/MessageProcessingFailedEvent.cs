using System;

namespace AboutCleanCode.Orchestrator;

record MessageProcessingFailedEvent(object Message, Exception Exception);
