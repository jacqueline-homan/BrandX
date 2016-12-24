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
