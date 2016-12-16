open System
open System.IO
open FParsec
open BrandX.SyntaxP



[<EntryPoint>]
let main argv = 
    test pISA "ISA*00*          *" |> printfn "%A"
    0 // return an integer exit code

