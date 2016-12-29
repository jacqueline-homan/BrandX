module BrandX.EDI

open BrandX.GS
open BrandX.ISA
open FParsec

let pEDI = pipe2 pISARec pGS (fun i g -> (i, g))
