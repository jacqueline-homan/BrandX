module BrandX.L11

open System
open System.Collections.Generic
open System.IO
open BrandX.Structures
open FParsec

type RefId = 
    | RefId of string 
let pRefId : Parser<RefId> = manyMinMaxSatisfy 1 30 (isNoneOf "*~**:***.") |>> RefId .>> pFSep

type L11 =
    | L11 of RefId

let pL11 : Parser<L11> =
    skipString "L11" .>> pFSep .>> pRefId 
