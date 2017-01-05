module BrandX.N1

open System
open System.IO
open FParsec
open BrandX.Structures

let pEntity : Parser<_> = manyMinMaxSatisfy 2 3 Char.IsLetterOrDigit

let pName : Parser<_> = manyMinMaxSatisfy 1 60 

let pN1 : Parser<_> = 
    skipString "N1" >>. pFSep 

