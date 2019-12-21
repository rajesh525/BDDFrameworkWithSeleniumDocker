Feature: SpecFlowFeaturesecond
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: multiply
	Given I have entered 5 into the calculator
	And I have entered 6 into the calculator
	When I press add
	Then the result should be 12 on the screen

@mytag
Scenario Outline: multiply two numbers
	Given I have entered <first> into the calculator
	And I have entered <second> into the calculator
	When I press add
	Then the result should be <result> on the screen

	Examples:
	| first | second | result |
	| 5     | 6      | 12     |
	| 9     | 18     | 918    |