module BrandX.Structures

open FParsec
open System

// Parse and EDI record value into a record type tag and then a sequence of fields
type Parser<'t> = Parser<'t, unit>

// The field separator
let pFSep : Parser<_> = skipChar '*'
// Optional fields are separated by two '*'
let pOFSep : Parser<_> = skipString "**"
// The record delimiter
let pRSep : Parser<_> = skipChar '~'
// Parse either separator
let pASep = (attempt pFSep) <|> (attempt pRSep)
//ISA-16: Component Element Separator. Since this is not a data structure, we only need a function.
let pElSep : Parser<_> = skipChar ':'

let pPSep : Parser<_> = skipChar '.'

let ws = spaces

let pNbr l = manyMinMaxSatisfy l l isDigit .>> pASep
let invInf = System.Globalization.DateTimeFormatInfo.InvariantInfo

// Try parsing a date format and just fail parsing if there's an exception
let pTryDate (fmt : string) : Parser<DateTime, unit> = 
    pNbr (fmt.Length) 
    >>= fun d -> 
        try 
            (preturn (DateTime.ParseExact(d, fmt, invInf)))
        with :? FormatException as ex -> 
            fail 
                (String.concat " " 
                     [ "Could not parse date:"; d; "format:"; fmt ])

let pDate : Parser<DateTime> = 
    (attempt (pTryDate "yyyyMMdd")) <|> (attempt (pTryDate "yyMMdd"))
let pTime : Parser<DateTime> = 
    (attempt (pTryDate "HHmm")) <|> (attempt (pTryDate "HHmmss"))
let pDateTime = 
    pDate >>= fun d -> pTime >>= fun t -> preturn (d.Add(t.TimeOfDay))


