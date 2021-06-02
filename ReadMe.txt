This file provides the information about the solution to the problem titled "Tame of Thrones".
This is a console based application, developed in Visual Studio 2015 environment.

=========================================================================================================
1. Running the application
=========================================================================================================
1.1 Open the TameOfThrones.sln in Visual Studio
1.2 Make sure TameOfThrones.PL is the startup project
1.3 Press F5 or "Ctrl + F5" key to run the application
1.4 A console window will be displayed

=========================================================================================================
2. How to use the application
=========================================================================================================
2.1 On the console window, choose the option by pressing respective number key.

-> 2.1.1 Pressing number key 1 will open the page to choose the question
	-> Choose the question by pressing respective number key i.e. 1 or 2
	-> Pressing number key 3 will take you back to previous page i.e. section 2.1
-> 2.1.2 Pressing number key 2 will open the page to send message to a kingdom from King Shan
	-> Input the kingdom name and message separated by Comma and press Enter key
		-> enter exit if you want to quit and go back
	-> If the input is valid, it'll continue to take input
	-> If the input is invalid, an error message will be displayed
		-> To continue with more input press y/Y otherwise press n/N to go back
-> 2.1.3 Pressing number key 3 will open the page to do the ballot process
	-> Input the names of competing kingdoms separated by Space
	-> If the input is invalid, an error message will be displayed
		-> To continue with more input press y/Y otherwise press n/N to go back
	-> If the input is valid, the ballot result will be displayed
-> 2.1.4 Pressing number key 4 will reset the previously selected Ruler and its Allies
-> 2.1.5 Pressing number key 5 will close/quit the application

=========================================================================================================
3. Code organization
=========================================================================================================
There are 3 assenblies in the solution i.e TameOfThrones.PL, TameOfThrones.BL and TameOfThrones.UnitTest.

3.1 TameOfThrones.PL [exe]
	-> This is the main executable assembly.
	-> It's the Presentation Layer or Ui. All kind of Input/Output is handled here.
	-> It gets user input and send it to BL layer for processing via an interfacing class BlAccessor.
	-> After processing done at BL layer, the results are displayed here.

3.2 TameOfThrones.BL [dll]
	-> This assembly is Business Layer.
	-> It processes the user input and send the results back.
	-> Two handler classes are implemented that handles two types of input i.e. Message or Ballot.
	-> Two parser classes are implemented that parse two types of input i.e. Message or Ballot.
	-> A Kingdom class is implemented which provides info about any kingdom and checks & provides allience.
	-> The InstanceProvider class is a kind of factory which creates instances of different classes.
	-> OutputDataProvider class provides the formatted data about ruling kingdom.
	-> The class BlAccessor provides functionality for interfacing with PL.

3.3 TameOfThrones.UnitTest [dll]
	-> This is the Unit Test assembly.
	-> Unit Tests run with each build.
	-> If all tests are passed then build will be success otherwise failure.

Both the layers (PL & BL) are separate and independent and connected via an iterfacing class BlAccessor.