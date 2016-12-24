module BrandX.GS

open FParsec
open BrandX.Structures

//The Functional Group Header
type FuncIdCode = CodeSM

//GS-01: Functional Identifier Code
let pFuncIdCode : Parser<FuncIdCode> = stringReturn "SM" CodeSM .>> pFSep

type CodeListSum = CodeListSum of string

//The Function Identifier and CodeList Summary data
type GSFunc = 
    {funcIdCode : FuncIdCode
     codeListSum : CodeListSum}

//GS-02: Application Sender Code



//TODO let pGSRec = skipString "GS" >>. pFSep >>. pGS .>> pElSep .>> pRSep