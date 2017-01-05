module BrandX.IEA

open System
open System.IO
open FParsec
open BrandX.Structures

type InclFunGrps = 
    | InclFunGrps of string

let pFgrps : Parser<InclFunGrps> = manyMinMaxSatisfy 1 5 (isNoneOf "~") |>> InclFunGrps .>> pFSep


type IntchgCtrlNo =
    | IntchgCtrlNo of uint16

let pCtrlNo : Parser<IntchgCtrlNo> = (manyMinMaxSatisfy 9 9 isDigit |>> (fun ctl -> UInt16.Parse(ctl) |> IntchgCtrlNo)) .>> pFSep

type IEA = IEA of InclFunGrps * IntchgCtrlNo

let pIEA =
    skipString "IEA" >>. pFSep >>. tuple2 pFgrps pCtrlNo |>> IEA .>> pRSep
