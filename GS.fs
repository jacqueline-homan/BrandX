module BrandX.GS

open System
open System.Collections.Generic
open System.IO
open FParsec
open BrandX.Structures

// GS*SM*MGCTLYST*BLNJ*20160930*145316*1*X*004010~

// GS-01: Functional Identifier Code, 2 chars, "SM"
type FuncIdCode =
    | MotorCarrierLoadTender

let pFuncIdCode = skipString "SM" >>. preturn MotorCarrierLoadTender .>> pFSep

let pRouteCode = manyMinMaxSatisfy 2 15 Char.IsUpper

//GS-02: Application Sender Code, 2 chars
type AppSndrCode =
    | AppSndrCode of string

let pSdr : Parser<AppSndrCode> = pRouteCode |>> AppSndrCode .>> pFSep

//GS-03: Application Receiver Code
type AppRecvrCode =
    | AppRecvrCode of string

let pRcvr : Parser<AppRecvrCode> = pRouteCode |>> AppRecvrCode .>> pFSep

//Handling the relationship set between Sender and Receiver
type Routing = { appSdndrCode : AppSndrCode
                 AppRecvrCode : AppRecvrCode }

let pRouting = pipe2 pSdr pRcvr (fun s r -> (s, r))

type GSRec = { funcIdCode : FuncIdCode
               routing : Routing}                 

let pGSRec =
    skipString "GS" >>. pFSep >>. pFuncIdCode 
    >>= fun fid -> 
        pRouting 
        >>= fun rte -> 
            preturn (fid, rte)

//GS-04: Date of transaction
type Date = Date of string 

let pdate = anyString 8 |>> Date >>. pFSep

//GS-05: Time of transaction
type Time = Time of string 

let ptime = manyMinMaxSatisfy 4 8  Char.IsDigit >>. pFSep

//GS-06: Group Control Number
type GrpCtrlNo = GrpCtrlNo of string 

let pGrpCtlNo = manyMinMaxSatisfy 1 9 Char.IsDigit >>. pFSep

//GS-07: Responsible Agency Code
type RspAgyCode = 
    | AccredStdsCmteX12

let pRspAgyCode = skipString "X" >>. preturn AccredStdsCmteX12 >>. pFSep

let pAgyCode<'T,'u> = manyMinMaxSatisfy 1 2 Char.IsUpper

//GS-08: Version/Release/Industry Indentifier Code
type VRIIcode =
    | DraftStds 

let pVRIIcode = skipString "00410" >>. preturn DraftStds >>. pFSep

let pVcode<'T,'u> = manyMinMaxSatisfy 1 12 Char.IsNumber

