open BrandX.EDI
open FParsec
open System
open System.IO
open System.Text

Console.ForegroundColor <- ConsoleColor.Cyan

[<EntryPoint>]
let main argv = 
    printfn "%A" 
        (runParserOnFile pEDI () @"204-MGCTLYST-BLNJ-16542577549-1.txt" 
             Encoding.ASCII)
    0
