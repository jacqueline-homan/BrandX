module BrandX.N3

open System
open System.IO
open FParsec
open BrandX.Structures

type AddressInfo =
    | AddressInfo of string

let pAddy : Parser<AddressInfo> = manyMinMaxSatisfy 1 55 (isNoneOf "*~") |>> AddressInfo //.>> pFSep

type Details = 
    | Details of string 

let pDet : Parser<Details option> = 
    (opt 
        (manyMinMaxSatisfy 1 55 (isNoneOf "*~") |>> Details)) .>> pRSep

type OptionalAddressInfo = 
    { address : AddressInfo
      optDetails : Details option}

let pOptAdInf = 
    pipe2 pAddy pDet (fun a d ->
        { address = a
          optDetails = d})

type N3 = 
    | N3 of OptionalAddressInfo 

let pN3Record = 
    pOptAdInf
    >>= fun x -> 
        preturn (N3(x))


let pN3 : Parser<N3> = 
    skipString "N3" >>. pFSep >>. pN3Record 