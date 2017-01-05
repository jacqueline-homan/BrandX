
//ST-01
type TrnsSetIdCode = 
    | MotorCarrierLoadTender

let pTrnsSetIdCode : Parser<TrnsSetIdCode> = skipString "204" >>. preturn MotorCarrierLoadTender .>> pFSep

//ST-02
type TrnSetCtrlNo = 
    | TrnSetCtrlNo of string 

let pCode : Parser<TrnSetCtrlNo> = anyString 3 |>> TrnSetCtrlNo .>> pRSep

type TransCode =
    { idCode : TrnsSetIdCode
      ctrlNo : TrnSetCtrlNo}

let pctrl : Parser<TrnSetCtrlNo> = manyMinMaxSatisfy 4 9 (fun c -> isDigit c || isAsciiLetter c || isAnyOf "~" c) .>> pFSep |>> TrnSetCtrlNo 

let pTransCode = 
    pipe2 pTrnsSetIdCode pCode (fun t c ->
        {idCode = t
         ctrlNo = c}) 
        
type ST = ST of TrnsSetIdCode * TrnSetCtrlNo * TransCode

let pST = 
    skipString "ST" >>. pFSep >>. pTrnsSetIdCode
    >>= fun p -> 
        pCode
        >>= fun q ->
            pctrl
            >>= fun r ->
                pTransCode
                >>= fun s ->
                preturn (ST(p, q, s))


//N1-01
type EntId =
    | BillToParty

let pEntId : Parser<EntId> = skipString "BT" >>. preturn BillToParty .>> pFSep

//N1-02
type Name = 
    | Name of string 

let pName : Parser<Name> = manyMinMaxSatisfy 1 60 (isNoneOf "~") |>> Name .>> pFSep

//N1-03
type IdCodeQual =
    | IdCodeQual of string

let pIdCodeQual : Parser<IdCodeQual> = stringReturn "93" IdCodeQual .>> pFSep

//N1-04
type IdCode =
    | IdCode of string 

let pIdCode : Parser<IdCode> = 
    manyMinMaxSatisfy 2 80 (isNoneOf "~") |>> IdCode .>> pRSep

type N1 = N1 of EntId * Name * IdCodeQual * IdCode
let pN1 = 
    skipString "N1" >>. pFSep >>. pEntId
    >>= fun a -> 
        pName
        >>= fun b ->
            pIdCodeQual 
            >>= fun c ->
                pIdCode
                >>= fun d ->
                        preturn (N1(a, b, c, d))




