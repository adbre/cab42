﻿README of cab42
===============

* (c) Adam Brengesjö <ca.brengesjo@gmail.com> 2011-2012
* Licensed under the Apache License, Version 2.0 (the "License")


Summary
-------

CAB42 is a graphical tool to quickly setup and build multiple CAB
files with diferent configurations for Windows Mobile smart devices.


About
-----

I use CAB42 to build Microsoft Mobile .CAB files with different
profiles and settings.
Also, cabwiz.exe is a PITA to configure when you want to dynamically include
files in various directories relative to your output as all files must be
explicitly be included with a absolute file path.


Installing
----------

See the INSTALL file


Usage
-----

    $ cab42 build MyProject.c42


Overwrite variables

    $ cab42 build MyProject.c42 --set-variable Version 2.1

this sets the project's $(Version) variable to "2.1"


Defining a new variable

    $ branch_name="$(git symbolic-ref HEAD 2>/dev/null)"
    $ branch_name=${branch_name##refs/heads/}
    $ cab42 build MyProject.c42 --set-variable GitBranch ${branch_name}

This example will define a new variable `GitBranch` with the value
set to the currently checked-out branch name (empty if not inside a git
repository).


**Information tab**

Define variables that CAB42 should use. Remember that you need to define where
the variables are to be used under the Includes tab (see FAQ 1).

**Output tab**

**Includes tab**

Define rules for how each file should be included/parsed. That includes saying
what XML-tags in a file that should be replaced by what values (or variable
values). 

**Profiles tab**

Enter the values of the variables.

FAQ
---

**Why do I have to write a value as $(NameOfVariable)?**

To support that $(NameOfVariable)-foo-$(NameOfOtherVariable) also works.
Same syntax is used in output-filename, eg. 

    PreCom $(Version)$(ReleaseNamePart)-$(ServerAddress) ($(Profile)) - Debuglevel $(DebugLevel)

**What if I have several ServerAddress tags with different absolute path?**

Then you must match these in Includes tab -> Edit Rule (for the file) -> check Modify XML contents and change the XML tag values.