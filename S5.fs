module BrandX.S5

open System
open System.Collections.Generic
open System.IO
open FParsec
open BrandX.Structures

type StopSeqNo = 
    | StopSeqNo of uint16

let pStopSeqNo : Parser<StopSeqNo> = manyMinMaxSatisfy 1 3 isDigit |>> (fun c -> UInt16.Parse(c) |> StopSeqNo) .>> pFSep

type StopReason =
    | Complete
    | CompleteUnload
    | PartLoad
    | PartUnload 

let pStopReason : Parser<StopReason> = 
    ((skipString "CL" >>? preturn Complete)
      <|> (skipString "CU" >>? preturn CompleteUnload)
      <|> (skipString "PL" >>? preturn PartLoad) 
      <|> (skipString "PU" >>? preturn PartUnload)) .>> pRSep

type S5 =
    | S5 of StopSeqNo * StopReason

let pS5 : Parser<S5> =
    skipString "S5" .>> pFSep >>. pStopSeqNo
    >>= fun x -> 
        pStopReason
        >>= fun y ->
            preturn (S5(x, y))
         
