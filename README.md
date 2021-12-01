---------------------------------------------------------------------------------------------------------------------------------------------------------------

# AdventOfCode

---------------------------------------------------------------------------------------------------------------------------------------------------------------

Add AoC sessionID as User Secret:

{
  "AocConfig": {
    "SessionID": "[sessionID]"
  }
}

---------------------------------------------------------------------------------------------------------------------------------------------------------------

Commands:

command [obligatory] (optional)
-------------------------------

year [year]                //Changes year

read [day]                 //Opens task in browser tab (change location of broswer exe in AocManager.OpenAoC()), gets possible test input and gets actual input.

print [day] ("test")       //Prints (test) day data of current year

run [day] (part) ("test")  //(Test) runs day (part) of current year

run ("test")               //(Test) runs all days of current year (except skipDays, addable in program.cs)

submit                     //Submits last available answer of last run day (shows response from AoC)

clear                      //Clears console

---------------------------------------------------------------------------------------------------------------------------------------------------------------
