module BrandX.B2A

open BrandX.Structures
open FParsec
open System
open System.Collections.Generic
open System.IO

//B2A: Transaction Set Purpose Code
type SetPurpCode =
    | Original
    | Cancellation
    | Change

let pSetPurpCode : Parser<SetPurpCode> =
    ((skipString "00" >>? preturn Original)
     <|> (skipString "01" >>? preturn Cancellation)
     <|> (skipString "04" >>? preturn Change)) .>> pFSep

type AppType =
    | AppType of string

let pAppType : Parser<AppType option> = opt (anyString 2 |>> AppType)

type B2A = SetPurpCode * AppType option

let pB2A : Parser<B2A> =
    skipString "B2A" >>. pFSep >>. tuple2 pSetPurpCode pAppType .>> pRSep
