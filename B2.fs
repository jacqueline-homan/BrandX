module BrandX.B2

open BrandX.Structures
open FParsec
open System
open System.Collections.Generic
open System.IO

//Beginning Segment for Shipment Information Transaction
type StdCarAlphaCode = 
    | StdCarAlphaCode of string

let pStdCarAlphaCode : Parser<Option<StdCarAlphaCode>> = 
    opt ((manyMinMaxSatisfy 2 4 isAsciiLetter |>> StdCarAlphaCode)) 
    .>> pOFSep

type ShipIdNo = 
    | ShipIdNo of string

let pShipIdNo : Parser<Option<ShipIdNo>> = 
    opt 
        (manyMinMaxSatisfy 1 30 (fun c -> isDigit c || isAsciiLetter c) 
         |>> ShipIdNo) .>> pOFSep

type ShipPmt = 
    | Collect
    | Prepaid
    | ThirdPartyPay

let pShipPmt : Parser<ShipPmt> = 
    (skipString "PP" >>? preturn Prepaid) 
    <|> (skipString "CC" >>? preturn Collect) 
    <|> (skipString "TP" >>? preturn ThirdPartyPay)

type B2 = Option<StdCarAlphaCode> * Option<ShipIdNo> * ShipPmt

// B2**BLNJ**BLNJ75035079T**PP~
let pB2 : Parser<B2> = 
    skipString "B2" >>. pOFSep 
    >>. tuple3 pStdCarAlphaCode pShipIdNo pShipPmt .>> pRSep