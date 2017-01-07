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

type Zipcode =
    | Zipcode of string

let pZip : Parser<Zipcode> = manyMinMaxSatisfy 3 15 (isNoneOf"*~.,':; ' '") |>> Zipcode .>> pFSep

type Country = 
    | Country of string

let pCountry : Parser<Country> = manyMinMaxSatisfy 2 3 isAsciiLetter |>> Country .>> pRSep


type AddressInfo = 
    { city : City
      state : State
      zip : Zipcode
      country : Country}

let pAddInf = 
    pipe4 pCity pState pZip pCountry (fun m s z c ->
        {city = m
         state = s
         zip = z
         country = c})

type N4 = 
    | N4 of AddressInfo * City * State * Zipcode * Country //City * State * Zipcode * Country

let pN4 =
    pAddInf
    >>= fun a -> 
        pCity
        >>= fun b -> 
            pState
            >>= fun c ->
                pZip
                >>= fun d ->
                    pCountry
                    >>= fun e ->
                        preturn (N4(a,b,c,d,e))

let pN4record = skipString "N4" >>. pFSep .>> pAddInf .>> pRSep 
(*
    skipString "N4" >>. pFSep >>. pCity
    >>= fun a ->
        pState
        >>= fun b ->
            pZip
            >>= fun c ->
                pCountry
                >>= fun d ->
                    preturn (N4(a, b, c, d)) 
    
*)

