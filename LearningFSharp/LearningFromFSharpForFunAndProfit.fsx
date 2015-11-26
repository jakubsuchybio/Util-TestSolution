#light
//---------------------
// Section 'Why use F#?'
// http://fsharpforfunandprofit.com/why-use-fsharp/
module WhyUseFSharp =
    // one-liners
    [1..100] |> List.sum |> printfn "sum=%d"

    // no curly braces, semicolons or parentheses
    let square x = x * x
    let sq = square 42

    // simple types in one line
    type Person = {First:string; Last:string}

    // complex types in a few lines
    type Employee =
      | Worker of Person
      | Manager of Employee list

    // type inference
    let jdoe = {First="John";Last="Doe"}
    let worker = Worker jdoe

    // automatic equality and comparison
    type Person2 = {First:string; Last:string}
    let person1 = {First="john"; Last="Doe"}
    let person2 = {First="john"; Last="Doe"}
    printfn "Equal? %A"  (person1 = person2)

    // easy IDisposable logic with "use" keyword
    //use reader = new StreamReader()

    // easy composition of functions
    let add2times3 = (+) 2 >> (*) 3
    let result = add2times3 5

    // strict type checking
    //printfn "print string %s" 123 //compile error

    // all values immutable by default
    //person1.First <- "new name"  //assignment error

    // never have to check for nulls
    let makeNewString str =
       //str can always be appended to safely
       let newString = str + " new!"
       newString

    // embed business logic into types
    //emptyShoppingCart.remove   // compile error!

    // units of measure
    //let distance = 10<m> + 10<ft> // error!

    // easy async logic with "async" keyword
    //let! result = async {something}

    // easy parallelism
    //Async.Parallel [ for i in 0..40 ->
    //    async { return fib(i) } ]

    // message queues
    //MailboxProcessor.Start(fun inbox-> async{
    //    let! msg = inbox.Receive()
    //    printfn "message is: %s" msg
    //    })

    // impure code when needed
    let mutable counter = 0

    // create C# compatible classes and interfaces
    type IEnumerator<'a> =
        abstract member Current : 'a
        abstract MoveNext : unit -> bool

    // extension methods
    type System.Int32 with
        member this.IsEven = this % 2 = 0

    let i=20
    if i.IsEven then printfn "'%i' is even" i

    // UI code
    //open System.Windows.Forms
    //let form =
    //    new Form(
    //        Width= 400, Height = 300, Visible = true, Text = "Hello World"
    //        )
    //form.TopMost <- true
    //form.Click.Add (fun args-> printfn "clicked!")
    //form.Show()

//---------------------
// Section 'Why use F#?' Series no. 1
// http://fsharpforfunandprofit.com/posts/why-use-fsharp-intro/
// No Code

//---------------------
// Section 'Why use F#?' Series no. 2
// http://fsharpforfunandprofit.com/posts/fsharp-in-60-seconds/

module WhyUseFSharpSeries2 =
    // single line comments use a double slash
    (* multi line comments use (* . . . *) pair

    -end of multi line comment- *)

    // ======== "Variables" (but not really) ==========
    // The "let" keyword defines an (immutable) value
    let myInt = 5
    let myFloat = 3.14
    let myString = "hello"   //note that no types needed

    // ======== Lists ============
    let twoToFive = [2;3;4;5]        // Square brackets create a list with
                                     // semicolon delimiters.
    let oneToFive = 1 :: twoToFive   // :: creates list with new 1st element
    // The result is [1;2;3;4;5]
    let zeroToFive = [0;1] @ twoToFive   // @ concats two lists

    // IMPORTANT: commas are never used as delimiters, only semicolons!

    // ======== Functions ========
    // The "let" keyword also defines a named function.
    let square x = x * x          // Note that no parens are used.
    square 3                      // Now run the function. Again, no parens.

    let add x y = x + y           // don't use add (x,y)! It means something
                                  // completely different.
    add 2 3                       // Now run the function.

    // to define a multiline function, just use indents. No semicolons needed.
    let evens list =
       let isEven x = x%2 = 0     // Define "isEven" as an inner ("nested") function
       List.filter isEven list    // List.filter is a library function
                                  // with two parameters: a boolean function
                                  // and a list to work on

    evens oneToFive               // Now run the function

    // You can use parens to clarify precedence. In this example,
    // do "map" first, with two args, then do "sum" on the result.
    // Without the parens, "List.map" would be passed as an arg to List.sum
    let sumOfSquaresTo100 =
       List.sum ( List.map square [1..100] )

    // You can pipe the output of one operation to the next using "|>"
    // Here is the same sumOfSquares function written using pipes
    let sumOfSquaresTo100piped =
       [1..100] |> List.map square |> List.sum  // "square" was defined earlier

    // you can define lambdas (anonymous functions) using the "fun" keyword
    let sumOfSquaresTo100withFun =
       [1..100] |> List.map (fun x->x*x) |> List.sum

    // In F# returns are implicit -- no "return" needed. A function always
    // returns the value of the last expression used.

    // ======== Pattern Matching ========
    // Match..with.. is a supercharged case/switch statement.
    let simplePatternMatch =
       let x = "a"
       match x with
        | "a" -> printfn "x is a"
        | "b" -> printfn "x is b"
        | _ -> printfn "x is something else"   // underscore matches anything

    // Some(..) and None are roughly analogous to Nullable wrappers
    let validValue = Some(99)
    let invalidValue = None

    // In this example, match..with matches the "Some" and the "None",
    // and also unpacks the value in the "Some" at the same time.
    let optionPatternMatch input =
       match input with
        | Some i -> printfn "input is an int=%d" i
        | None -> printfn "input is missing"

    optionPatternMatch validValue
    optionPatternMatch invalidValue

    // ========= Complex Data Types =========

    // Tuple types are pairs, triples, etc. Tuples use commas.
    let twoTuple = 1,2
    let threeTuple = "a",2,true

    // Record types have named fields. Semicolons are separators.
    type Person = {First:string; Last:string}
    let person1 = {First="john"; Last="Doe"}

    // Union types have choices. Vertical bars are separators.
    type Temp =
        | DegreesC of float
        | DegreesF of float
    let temp = DegreesF 98.6

    // Types can be combined recursively in complex ways.
    // E.g. here is a union type that contains a list of the same type:
    type Employee =
      | Worker of Person
      | Manager of Employee list
    let jdoe = {First="John";Last="Doe"}
    let worker = Worker jdoe

    // ========= Printing =========
    // The printf/printfn functions are similar to the
    // Console.Write/WriteLine functions in C#.
    printfn "Printing an int %i, a float %f, a bool %b" 1 2.0 true
    printfn "A string %s, and something generic %A" "hello" [1;2;3;4]

    // all complex types have pretty printing built in
    printfn "twoTuple=%A,\nPerson=%A,\nTemp=%A,\nEmployee=%A"
             twoTuple person1 temp worker

    // There are also sprintf/sprintfn functions for formatting data
    // into a string, similar to String.Format.

//---------------------
// Section 'Why use F#?' Series no. 3
// http://fsharpforfunandprofit.com/posts/fvsc-sum-of-squares/
// define the square function
module WhyUseFSharpSeries3 =
    let square x = x * x

    // define the sumOfSquares function
    let sumOfSquares n =
       [1..n] |> List.map square |> List.sum

    // try it
    sumOfSquares 100

    let squareF x = x * x

    // define the sumOfSquares function
    let sumOfSquaresF n =
       [1.0 .. n] |> List.map squareF |> List.sum  // "1.0" is a float

    sumOfSquaresF 100.0

//---------------------
// Section 'Why use F#?' Series no. 4
// http://fsharpforfunandprofit.com/posts/fvsc-quicksort/
module WhyUseFSharpSeries4 =
    let rec quicksort list =
       match list with
       | [] ->                            // If the list is empty
            []                            // return an empty list
       | firstElem::otherElements ->      // If the list is not empty
            let smallerElements =         // extract the smaller ones
                otherElements
                |> List.filter (fun e -> e < firstElem)
                |> quicksort              // and sort them
            let largerElements =          // extract the large ones
                otherElements
                |> List.filter (fun e -> e >= firstElem)
                |> quicksort              // and sort them
            // Combine the 3 parts into a new list and return it
            List.concat [smallerElements; [firstElem]; largerElements]

    //test
    printfn "%A" (quicksort [1;5;23;18;9;1;3])

    let rec quicksort2 = function
       | [] -> []
       | first::rest ->
            let smaller,larger = List.partition ((>=) first) rest
            List.concat [quicksort2 smaller; [first]; quicksort2 larger]

    // test code
    printfn "%A" (quicksort2 [1;5;23;18;9;1;3])

//---------------------
// Section 'Why use F#?' Series no. 5
// http://fsharpforfunandprofit.com/posts/fvsc-download/
// "open" brings a .NET namespace into visibility
module WhyUseFSharpSeries5 =
    open System
    open System.IO
    open System.Net

    // Fetch the contents of a web page
    let fetchUrl callback url =
        let req = WebRequest.Create(Uri(url))
        use resp = req.GetResponse()
        use stream = resp.GetResponseStream()
        use reader = new IO.StreamReader(stream)
        callback reader url


    let myCallback (reader:IO.StreamReader) url =
        let html = reader.ReadToEnd()
        let html1000 = html.Substring(0,1000)
        printfn "Downloaded %s. First 1000 is %s" url html1000
        html      // return all the html

    //test
    let google = fetchUrl myCallback "http://google.com"

    // build a function with the callback "baked in"
    let fetchUrl2 = fetchUrl myCallback

    // test
    let google2 = fetchUrl2 "http://www.google.com"
    let bbc    = fetchUrl2 "http://news.bbc.co.uk"

    // test with a list of sites
    let sites = ["http://www.bing.com";
                 "http://www.google.com";
                 "http://www.yahoo.com"]

    // process each site in the list
    sites |> List.map fetchUrl2

//---------------------
// Section 'Why use F#?' Series no. 6
// http://fsharpforfunandprofit.com/posts/key-concepts/
module WhyUseFSharpSeries6 =
    let square x = x * x

    // functions as values
    let squareclone = square
    let result = [1..10] |> List.map squareclone

    // functions taking other functions as parameters
    let execFunction aFunc aParam = aFunc aParam
    let result2 = execFunction square 12


    //declare it
    type IntAndBool = {intPart: int; boolPart: bool}

    //use it
    let x = {intPart=1; boolPart=false}

    //declare it
    type IntOrBool =
        | IntChoice of int
        | BoolChoice of bool

    //use it
    let y = IntChoice 42
    let z = BoolChoice true

// if-like style
//    match booleanExpression with
//        true -> // true branch
//        false -> // false branch

// switch-like style
//    match aDigit with
//    | 1 -> // Case when digit=1
//    | 2 -> // Case when digit=2
//    | _ -> // Case otherwise

// loop-like style
//    match aList with
//    | [] ->
//         // Empty case
//    | first::rest ->
//         // Case with at least one element.
//         // Process first element, and then call
//         // recursively with the rest of the list

    type Shape =        // define a "union" of alternative structures
    | Circle of int
    | Rectangle of int * int
    | Polygon of (int * int) list
    | Point of (int * int)

    let draw shape =    // define a function "draw" with a shape param
      match shape with
      | Circle radius ->
          printfn "The circle has a radius of %d" radius
      | Rectangle (height,width) ->
          printfn "The rectangle is %d high by %d wide" height width
      | Polygon points ->
          printfn "The polygon is made of these points %A" points
      | _ -> printfn "I don't recognize this shape"

    let circle = Circle(10)
    let rect = Rectangle(4,5)
    let polygon = Polygon( [(1,1); (2,2); (3,3)])
    let point = Point(2,3)

    [circle; rect; polygon; point] |> List.iter draw

//---------------------
// Section 'Why use F#?' Series no. 7
// http://fsharpforfunandprofit.com/posts/conciseness-intro/
// No Code


//---------------------
// Section 'Why use F#?' Series no. 8
// http://fsharpforfunandprofit.com/posts/conciseness-type-inference/
module WhyUseFSharpSeries8 =
    let Where source predicate =
        //use the standard F# implementation
        Seq.filter predicate source

    let GroupBy source keySelector =
        //use the standard F# implementation
        Seq.groupBy keySelector source

    let i  = 1
    let s = "hello"
    let tuple = s,i      // pack into tuple
    let s2,i2 = tuple    // unpack
    let list = [s2]       // type is string list

    let sumLengths strList =
        strList |> List.map String.length |> List.sum

    // function type is: string list -> int

//---------------------
// Section 'Why use F#?' Series no. 9
// http://fsharpforfunandprofit.com/posts/conciseness-type-definitions/
module WhyUseFSharpSeries9 =
    open System

    // some "record" types
    type Person = {FirstName:string; LastName:string; Dob:DateTime}
    type Coord = {Lat:float; Long:float}

    // some "union" (choice) types
    type TimePeriod = Hour | Day | Week | Year
    type Temperature = C of int | F of int
    type Appointment = OneTime of DateTime
                       | Recurring of DateTime list

                       type PersonalName = {FirstName:string; LastName:string}

    // Addresses
    type StreetAddress = {Line1:string; Line2:string; Line3:string }

    type ZipCode =  ZipCode of string
    type StateAbbrev =  StateAbbrev of string
    type ZipAndState =  {State:StateAbbrev; Zip:ZipCode }
    type USAddress = {Street:StreetAddress; Region:ZipAndState}

    type UKPostCode =  PostCode of string
    type UKAddress = {Street:StreetAddress; Region:UKPostCode}

    type InternationalAddress = {
        Street:StreetAddress; Region:string; CountryName:string}

    // choice type  -- must be one of these three specific types
    type Address = USAddress | UKAddress | InternationalAddress

    // Email
    type Email = Email of string

    // Phone
    type CountryPrefix = Prefix of int
    type Phone = {CountryPrefix:CountryPrefix; LocalNumber:string}

    type Contact =
        {
        PersonalName: PersonalName;
        // "option" means it might be missing
        Address: Address option;
        Email: Email option;
        Phone: Phone option;
        }

    // Put it all together into a CustomerAccount type
    type CustomerAccountId  = AccountId of string
    type CustomerType  = Prospect | Active | Inactive

    // override equality and deny comparison
    [<CustomEquality; NoComparison>]
    type CustomerAccount =
        {
        CustomerAccountId: CustomerAccountId;
        CustomerType: CustomerType;
        ContactInfo: Contact;
        }

        override this.Equals(other) =
            match other with
            | :? CustomerAccount as otherCust ->
              (this.CustomerAccountId = otherCust.CustomerAccountId)
            | _ -> false

        override this.GetHashCode() = hash this.CustomerAccountId

//---------------------
// Section 'Why use F#?' Series no. 10
// http://fsharpforfunandprofit.com/posts/conciseness-extracting-boilerplate/
module WhyUseFSharpSeries10 =
    let product n =
        let initialValue = 1
        let action productSoFar x = productSoFar * x
        [1..n] |> List.fold action initialValue

    //test
    product 10

    let sumOfOdds n =
        let initialValue = 0
        let action sumSoFar x = if x%2=0 then sumSoFar else sumSoFar+x
        [1..n] |> List.fold action initialValue

    //test
    sumOfOdds 10

    let alternatingSum n =
        let initialValue = (true,0)
        let action (isNeg,sumSoFar) x = if isNeg then (false,sumSoFar-x)
                                                 else (true ,sumSoFar+x)
        [1..n] |> List.fold action initialValue |> snd

    //test
    alternatingSum 100

    let sumOfSquaresWithFold n =
        let initialValue = 0
        let action sumSoFar x = sumSoFar + (x*x)
        [1..n] |> List.fold action initialValue

    //test
    sumOfSquaresWithFold 100

    type NameAndSize= {Name:string;Size:int}

    let maxNameAndSize list =

        let innerMaxNameAndSize initialValue rest =
            let action maxSoFar x = if maxSoFar.Size < x.Size then x else maxSoFar
            rest |> List.fold action initialValue

        // handle empty lists
        match list with
        | [] ->
            None
        | first::rest ->
            let max = innerMaxNameAndSize first rest
            Some max

    //test
    let list = [
        {Name="Alice"; Size=10}
        {Name="Bob"; Size=1}
        {Name="Carol"; Size=12}
        {Name="David"; Size=5}
        ]
    maxNameAndSize list
    maxNameAndSize []

    let maxNameAndSize2 list =
        match list with
        | [] ->
            None
        | _ ->
            let max = list |> List.maxBy (fun item -> item.Size)
            Some max

//---------------------
// Section 'Why use F#?' Series no. 11
module WhyUseFSharpSeries11 =
    // building blocks
    let add2 x = x + 2
    let mult3 x = x * 3
    let square x = x * x

    // test
    [1..10] |> List.map add2 |> printfn "%A"
    [1..10] |> List.map mult3 |> printfn "%A"
    [1..10] |> List.map square |> printfn "%A"

    // new composed functions
    let add2ThenMult3 = add2 >> mult3
    let mult3ThenSquare = mult3 >> square
    // test
    add2ThenMult3 5
    mult3ThenSquare 5
    [1..10] |> List.map add2ThenMult3 |> printfn "%A"
    [1..10] |> List.map mult3ThenSquare |> printfn "%A"

    // helper functions;
    let logMsg msg x = printf "%s%i" msg x; x     //without linefeed
    let logMsgN msg x = printfn "%s%i" msg x; x   //with linefeed

    // new composed function with new improved logging!
    let mult3ThenSquareLogged =
       logMsg "before="
       >> mult3
       >> logMsg " after mult3="
       >> square
       >> logMsgN " result="

    // test
    mult3ThenSquareLogged 5
    [1..10] |> List.map mult3ThenSquareLogged //apply to a whole list

    let listOfFunctions = [
       mult3;
       square;
       add2;
       logMsgN "result=";
       ]

    // compose all functions in the list into a single one
    let allFunctions = List.reduce (>>) listOfFunctions

    //test
    allFunctions 5

    // set up the vocabulary
    type DateScale = Hour | Hours | Day | Days | Week | Weeks
    type DateDirection = Ago | Hence

    // define a function that matches on the vocabulary
    let getDate interval scale direction =
        let absHours = match scale with
                       | Hour | Hours -> 1 * interval
                       | Day | Days -> 24 * interval
                       | Week | Weeks -> 24 * 7 * interval
        let signedHours = match direction with
                          | Ago -> -1 * absHours
                          | Hence ->  absHours
        System.DateTime.Now.AddHours(float signedHours)

    // test some examples
    let example1 = getDate 5 Days Ago
    let example2 = getDate 1 Hour Hence

    // create an underlying type
    type FluentShape = {
        label : string;
        color : string;
        onClick : FluentShape->FluentShape // a function type
        }

    let defaultShape =
        {label=""; color=""; onClick=fun shape->shape}

    let click shape =
        shape.onClick shape

    let display shape =
        printfn "My label=%s and my color=%s" shape.label shape.color
        shape   //return same shape

    let setLabel label shape =
       {shape with FluentShape.label = label}

    let setColor color shape =
       {shape with FluentShape.color = color}

    //add a click action to what is already there
    let appendClickAction action shape =
       {shape with FluentShape.onClick = shape.onClick >> action}

    // Compose two "base" functions to make a compound function.
    let setRedBox = setColor "red" >> setLabel "box"

    // Create another function by composing with previous function.
    // It overrides the color value but leaves the label alone.
    let setBlueBox = setRedBox >> setColor "blue"

    // Make a special case of appendClickAction
    let changeColorOnClick color = appendClickAction (setColor color)

    //setup some test values
    let redBox = defaultShape |> setRedBox
    let blueBox = defaultShape |> setBlueBox

    // create a shape that changes color when clicked
    redBox
        |> display
        |> changeColorOnClick "green"
        |> click
        |> display  // new version after the click

    // create a shape that changes label and color when clicked
    blueBox
        |> display
        |> appendClickAction (setLabel "box2" >> setColor "green")
        |> click
        |> display  // new version after the click

    let rainbow =
        ["red";"orange";"yellow";"green";"blue";"indigo";"violet"]

    let showRainbow =
        let setColorAndDisplay color = setColor color >> display
        rainbow
        |> List.map setColorAndDisplay
        |> List.reduce (>>)

    // test the showRainbow function
    defaultShape |> showRainbow

//---------------------
// Section 'Why use F#?' Series no. 12
module WhyUseFSharpSeries12 =
    //matching tuples directly
    let first, second, _ =  (1,2,3)  // underscore means ignore

    //matching lists directly
    let e1::e2::rest = [1..10]       // ignore the warning for now

    //matching lists inside a match..with
    let listMatcher aList =
        match aList with
        | [] -> printfn "the list is empty"
        | [first] -> printfn "the list has one element %A " first
        | [first; second] -> printfn "list is %A and %A" first second
        | _ -> printfn "the list has more than two elements"

    listMatcher [1;2;3;4]
    listMatcher [1;2]
    listMatcher [1]
    listMatcher []

    // create some types
    type Address = { Street: string; City: string; }
    type Customer = { ID: int; Name: string; Address: Address}

    // create a customer
    let customer1 = { ID = 1; Name = "Bob";
          Address = {Street="123 Main"; City="NY" } }

    // extract name only
    let { Name=name1 } =  customer1
    printfn "The customer is called %s" name1

    // extract name and id
    let { ID=id2; Name=name2; } =  customer1
    printfn "The customer called %s has id %i" name2 id2

    // extract name and address
    let { Name=name3;  Address={Street=street3}  } =  customer1
    printfn "The customer is called %s and lives on %s" name3 street3


//---------------------
// Section 'Why use F#?' Series no. 13
// No Code


//---------------------
// Section 'Why use F#?' Series no. 14
module WhyUseFSharpSeries14 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 15
module WhyUseFSharpSeries15 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 16
module WhyUseFSharpSeries16 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 17
module WhyUseFSharpSeries17 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 18
module WhyUseFSharpSeries18 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 19
module WhyUseFSharpSeries19 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 20
module WhyUseFSharpSeries20 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 21
module WhyUseFSharpSeries21 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 22
module WhyUseFSharpSeries22 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 23
module WhyUseFSharpSeries23 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 24
module WhyUseFSharpSeries24 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 25
module WhyUseFSharpSeries25 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 26
module WhyUseFSharpSeries26 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 27
module WhyUseFSharpSeries27 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 28
module WhyUseFSharpSeries28 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 29
module WhyUseFSharpSeries29 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 30
module WhyUseFSharpSeries30 =
    let x = 0


//---------------------