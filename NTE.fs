module BrandX.NTE

open System
open System.IO
open System.Collections.Generic
open FParsec
open BrandX.Structures

type RefCode =
    | RefCode of string

let pRefCode : Parser<Option<RefCode>> =
    opt
        (anyString 3 |>> RefCode) .>> pPSep .>> ws .>> pPSep

type Description =
    | Description of string

let pDescription : Parser<Description> =
    manyMinMaxSatisfy 1 80 (fun c -> isDigit c || isAsciiLetter c || isHex c) |>> Description .>> pRSep


type NTE = NTE of Option<RefCode> * Description

let pNTE =
    skipString "NTE" .>> pOFSep >>. pRefCode
    >>= fun d ->
        pDescription
        >>= fun n ->
            preturn (NTE(d, n))
