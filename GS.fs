module BrandX.GS

open FParsec
open BrandX.Structures

//The Functional Group Header
type FuncIdCode = CodeSM

//GS-01: Functional Identifier Code
let pFuncIdCode : Parser<FuncIdCode> = stringReturn "SM" CodeSM .>> pFSep

type CodeListSum = 
    | CodeListSum of string

//GS-02: Application Sender Code
