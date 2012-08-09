CAB42 is a graphical tool to quickly setup and build multiple CAB files with diferent configurations for Windows Mobile smart devices.

Information tab
Define variables that CAB42 should use. Remember that you need to define where the variables are to be used under the Includes tab (see FAQ 1).

Output tab

Includes tab
Define rules for how each file should be included/parsed. That includes saying what XML-tags in a file that should be replaced by what values (or variable values). 

Profiles tab
Enter the values of the variables.

FAQ
1. Why do I have to write a value as $(NameOfVariable)? 
To support that $(NameOfVariable)-foo-$(NameOfOtherVariable) also works.
Same syntax is used in output-filename, eg. PreCom $(Version)$(ReleaseNamePart)-$(ServerAddress) ($(Profile)) - Debuglevel $(DebugLevel)

2. What if I have several ServerAddress tags with different absolute path? 
Then you must match these in Includes tab -> Edit Rule (for the file) -> check Modify XML contents and change the XML tag values.