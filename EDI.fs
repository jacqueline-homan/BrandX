module BrandX.EDI

open BrandX.GS
open BrandX.ISA
open BrandX.ST
open BrandX.B2
open FParsec

let pEDI = pipe4 pISARec pGS pST pB2 (fun i g s b -> (i, g, s, b))
