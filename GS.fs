module BrandX.GS
open System
open System.Collections.Generic
open System.IO
open FParsec
open BrandX.Structures


//The Functional Group Header
type FuncIdCode = SomeSM

//GS-01: Functional Identifier Code
let pFuncIdCode : Parser<FuncIdCode> = stringReturn "SM" SomeSM .>> pFSep

//The Function Identifier and CodeList Summary data
type GSFunc = {funcIdCode : FuncIdCode}

//GS-02: Application Sender Code
type AppSndrCode = 
    | AppSndrCode of string

let pSdr : Parser<AppSndrCode> = anyString 2 |>> AppSndrCode .>> pFSep

//GS-03: Application Receiver Code
type AppRecvrCode = 
    | AppRecvrCode of string list

//let pRcvr : Parser<AppRecvrCode> = manyRA [2..15] |>> AppRecvrCode .>> pFSep


//TODO let pGSRec = skipString "GS" >>. pFSep >>. pGS .>> pElSep .>> pRSep