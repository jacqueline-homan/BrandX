module BrandX.N1

open BrandX.Structures
open FParsec
open System
open System.IO

type Entity = 
    | Entity of string

let pEntity : Parser<_> = manyMinMaxSatisfy 2 3 isAsciiLetter |>> Entity

type Name = 
    | Name of string

let pName : Parser<_> = manyMinMaxSatisfy 1 60 (isNoneOf "*~") |>> Name

type IdQual = 
    | IdQual of string

let pIdQual : Parser<_> = manyMinMaxSatisfy 1 2 isDigit |>> IdQual

type IdCode = 
    | IdCode of string

let pIdCode : Parser<_> = manyMinMaxSatisfy 2 80 (isNoneOf "*~") |>> IdCode

type N1 = 
    | N1 of Entity * Name //* IdQual * IdCode

let pN1 : Parser<N1> = 
    skipString "N1" >>. pFSep >>. tuple2 (pEntity .>> pFSep) pName |>> N1
(*
        >>= fun n ->
            pName
            >>= fun q ->
                pIdQual
                >>= fun c ->
                    pIdCode
                    >>= fun d ->
                        preturn (N1(n, q, c, d))
    *)
