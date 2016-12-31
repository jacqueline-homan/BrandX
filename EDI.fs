module BrandX.EDI

open BrandX.GS
open BrandX.ISA
open BrandX.ST
open FParsec

let pEDI = pipe3 pISARec pGS pST (fun i g h -> (i, g, h))
