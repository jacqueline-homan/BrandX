open System
open System.IO
open FParsec
open BrandX.ISA
open BrandX.Structures
open BrandX.GS

Console.ForegroundColor <- ConsoleColor.Cyan

(*
let path = @"204-MGCTLYST-BLNJ-16542577549-2.txt"
let file = File.ReadAllText(path)
*)
[<EntryPoint>]
let main argv =
    //test pISARec "ISA*00*          *00*          *ZZ*MGCTLYST       *02*BLNJ           *160930*1453*U*00401*000000001*0*P*:~" |> printfn "%A"
    printfn "%A"
        (runParserOnFile pISARec () @"/home/jacque/Projects/F-sharp/BrandX/BrandX/204-MGCTLYST-BLNJ-16542577549-2.txt" 
            System.Text.Encoding.ASCII)
    0 // return an integer exit code
