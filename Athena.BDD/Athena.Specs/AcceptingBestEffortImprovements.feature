Feature: Highlight accepted BestEffort work items

Scenario: Rendering the initiative backlog
	GIVEN an improvement
	AND BacklogSet is set to 'Best Effort'
	AND Decision is set to 'accepted'
	WHEN rendering the initiative backlog
	THEN the BacklogSet column is highlighted

Scenario: Rendering the team backlog	
	GIVEN an improvement
	AND BacklogSet is set to 'Best Effort'
	AND Decision is set to 'accepted'
	WHEN rendering the team backlog
	THEN the BacklogSet column is highlighted

Scenario: Rendering the team improvements backlog
	GIVEN an improvement
	AND BacklogSet is set to 'Best Effort'
	AND Decision is set to 'accepted'
	WHEN rendering the team improvements backlog
	THEN the BacklogSet column is highlighted

Scenario: Accepted MMP improvements are not high
	GIVEN an improvement
	AND BacklogSet is set to 'MMP'
	AND Decision is set to 'accepted'
	WHEN rendering the initiative backlog
	THEN the BacklogSet column is not highlighted


