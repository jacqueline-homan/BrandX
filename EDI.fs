module BrandX.EDI

open BrandX.GS
open BrandX.ISA
open FParsec
open System

let pEDI = pipe2 pISARec pGSRec (fun i g -> (i, g))