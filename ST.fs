module BrandX.ST

open FParsec
open System.Collections.Generic
open System.IO
open BrandX.Structures

type TrnsSetIdCode = 
    | MotorCarrierLoadTender

let pTrnsSetIdCode : Parser<TrnsSetIdCode> = skipString "M" >>. preturn MotorCarrierLoadTender .>> pFSep
