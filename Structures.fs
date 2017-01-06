module BrandX.Structures

open FParsec
open System

// Parse and EDI record value into a record type tag and then a sequence of fields
type Parser<'t> = Parser<'t, unit>

// The field separator
let pFSep : Parser<_> = skipChar '*' <?> "Field Separator"
// Optional fields are separated by two '*'
let pOFSep : Parser<_> = skipString "**" <?> "Double Field Separator"
//The triple stars field separator
let pTSep : Parser<_> = skipString "***" <?> "Triple Field Separator"
// The record delimiter
let pRSep : Parser<_> = skipChar '~' <?> "Record Separator"
// Parse either separator
let pASep = 
    (attempt pFSep) <|> (attempt pRSep) <?> "Field or Record Separator"
//ISA-16: Component Element Separator. Since this is not a data structure, we only need a function.
let pElSep : Parser<_> = skipChar ':' <?> "Semicolon"
let pPSep : Parser<_> = skipChar '.' <?> "Dot"
let ws : Parser<_> = spaces <?> "Spaces"
let pNbr l = manyMinMaxSatisfy l l isDigit .>> pASep <?> "Number"
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
    <?> "Date"
let pTime : Parser<DateTime> = 
    (attempt (pTryDate "HHmm")) <|> (attempt (pTryDate "HHmmss")) <?> "Time"
let pDateTime = 
    pDate 
    >>= fun d -> 
        pTime >>= fun t -> preturn (d.Add(t.TimeOfDay)) <?> "DateTime"
