//
//  Author:
//       evan <>
//
//  Copyright (c) 2016 evan
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
module BrandX.SyntaxP

open FParsec

// Parse a record like:
//
//     ISA*00*          *00*          *ZZ*MGCTLYST       *02*BLNJ           *160930*1453*U*00401*000000001*0*P*:
//
// Into a record type tag and then a sequence of fields
type Parser<'t> = Parser<'t, unit>

// The field separator
let pFSep : Parser<_> = skipChar '*'

// The record delimiter
let pRSep : Parser<_> = skipChar '~'

// The Authorization Qualifier
type AuthQual = AQNone

// ISA-01: We only have one of these anyway
let pAuthQual : Parser<AuthQual> = stringReturn "00" AQNone .>> pFSep

type AuthInfo = 
    | AuthInfo of string

//ISA-02: this is 10 chars, may be whitespace
let pAuthInfo : Parser<AuthInfo> = anyString 10 |>> AuthInfo .>> pFSep

// The Authorization Qualifier and Info
type Auth = 
    { authQual : AuthQual
      authInfo : AuthInfo }

let pAuth = 
    pipe2 pAuthQual pAuthInfo (fun q i -> 
        { authQual = q
          authInfo = i })

// The Security Info Qualifier
type SecQual = SQNone

//ISA-03
let pSecQual : Parser<SecQual> = stringReturn "00" SQNone .>> pFSep

type SecInfo = 
    | SecInfo of string

//ISA-04
let pSecInfo : Parser<SecInfo> = anyString 10 |>> SecInfo .>> pFSep

// The Security Info Qualifier and stuff
type Sec = 
    { secQual : SecQual
      secInfo : SecInfo }

let pSec = 
    pipe2 pSecQual pSecInfo (fun q i -> 
        { secQual = q
          secInfo = i })

// ISA-05: The Interchange ID Qualifier
type InterchangeID = InterchangeID of string

let pInterchgeID = anyString 2 |>> InterchangeID .>> pFSep 

// ISA-06: The Interchange Sender ID
type InterchgSndrID = InterchgSndrID of string 

let pInterchgSndrId = anyString 15 |>> InterchgSndrID .>> pFSep

// ISA-07 is exactly the same as ISA-05 in the MG-EDI pdf manual
type InterchgIdQual = InterchgIdQual of string 

let pInterchgIdQual = anyString 2 |>> InterchgIdQual .>> pFSep

//ISA-08: The Interchange Receiver ID
type InterchgRecvrID = InterchgRecvrID of string

let pInterchgRcvId = anyString 15 |>> InterchgRecvrID .>> pFSep

//ISA-09: Interchange Date
type InterchgDate = InterchgDate of string 

let pInterchgDate = anyString 6 |>> InterchgDate .>> pFSep

//ISA-10: Interchange Time
type InterchgTime = InterchgTime of string

let pInterchgTime<'T,'u> = anyString 4 |>> InterchgTime .>> pFSep

//ISA-11: Interchange Control Standards Identifier
type InterchgCtrlStds = InterchgCtrlStds of string 

let pInterchgCtrlStds<'T, 'u> = anyString 1 |>> InterchgCtrlStds .>> pFSep

//ISA-12: Interchange Control Version Number
type InterchgCtrlVerNo = InterchgCtrlVerNo of string

let pInterchgCtrlVerNo = anyString 5 |>> InterchgCtrlVerNo .>> pFSep

//ISA-13: Interchange Control Number
type InterchgCtrlNo = InterchgCtrlNo of string

let pInterchgCtrlNo = anyString 9 |>> InterchgCtrlNo .>> pFSep

//ISA-14: Acknowledgement Requested
type AckReq = AckReq of string 

let pAckReq = anyString 1 |>> AckReq .>> pFSep   

//ISA-15: Usage Indicator
type UsageInd = UsageInd of string

let pUsageInd = anyString 1 |>> UsageInd .>> pFSep

//ISA-16: Component Element Separator. Since this is not a data structure, we only need a function.
let pElSep : Parser<_> = skipChar ':'

type ISA = 
    | ISA of Auth * Sec * InterchangeID * InterchgSndrID * InterchgIdQual * InterchgRecvrID * InterchgDate * InterchgTime * InterchgCtrlStds * InterchgCtrlVerNo * InterchgCtrlNo * AckReq * UsageInd


let pISARec : Parser<_> = skipString "ISA" >>. pFSep 

let pISA = 
    pISARec >>. pAuth 
    >>= fun a -> 
        pSec 
        >>= fun b -> 
            pInterchgeID 
            >>= fun c -> 
                pInterchgSndrId 
                >>= fun d ->
                    pInterchgIdQual
                    >>= fun e ->
                        pInterchgRcvId
                        >>= fun f -> 
                            pInterchgDate
                            >>= fun g ->
                                pInterchgTime
                                >>= fun h ->
                                    pInterchgCtrlStds
                                    >>= fun i ->
                                        pInterchgCtrlVerNo
                                        >>= fun j ->
                                            pInterchgCtrlNo
                                            >>= fun k -> 
                                                pAckReq
                                                >>= fun l ->
                                                    pUsageInd
                                                    >>= fun m -> 

                                            preturn (ISA(a, b, c, d, e, f, g, h, i, j, k, l, m)) 



let test p str = 
    match run p str with
    | Success(result, _, _) -> printfn "%A" result
    | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg
