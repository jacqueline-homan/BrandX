open System
open System.IO
open System.Text
open FParsec
open BrandX.ISA
open BrandX.Structures
open BrandX.GS
open BrandX.EDI

Console.ForegroundColor <- ConsoleColor.Cyan

(*
let path = @"204-MGCTLYST-BLNJ-16542577549-2.txt"
let file = File.ReadAllText(path)
*)



[<EntryPoint>]
let main argv =
    //test pISARec "ISA*00*          *00*          *ZZ*MGCTLYST       *02*BLNJ           *160930*1453*U*00401*000000001*0*P*:~" |> printfn "%A"
    printfn "%A"
        (runParserOnFile pEDI () @"/home/jacque/Projects/F-sharp/BrandX/BrandX/204-MGCTLYST-BLNJ-16542577549-1.txt" 
            Encoding.ASCII)
    0 // return an integer exit code
