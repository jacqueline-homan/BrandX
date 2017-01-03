module BrandX.B2

open FParsec
open System
open System.Collections.Generic
open System.IO
open BrandX.Structures

//Beginning Segment for Shipment Information Transaction

let pAlpha : Parser<_> = manyMinMaxSatisfy 2 4 (fun c -> isDigit c) .>> pFSep

//The Shipment Identification Number cannot contain whitespaces or special chars
let pShip : Parser<_> = manyMinMaxSatisfy 1 30 (fun c -> isDigit c || isAsciiLetter c) .>> pFSep

//The Shipment Method of Payment code is a choice of one of three possibilities: CC, PP, TP
let pPay : Parser<_> = anyString 2 .>> pFSep

let pB2 = 
    skipString "B2" >>. pFSep >>. pAlpha
    >>= fun s ->
        pShip
        >>= fun p -> 
            pPay 
(*
type StdCarAlphaCode = 
    | StdCarAlphaCode of string 

let pAlpha : Parser<StdCarAlphaCode> = manyMinMaxSatisfy 2 4 (fun c -> isDigit c) |>> StdCarAlphaCode .>> pFSep

type ShipIdNo = 
    | ShipIdNo of string 

let pShip : Parser<ShipIdNo> = manyMinMaxSatisfy 1 30 (fun c -> isDigit c || isAsciiLetter c) |>> ShipIdNo .>> pFSep

type ShipPmt = 
    | Collect of string
    | Prepaid of string 
    | ThirdPartyPay of string

let pPmt : ShipPmt = anyString

let pB2 =
    skipString "B2" >>. pFSep
*)