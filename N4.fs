module BrandX.N4

open System
open System.Collections.Generic
open System.IO
open FParsec
open BrandX.Structures

type City = 
    | City of string

let pCity : Parser<City> =
    manyMinMaxSatisfy 2 30 (isNoneOf "*~") |>> City .>> pFSep

type State =
    | State of string

let pState : Parser<State> = anyString 2 |>> State .>> pFSep

let pZip : Parser<_> = manyMinMaxSatisfy 3 15 (isNoneOf"*~.,':; ' '") .>> pFSep

let pCountry : Parser<_> = manyMinMaxSatisfy 2 3 isAsciiLetter .>> pRSep

type N4 = 
    | N4 of City * State

let pN4 : Parser<N4> =
    //skipString "N4" >>. pFSep >>. pCity |>> N4//|>> fun _ -> N4
    skipString "N4" >>. pFSep >>. pCity
    >>= fun a ->
        pState
        >>= fun b ->
            preturn (N4(a, b)) 


