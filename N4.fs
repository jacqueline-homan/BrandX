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


type Location = 
    { city : City option
      state : State option
      zip : Zipcode option
      country : Country option}

let pLoc = 
    pipe4 pCity pState pZip pCountry (fun m s z c ->
        {city = m
         state = s
         zip = z
         country = c}) 

type N4 = 
    | N4 of Location 

let pN4Record = 
    pLoc
    >>= fun x -> 
        preturn (N4(x)) 
    

let pN4 = skipString "N4" >>. pFSep >>. pN4Record
  





