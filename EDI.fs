module BrandX.EDI

open BrandX.B2
open BrandX.B2A
open BrandX.GS
open BrandX.ISA
open BrandX.ST
open BrandX.NTE
open FParsec

type EDI =
    | EDI of ISA * GS * ST * B2 * B2A

let pEDI =
    pISARec
    >>= fun a ->
        pGS
        >>= fun b ->
            pST
            >>= fun c ->
                pB2
                >>= fun d -> pB2A >>= fun e -> preturn (EDI(a, b, c, d, e))
