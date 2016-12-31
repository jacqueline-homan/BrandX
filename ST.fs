module BrandX.ST

open FParsec
open System
open System.Collections.Generic
open System.IO
open BrandX.Structures

//ST-01
type TrnsSetIdCode = 
    | MotorCarrierLoadTender

let pTrnsSetIdCode : Parser<TrnsSetIdCode> = skipString "204" >>. preturn MotorCarrierLoadTender //.>> pFSep

//ST-02
type TrnSetCtrlNo = 
    | TrnSetCtrlNo of string 

let pCode : Parser<TrnSetCtrlNo> = anyString 3 |>> TrnSetCtrlNo //.>> pRSep

type TransCode =
    { idCode : TrnsSetIdCode
      ctrlNo : TrnSetCtrlNo}

let pctrl : Parser<TrnSetCtrlNo> = manyMinMaxSatisfy 4 9 Char.IsDigit .>> pFSep |>> TrnSetCtrlNo 

let pTransCode = 
    pipe2 pTrnsSetIdCode pCode (fun t c ->
        {idCode = t
         ctrlNo = c}) 
        
type ST = ST of TrnsSetIdCode * TrnSetCtrlNo * TransCode

let pST = 
    skipString "ST" >>. pFSep >>. pTrnsSetIdCode
    >>= fun p -> 
        pCode
        >>= fun q ->
            pctrl
            >>= fun r ->
                pTransCode
                >>= fun s ->
                preturn (ST(p, q, s))
    