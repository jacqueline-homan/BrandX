module BrandX.B2

open FParsec
open System
open System.Collections.Generic
open System.IO
open BrandX.Structures

//Beginning Segment for Shipment Information Transaction

type StdCarAlphaCode = 
    | StdCarAlphaCode of string 

let pAlpha : Parser<StdCarAlphaCode> = manyMinMaxSatisfy 2 4 (fun c -> isDigit c) |>> StdCarAlphaCode .>> pFSep

type ShipIdNo = 
    | ShipIdNo of string 

let pShip : Parser<ShipIdNo> = manyMinMaxSatisfy 1 30 (fun c -> isDigit c || isAsciiLetter c) |>> ShipIdNo .>> pFSep

type ShipPmt = 
    | Collect of ShipPmt
    | Prepaid of ShipPmt
    | ThirdPartyPay of ShipPmt

