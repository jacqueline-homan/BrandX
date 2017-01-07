# BrandX

BrandX is an F# console application that parses EDI files for the trucking industry. It writes the EDI values to F# data structure types and maintains EDI-compliance. It was developed using Monodevelop v. 6.1 using Nuget for package management on a Debian 8 Sid/Stretch Linux OS.

##How to Use This Program

- You should first have a working installation of F# 4.0 or higher on your computer.
- Clone or download this repo into your local computer.
- This program includes a test EDI text file in its solution folder. You may remove it and add another EDI text file by clicking on the second BrandX folder in the solution and selecting "Add File" and then simply add the EDI text file that you wish to parse.
- After adding the EDI file you want to parse to the project, select Rebuild.
- Replace the old EDI text file reference in the parser function body in Program.fs underneath the [<EntryPoint>] attribute with the path/filename of the new EDI file you want to parse and save the solution. 
- The preferences in this solution are set to output to an external console (your terminal). Click on Run in either the Xamarin or Monodevelop environment (or Visual Studio), or open up a terminal and go into the directory that you've cloned this repo into and go to the **bin/Debug directory** and run the command `mono BrandX.exe` and it should parse your EDI file, writing your EDI groups and field element values to F# data types, preserving all of the structures.
