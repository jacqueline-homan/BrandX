//
//  SyntaxP.fs
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

// The Interchange ID Qualifier
type InterchangeID = InterchangeID of string

let pInterchgeID = anyString 2 |>> InterchangeID .>> pFSep 

// The Interchange Sender ID
type InterchgSndrID = InterchgSndrID of string 

let pInterchgSndrId = anyString 15 |>> InterchgSndrID .>> pFSep
    
type ISA = 
    | ISA of Auth * Sec * InterchangeID * InterchgSndrID


let pISARec : Parser<_> = skipString "ISA" >>. pFSep

let pISA = 
    pISARec >>. pipe4 pAuth pSec pInterchgeID pInterchgSndrId (fun q i n x -> ISA (q, i, n, x)) 




let test p str = 
    match run p str with
    | Success(result, _, _) -> printfn "Success: %A" result
    | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg
