module BrandX.EDI

open BrandX.B2
open BrandX.B2A
open BrandX.GS
open BrandX.ISA
open BrandX.ST
open BrandX.NTE
open FParsec

let pEDI = pISARec .>>. pGS .>>. pST .>>. pB2 .>>. pB2A .>>. pNTE
