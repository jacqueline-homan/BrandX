﻿module BrandX.EDI

open BrandX.B2
open BrandX.GS
open BrandX.ISA
open BrandX.ST
open FParsec

let pEDI = pISARec .>>. pGS .>>. pST .>>. pB2
