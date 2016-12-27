module BrandX.Structures

open FParsec


// Parse and EDI record value into a record type tag and then a sequence of fields
type Parser<'t> = Parser<'t, unit>

// The field separator
let pFSep : Parser<_> = skipChar '*'

// The record delimiter
let pRSep : Parser<_> = skipChar '~'

//ISA-16: Component Element Separator. Since this is not a data structure, we only need a function.
let pElSep : Parser<_> = skipChar ':'

//Parser function for handling a range, which is needed for GS-03 which has a
//min/max number of characters from 2-15
(*
let manyRA p =
  // the compiler expands the call to Inline.Many to an optimized sequence parser
  Inline.Many(elementParser = p,
              stateFromFirstElement = (fun x0 ->
                                         let ra = ResizeArray<_>()
                                         ra.Add(x0)
                                         ra),
              foldState = (fun ra x -> ra.Add(x); ra),
              resultFromState = (fun ra -> ra),
              resultForEmptySequence = (fun () -> ResizeArray<_>()))
*)


