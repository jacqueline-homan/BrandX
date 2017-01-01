module BrandX.ST

open FParsec
open System
open System.Collections.Generic
open System.IO
open BrandX.Structures

type TsID = 
    | MotorCarrierLoadTender

let pTsId : Parser<TsID> = skipString "204" >>. preturn MotorCarrierLoadTender .>> pFSep

type TctrlNo =
    | TctrlNo of string 

let pTctrlNo : Parser<TctrlNo> = manyMinMaxSatisfy 4 9 (fun c -> isDigit c ) |>> TctrlNo .>> pRSep  

type ST = ST of TsID * TctrlNo 

let pST = 
    skipString "ST" >>. pFSep >>. pTsId
    >>= fun id ->
        pTctrlNo
        >>= fun ctrl -> 
            preturn (ST(id, ctrl))
(*
let pST = 
    skipString "ST" >>. pFSep 
    >>= fun _ -> 
        pTsId
        >>= fun id ->
            pTctrlNo 
            >>= fun ctrl ->
                preturn (ST(id, ctrl))

*) 