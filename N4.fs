module BrandX.N4

open System
open System.Collections.Generic
open System.IO
open FParsec
open BrandX.Structures

type City = 
    | City of string

let pCity : Parser<City option> =
    (opt
        (manyMinMaxSatisfy 2 30 (isNoneOf "*~") |>> City)) .>> pFSep

type State =
    | State of string

let pState : Parser<State option> = 
    (opt 
        (anyString 2 |>> State)) .>> pFSep

type Zipcode =
    | Zipcode of string

let pZip : Parser<Zipcode option> = 
    (opt
        (manyMinMaxSatisfy 3 15 (isNoneOf"*~.,':;' '") |>> Zipcode)) .>> pFSep

type Country = 
    | Country of string

let pCountry : Parser<Country option> =
    (opt 
        (manyMinMaxSatisfy 2 3 isAsciiLetter |>> Country)) .>> pRSep

(*
type AddressInfo = 
    { city : City option
      state : State option
      zip : Zipcode option
      country : Country option}

let pAddInf = 
    pipe4 pCity pState pZip pCountry (fun m s z c ->
        {city = m
         state = s
         zip = z
         country = c}) //.>> pRSep
*)
type N4 = 
    | N4 of City option * State option * Zipcode option * Country option //City * State * Zipcode * Country

let pN4 = 
    skipString "N4" >>. pFSep >>. pCity
    >>= fun a ->
        pState
        >>= fun b ->
            pZip
            >>= fun c ->
                pCountry
                >>= fun d ->
                    preturn (N4(a, b, c, d)) 

(*
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
*)

//let pN4record = skipString "N4" .>> pFSep >>. pN4 .>> pRSep 


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


