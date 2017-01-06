module BrandX.N3

open System
open System.IO
open FParsec
open BrandX.Structures

type AddressInfo =
    | AddressInfo of string

let pAddy : Parser<AddressInfo> = manyMinMaxSatisfy 1 55 (isNoneOf "*~") |>> AddressInfo .>> pFSep

type Details = 
    | Details of string 

let pDet : Parser<Details option> = 
    (opt 
        (manyMinMaxSatisfy 1 55 (isNoneOf "*~") |>> Details)) .>> pRSep

type N3 = 
    | N3 of AddressInfo * Details option

let pN3 : Parser<N3> = 
    //skipString "N3" >>. pFSep >>. tuple2 (pAddy .>> pFSep) (pDet .>> pFSep) |>> N3 .>> pRSep
    skipString "N3" >>. pFSep >>. pAddy 
    >>= fun a ->
        pDet 
        >>= fun b ->
            preturn (N3(a, b)) //.>> pRSep
