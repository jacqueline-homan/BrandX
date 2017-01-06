module BrandX.N1

open BrandX.Structures
open FParsec
open System
open System.IO

type Entity =
    | Entity of string
let pEntity : Parser<Entity> = manyMinMaxSatisfy 2 3 isAsciiLetter |>> Entity .>> pFSep

type Name =
    | Name of string
let pName : Parser<Name> = manyMinMaxSatisfy 1 60 (isNoneOf "*~") |>> Name .>> pFSep

type IdQual =
    | IdQual of string

let pIdQual : Parser<IdQual> = manyMinMaxSatisfy 1 2 isDigit |>> IdQual .>> pFSep

type IdCode =
    | IdCode of string

let pIdCode : Parser<IdCode> = manyMinMaxSatisfy 2 80 (isNoneOf "*~") |>> IdCode .>> pRSep

type N1 =
    | N1 of Entity * Name * IdQual * IdCode

(*
let pN1 : Parser<N1> =
    skipString "N1" >>. pFSep >>. tuple4 (pEntity .>> pFSep) ( pName .>> pFSep) (pIdQual .>> pFSep) (pIdCode .>> pRSep)|>> N1
*)
//because I did not want a nasty tuple4 monster staring me down, I refactored
let pN1 : Parser<N1> =
    skipString "N1" >>. pFSep >>. pEntity
        >>= fun n ->
            pName
            >>= fun q ->
                pIdQual
                >>= fun c ->
                    pIdCode
                    >>= fun x ->
                        preturn (N1(n, q, c, x))
