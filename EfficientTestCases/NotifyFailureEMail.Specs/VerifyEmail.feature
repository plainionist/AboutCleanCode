Feature: Notify teams by e-mail
	Notifies the development teams by e-mail in case of errors occured during
	defect generation or in case no faililng test cases were found.

Scenario: E-mail notifies that no defects were created
	Given a valid build number "SFO.A1.v9.NB_20210417.1"
	And a test fixture "Foo" with test case "VerifySomeValue"
	And a defect already existing for test case "Foo.TestFixtureSetup"
	When test case "Foo.TestFixtureSetup" fails with error
		"""
		Count expected to be 7 but was 42
		"""
	Then no defects created
	And an e-mail is sent with subject
		"""
		[DefectGenerator] No defects created for build SFO.A1.v9.NB_20210417.1
		"""
