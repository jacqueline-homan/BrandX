open System
open System.IO
open FParsec
open BrandX.SyntaxP

Console.ForegroundColor <- ConsoleColor.Cyan


[<EntryPoint>]
let main argv = 
    test pISA "ISA*00*          *00*          *ZZ*MGCTLYST       *02*BLNJ           *160930*1453*U*00401*000000001*0*P*:~" |> printfn "%A"

    0 // return an integer exit code

