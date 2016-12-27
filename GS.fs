module BrandX.GS

open BrandX.Structures
open FParsec
open System
open System.Collections.Generic
open System.IO

// GS*SM*MGCTLYST*BLNJ*20160930*145316*1*X*004010~
// GS-01: Functional Identifier Code, 2 chars, "SM"
type FuncIdCode = 
    | MotorCarrierLoadTender

let pFuncIdCode = 
    skipString "SM" >>. preturn MotorCarrierLoadTender .>> pFSep
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
type Routing = 
    { appSdndrCode : AppSndrCode
      appRecvrCode : AppRecvrCode }

let pRouting = 
    pipe2 pSdr pRcvr (fun s r -> 
        { appSdndrCode = s
          appRecvrCode = r })

//GS-04, GS-05: Date of transaction
type TxnTimeStamp = 
    | TxnTimeStamp of DateTime

let pTxnTimeStamp = pDateTime |>> TxnTimeStamp

//GS-06: Group Control Number
type GrpCtrlNo = 
    | GrpCtrlNo of string

let pGrpCtlNo = manyMinMaxSatisfy 1 9 Char.IsDigit .>> pFSep |>> GrpCtrlNo

//GS-07: Responsible Agency Code
type RspAgyCode = 
    | AccredStdsCmteX12

let pRspAgyCode = skipString "X" >>. preturn AccredStdsCmteX12 .>> pFSep

//GS-08: Version/Release/Industry Indentifier Code
type VRIIcode = 
    | DraftStds

let pVRIIcode = skipString "004010" >>. preturn DraftStds .>> pRSep

type GS = 
    | GS of FuncIdCode * Routing * TxnTimeStamp * GrpCtrlNo * RspAgyCode * VRIIcode

let pGS = 
    skipString "GS" >>. pFSep >>. pFuncIdCode 
    >>= fun fid -> 
        pRouting 
        >>= fun rtg -> 
            pTxnTimeStamp 
            >>= fun ts -> 
                pGrpCtlNo 
                >>= fun gcn -> 
                    pRspAgyCode 
                    >>= fun ragy -> 
                        pVRIIcode 
                        >>= fun vrii -> 
                            preturn (GS(fid, rtg, ts, gcn, ragy, vrii))
