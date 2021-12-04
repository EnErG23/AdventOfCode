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

y("ear") [year] --- Changes [year] (Overrides the automatically chosen year for AoC, starting from the release of the first task)

o("pen") [day] --- Opens [day] task in browser tab (change location of broswer exe in AocManager.OpenAoC()), tries to get possible test input and gets actual input

i("nput") [day] --- Gets [day] input and tries to get possible test input

p("rint") [day] ("test") --- Prints (test) [day] data of current year

r("un") ([day] (part)) ("test") --- (Test) runs [day] (part) of current year or runs all days of current year (except skipDays, addable in program.cs)

s("ubmit") --- Submits last available answer of last run day (shows response from AoC)

v("isualize") [day] (part) --- Shows visualization of [day] (part)

c("lear") --- Clears console

e("xit") --- Closes console

---------------------------------------------------------------------------------------------------------------------------------------------------------------
