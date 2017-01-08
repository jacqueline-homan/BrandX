module BrandX.L11

open System
open System.Collections.Generic
open System.IO
open BrandX.Structures
open FParsec

type RefId = 
    | RefId of string 

let pRefId : Parser<RefId> = manyMinMaxSatisfy 1 30 (isNoneOf "*~**:' '***'.") |>> RefId .>> pFSep


type RefQual = 
    | RefQual of string 

let pRefQual : Parser<RefQual> = manyMinMaxSatisfy 2 3 (isNoneOf "*~**:' '***'.") |>> RefQual .>> pFSep


type BusInstruct = 
    { referenceId : RefId
      referenceQual : RefQual}

let pBus = 
    pipe2 pRefId pRefQual (fun i q -> {referenceId = i; referenceQual = q}) 


type Description =
    | Description of string 

let pDesc : Parser<Description> = manyMinMaxSatisfy 1 80 (isNoneOf "*~**:' '") |>> Description .>> pFSep
       
type L11 =
    | L11 of BusInstruct * Description

let pL11Record = 
    pBus 
    >>= fun x ->
        pDesc 
        >>= fun y -> 
        preturn (L11(x, y))

let pL11 = 
    skipString "L11" >>. pFSep >>. pL11Record .>> pRSep 