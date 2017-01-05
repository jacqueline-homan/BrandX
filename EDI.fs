module BrandX.EDI

open BrandX.B2
open BrandX.B2A
open BrandX.GS
open BrandX.ISA
open BrandX.NTE
open BrandX.ST
open BrandX.N1
open BrandX.IEA
open FParsec

type EDI = 
    | EDI of ISA * GS * ST * B2 * B2A * NTE * N1

let pEDI = 
    pISARec 
    >>= fun a -> 
        pGS 
        >>= fun b -> 
            pST 
            >>= fun c -> 
                pB2 
                >>= fun d -> 
                    pB2A 
                    >>= fun e -> 
                        pNTE 
                        >>= fun f -> 
                            pN1
                            >>= fun g -> 
                                preturn (EDI(a, b, c, d, e, f, g))
