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
    type PersonalName = {FirstName:string; LastName:string}
    type USAddress =
       {Street:string; City:string; State:string; Zip:string}
    type UKAddress =
       {Street:string; Town:string; PostCode:string}
    type Address = US of USAddress | UK of UKAddress
    type Person =
       {Name:string; Address:Address}

    let alice = {
       Name="Alice";
       Address=US {Street="123 Main";City="LA";State="CA";Zip="91201"}}
    let bob = {
       Name="Bob";
       Address=UK {Street="221b Baker St";Town="London";PostCode="NW1 6XE"}}

    printfn "Alice is %A" alice
    printfn "Bob is %A" bob

    let alice1 = {FirstName="Alice"; LastName="Adams"}
    let alice2 = {FirstName="Alice"; LastName="Adams"}
    let bob1 = {FirstName="Bob"; LastName="Bishop"}

    //test
    printfn "alice1=alice2 is %A" (alice1=alice2)
    printfn "alice1=bob1 is %A" (alice1=bob1)

    type Suit = Club | Diamond | Spade | Heart
    type Rank = Two | Three | Four | Five | Six | Seven | Eight
                | Nine | Ten | Jack | Queen | King | Ace

    let compareCard card1 card2 =
        if card1 < card2
        then printfn "%A is greater than %A" card2 card1
        else printfn "%A is greater than %A" card1 card2

    let aceHearts = Heart, Ace
    let twoHearts = Heart, Two
    let aceSpades = Spade, Ace

    compareCard aceHearts twoHearts
    compareCard twoHearts aceSpades

    let hand = [ Club,Ace; Heart,Three; Heart,Ace;
                 Spade,Jack; Diamond,Two; Diamond,Ace ]

    //instant sorting!
    List.sort hand |> printfn "sorted hand is (low to high) %A"
    List.max hand |> printfn "high card is %A"
    List.min hand |> printfn "low card is %A"

//---------------------
// Section 'Why use F#?' Series no. 15
// http://fsharpforfunandprofit.com/posts/convenience-functions-as-interfaces/
module WhyUseFSharpSeries15 =
    let addingCalculator input = input + 1

    let loggingCalculator innerCalculator input =
       printfn "input is %A" input
       let result = innerCalculator input
       printfn "result is %A" result
       result

    let add1 input = input + 1
    let times2 input = input * 2

    let genericLogger anyFunc input =
       printfn "input is %A" input   //log the input
       let result = anyFunc input    //evaluate the function
       printfn "result is %A" result //log the result
       result                        //return the result

    let add1WithLogging = genericLogger add1
    let times2WithLogging = genericLogger times2

    // test
    add1WithLogging 3
    times2WithLogging 3

    [1..5] |> List.map add1WithLogging

    let genericTimer anyFunc input =
       let stopwatch = System.Diagnostics.Stopwatch()
       stopwatch.Start()
       let result = anyFunc input  //evaluate the function
       System.Threading.Thread.Sleep(100)
       stopwatch.Stop()
       printfn "elapsed ms is %A" stopwatch.ElapsedMilliseconds
       result

    let add1WithTimer = genericTimer add1WithLogging

    // test
    add1WithTimer 3

    type Animal(noiseMakingStrategy) =
        member this.MakeNoise =
            noiseMakingStrategy() |> printfn "Making noise %s"

    // now create a cat
    let meowing() = "Meow"
    let cat = Animal(meowing)
    cat.MakeNoise

    // .. and a dog
    let woofOrBark() = if (System.DateTime.Now.Second % 2 = 0)
                       then "Woof" else "Bark"
    let dog = Animal(woofOrBark)
    dog.MakeNoise
    dog.MakeNoise  //try again a second later


//---------------------
// Section 'Why use F#?' Series no. 16
// http://fsharpforfunandprofit.com/posts/convenience-partial-application/
module WhyUseFSharpSeries16 =
    // define a adding function
    let add x y = x + y

    // normal use
    let z = add 1 2

    let add42 = add 42
    // use the new function
    add42 2
    add42 3

    let genericLogger before after anyFunc input =
        before input               //callback for custom behavior
        let result = anyFunc input //evaluate the function
        after result               //callback for custom behavior
        result                     //return the result


    let add1 input = input + 1

    // reuse case 1
    genericLogger
        (fun x -> printf "before=%i. " x) // function to call before
        (fun x -> printfn " after=%i." x) // function to call after
        add1                              // main function
        2                                 // parameter

    // reuse case 2
    genericLogger
        (fun x -> printf "started with=%i " x) // different callback
        (fun x -> printfn " ended with=%i" x)
        add1                              // main function
        2                                 // parameter

    // define a reusable function with the "callback" functions fixed
    let add1WithConsoleLogging =
        genericLogger
            (fun x -> printf "input=%i. " x)
            (fun x -> printfn " result=%i" x)
            add1
            // last parameter NOT defined here yet!

    add1WithConsoleLogging 2
    add1WithConsoleLogging 3
    add1WithConsoleLogging 4
    [1..5] |> List.map add1WithConsoleLogging


//---------------------
// Section 'Why use F#?' Series no. 17
// http://fsharpforfunandprofit.com/posts/convenience-active-patterns/
module WhyUseFSharpSeries17 =
    // create an active pattern
    let (|Int|_|) str =
       match System.Int32.TryParse(str) with
       | (true,int) -> Some(int)
       | _ -> None

    // create an active pattern
    let (|Bool|_|) str =
       match System.Boolean.TryParse(str) with
       | (true,bool) -> Some(bool)
       | _ -> None

    // create a function to call the patterns
    let testParse str =
        match str with
        | Int i -> printfn "The value is an int '%i'" i
        | Bool b -> printfn "The value is a bool '%b'" b
        | _ -> printfn "The value '%s' is something else" str

    // test
    testParse "12"
    testParse "true"
    testParse "abc"

    // create an active pattern
    open System.Text.RegularExpressions
    let (|FirstRegexGroup|_|) pattern input =
       let m = Regex.Match(input,pattern)
       if (m.Success) then Some m.Groups.[1].Value else None

    // create a function to call the pattern
    let testRegex str =
        match str with
        | FirstRegexGroup "http://(.*?)/(.*)" host ->
               printfn "The value is a url and the host is %s" host
        | FirstRegexGroup ".*?@(.*)" host ->
               printfn "The value is an email and the host is %s" host
        | _ -> printfn "The value '%s' is something else" str

    // test
    testRegex "http://google.com/test"
    testRegex "alice@hotmail.com"

    // setup the active patterns
    let (|MultOf3|_|) i = if i % 3 = 0 then Some MultOf3 else None
    let (|MultOf5|_|) i = if i % 5 = 0 then Some MultOf5 else None

    // the main function
    let fizzBuzz i =
        match i with
        | MultOf3 & MultOf5 -> printf "FizzBuzz, "
        | MultOf3 -> printf "Fizz, "
        | MultOf5 -> printf "Buzz, "
        | _ -> printf "%i, " i

    // test
    [1..20] |> List.iter fizzBuzz

//---------------------
// Section 'Why use F#?' Series no. 18
// http://fsharpforfunandprofit.com/posts/correctness-intro/
// No Code

//---------------------
// Section 'Why use F#?' Series no. 19
// http://fsharpforfunandprofit.com/posts/correctness-immutability/
module WhyUseFSharpSeries19 =
    // immutable list
    let list = [1;2;3;4]

    type PersonalName = {FirstName:string; LastName:string}
    // immutable person
    let john = {FirstName="John"; LastName="Doe"}
    let alice = {john with FirstName="Alice"}

    // create an immutable list
    let list1 = [1;2;3;4]

    // prepend to make a new list
    let list2 = 0::list1

    // get the last 4 of the second list
    let list3 = list2.Tail

    // the two lists are the identical object in memory!
    System.Object.ReferenceEquals(list1,list3)

//---------------------
// Section 'Why use F#?' Series no. 20
// http://fsharpforfunandprofit.com/posts/correctness-exhaustive-pattern-matching/
module WhyUseFSharpSeries20 =
    type State = New | Draft | Published | Inactive | Discontinued
    let handleState state =
       match state with
       | Inactive -> () // code for Inactive
       | Draft -> () // code for Draft
       | New -> () // code for New
       | Discontinued -> () // code for Discontinued

    let getFileInfo filePath =
        let fi = new System.IO.FileInfo(filePath)
        if fi.Exists then Some(fi) else None

    let goodFileName2 = "good.txt"
    let badFileName2 = "bad.txt"

    let goodFileInfo = getFileInfo goodFileName2 // Some(fileinfo)
    let badFileInfo = getFileInfo badFileName2   // None

    match goodFileInfo with
        | Some fileInfo ->
            printfn "the file %s exists" fileInfo.FullName
        | None ->
            printfn "the file doesn't exist"

    match badFileInfo with
        | Some fileInfo ->
            printfn "the file %s exists" fileInfo.FullName
        | None ->
            printfn "the file doesn't exist"

    let rec movingAverages list =
        match list with
        // if input is empty, return an empty list
        | [] -> []
        // otherwise process pairs of items from the input
        | x::y::rest ->
            let avg = (x+y)/2.0
            //build the result by recursing the rest of the list
            avg :: movingAverages (y::rest)
        // for one item, return an empty list
        | [_] -> []

    // test
    movingAverages [1.0]
    movingAverages [1.0; 2.0]
    movingAverages [1.0; 2.0; 3.0]

    // define a "union" of two different alternatives
    type Result<'a, 'b> =
        | Success of 'a  // 'a means generic type. The actual type
                         // will be determined when it is used.
        | Failure of 'b  // generic failure type as well

//    type Result<'a, 'b> =
//        | Success of 'a
//        | Failure of 'b
//        | Indeterminate

//    type Result<'a> =
//        | Success of 'a

    // define all possible errors
    type FileErrorReason =
        | FileNotFound of string
        | UnauthorizedAccess of string * System.Exception

    // define a low level function in the bottom layer
    let performActionOnFile action filePath =
       try
          //open file, do the action and return the result
          use sr = new System.IO.StreamReader(filePath:string)
          let result = action sr  //do the action to the reader
          sr.Close()
          Success (result)        // return a Success
       with      // catch some exceptions and convert them to errors
          | :? System.IO.FileNotFoundException as ex
              -> Failure (FileNotFound filePath)
          | :? System.Security.SecurityException as ex
              -> Failure (UnauthorizedAccess (filePath,ex))
          // other exceptions are unhandled

    // a function in the middle layer
    let middleLayerDo action filePath =
        let fileResult = performActionOnFile action filePath
        // do some stuff
        fileResult //return

    // a function in the top layer
    let topLayerDo action filePath =
        let fileResult = middleLayerDo action filePath
        // do some stuff
        fileResult //return

    /// get the first line of the file
    let printFirstLineOfFile filePath =
        let fileResult = topLayerDo (fun fs->fs.ReadLine()) filePath

        match fileResult with
        | Success result ->
            // note type-safe string printing with %s
            printfn "first line is: '%s'" result
        | Failure reason ->
           match reason with  // must match EVERY reason
           | FileNotFound file ->
               printfn "File not found: %s" file
           | UnauthorizedAccess (file,_) ->
               printfn "You do not have access to the file: %s" file

    /// write some text to a file
    let writeSomeText filePath someText =
        use writer = new System.IO.StreamWriter(filePath:string)
        writer.WriteLine(someText:string)
        writer.Close()

    /// get the length of the text in the file
    let printLengthOfFile filePath =
       let fileResult =
         topLayerDo (fun fs->fs.ReadToEnd().Length) filePath

       match fileResult with
       | Success result ->
          // note type-safe int printing with %i
          printfn "length is: %i" result
       | Failure _ ->
          printfn "An error happened but I don't want to be specific"

    let goodFileName = "good.txt"
    let badFileName = "bad.txt"

    writeSomeText goodFileName "hello"

    printFirstLineOfFile goodFileName
    printLengthOfFile goodFileName

    printFirstLineOfFile badFileName
    printLengthOfFile badFileName

//---------------------
// Section 'Why use F#?' Series no. 21
// http://fsharpforfunandprofit.com/posts/correctness-type-checking/
module WhyUseFSharpSeries21 =
    //define a "safe" email address type
    type EmailAddress = EmailAddress of string

    //define a function that uses it
    let sendEmail (EmailAddress email) =
       printfn "sent an email to %s" email

    //try to send one
    let aliceEmail = EmailAddress "alice@example.com"
    sendEmail aliceEmail

    //try to send a plain string
//    sendEmail "bob@example.com"   //error

//    let printingExample =
//       printf "an int %i" 2                        // ok
//       printf "an int %i" 2.0                      // wrong type
//       printf "an int %i" "hello"                  // wrong type
//       printf "an int %i"                          // missing param
//
//       printf "a string %s" "hello"                // ok
//       printf "a string %s" 2                      // wrong type
//       printf "a string %s"                        // missing param
//       printf "a string %s" "he" "lo"              // too many params
//
//       printf "an int %i and string %s" 2 "hello"  // ok
//       printf "an int %i and string %s" "hello" 2  // wrong type
//       printf "an int %i and string %s" 2          // missing param

    let printAString x = printf "%s" x
    let printAnInt x = printf "%i" x

    // the result is:
    // val printAString : string -> unit  //takes a string parameter
    // val printAnInt : int -> unit       //takes an int parameter

    // define some measures
    [<Measure>]
    type cm

    [<Measure>]
    type inches

    [<Measure>]
    type feet =
       // add a conversion function
       static member toInches(feet : float<feet>) : float<inches> =
          feet * 12.0<inches/feet>

    // define some values
    let meter = 100.0<cm>
    let yard = 3.0<feet>

    //convert to different measure
    let yardInInches = feet.toInches(yard)

    // can't mix and match!
//    yard + meter

    // now define some currencies
    [<Measure>]
    type GBP

    [<Measure>]
    type USD

    let gbp10 = 10.0<GBP>
    let usd10 = 10.0<USD>
    gbp10 + gbp10             // allowed: same currency
//    gbp10 + usd10             // not allowed: different currency
//    gbp10 + 1.0               // not allowed: didn't specify a currency
    gbp10 + 1.0<_>            // allowed using wildcard

    open System
    let obj = new Object()
    let ex = new Exception()
//    let b = (obj = ex) // ERROR

    // deny comparison
    [<NoEquality; NoComparison>]
    type CustomerAccount = {CustomerAccountId: int}

    let x = {CustomerAccountId = 1}

//    x = x       // error!
    x.CustomerAccountId = x.CustomerAccountId // no error

//---------------------
// Section 'Why use F#?' Series no. 22
// http://fsharpforfunandprofit.com/posts/designing-for-correctness/
module WhyUseFSharpSeries22 =
    type CartItem = string    // placeholder for a more complicated type

    type EmptyState = NoItems // don't use empty list! We want to
                              // force clients to handle this as a
                              // separate case. E.g. "you have no
                              // items in your cart"

    type ActiveState = { UnpaidItems : CartItem list; }
    type PaidForState = { PaidItems : CartItem list;
                          Payment : decimal}

    type Cart =
        | Empty of EmptyState
        | Active of ActiveState
        | PaidFor of PaidForState

    // =============================
    // operations on empty state
    // =============================

    let addToEmptyState item =
       // returns a new Active Cart
       Cart.Active {UnpaidItems=[item]}

    // =============================
    // operations on active state
    // =============================

    let addToActiveState state itemToAdd =
       let newList = itemToAdd :: state.UnpaidItems
       Cart.Active {state with UnpaidItems=newList }

    let removeFromActiveState state itemToRemove =
       let newList = state.UnpaidItems
                     |> List.filter (fun i -> i<>itemToRemove)

       match newList with
       | [] -> Cart.Empty NoItems
       | _ -> Cart.Active {state with UnpaidItems=newList}

    let payForActiveState state amount =
       // returns a new PaidFor Cart
       Cart.PaidFor {PaidItems=state.UnpaidItems; Payment=amount}

    type EmptyState with
       member this.Add = addToEmptyState

    type ActiveState with
       member this.Add = addToActiveState this
       member this.Remove = removeFromActiveState this
       member this.Pay = payForActiveState this

    let addItemToCart cart item =
       match cart with
       | Empty state -> state.Add item
       | Active state -> state.Add item
       | PaidFor state ->
           printfn "ERROR: The cart is paid for"
           cart

    let removeItemFromCart cart item =
       match cart with
       | Empty state ->
          printfn "ERROR: The cart is empty"
          cart   // return the cart
       | Active state ->
          state.Remove item
       | PaidFor state ->
          printfn "ERROR: The cart is paid for"
          cart   // return the cart

    let displayCart cart  =
       match cart with
       | Empty state ->
          printfn "The cart is empty"   // can't do state.Items
       | Active state ->
          printfn "The cart contains %A unpaid items"
                                                    state.UnpaidItems
       | PaidFor state ->
          printfn "The cart contains %A paid items. Amount paid: %f"
                                        state.PaidItems state.Payment

    type Cart with
       static member NewCart = Cart.Empty NoItems
       member this.Add = addItemToCart this
       member this.Remove = removeItemFromCart this
       member this.Display = displayCart this

    let emptyCart = Cart.NewCart
    printf "emptyCart="; emptyCart.Display

    let cartA = emptyCart.Add "A"
    printf "cartA="; cartA.Display

    let cartAB = cartA.Add "B"
    printf "cartAB="; cartAB.Display

    let cartB = cartAB.Remove "A"
    printf "cartB="; cartB.Display

    let emptyCart2 = cartB.Remove "B"
    printf "emptyCart2="; emptyCart2.Display

    let emptyCart3 = emptyCart2.Remove "B"    //error
    printf "emptyCart3="; emptyCart3.Display

    //  try to pay for cartA
    let cartAPaid =
        match cartA with
        | Empty _ | PaidFor _ -> cartA
        | Active state -> state.Pay 100m
    printf "cartAPaid="; cartAPaid.Display

    //  try to pay for emptyCart
    let emptyCartPaid =
        match emptyCart with
        | Empty _ | PaidFor _ -> emptyCart
        | Active state -> state.Pay 100m
    printf "emptyCartPaid="; emptyCartPaid.Display

    //  try to pay for cartAB
    let cartABPaid =
        match cartAB with
        | Empty _ | PaidFor _ -> cartAB // return the same cart
        | Active state -> state.Pay 100m

    //  try to pay for cartAB again
    let cartABPaidAgain =
        match cartABPaid with
        | Empty _ | PaidFor _ -> cartABPaid  // return the same cart
        | Active state -> state.Pay 100m

//    match cartABPaid with
//    | Empty state -> state.Pay 100m
//    | PaidFor state -> state.Pay 100m
//    | Active state -> state.Pay 100m

//---------------------
// Section 'Why use F#?' Series no. 23
// No Code

//---------------------
// Section 'Why use F#?' Series no. 24
module WhyUseFSharpSeries24 =
    open System

    let userTimerWithCallback =
        // create a event to wait on
        let event = new System.Threading.AutoResetEvent(false)

        // create a timer and add an event handler that will signal the event
        let timer = new System.Timers.Timer(2000.0)
        timer.Elapsed.Add (fun _ -> event.Set() |> ignore )

        //start
        printfn "Waiting for timer at %O" DateTime.Now.TimeOfDay
        timer.Start()

        // keep working
        printfn "Doing something useful while waiting for event"

        // block on the timer via the AutoResetEvent
        event.WaitOne() |> ignore

        //done
        printfn "Timer ticked at %O" DateTime.Now.TimeOfDay


    open System
    //open Microsoft.FSharp.Control  // Async.* is in this module.

    let userTimerWithAsync =

        // create a timer and associated async event
        let timer = new System.Timers.Timer(2000.0)
        let timerEvent = Async.AwaitEvent (timer.Elapsed) |> Async.Ignore

        // start
        printfn "Waiting for timer at %O" DateTime.Now.TimeOfDay
        timer.Start()

        // keep working
        printfn "Doing something useful while waiting for event"

        // block on the timer event now by waiting for the async to complete
        Async.RunSynchronously timerEvent

        // done
        printfn "Timer ticked at %O" DateTime.Now.TimeOfDay


    let fileWriteWithAsync =

        // create a stream to write to
        use stream = new System.IO.FileStream("test.txt",System.IO.FileMode.Create)

        // start
        printfn "Starting async write"
        let asyncResult = stream.BeginWrite(Array.empty,0,0,null,null)

        // create an async wrapper around an IAsyncResult
        let async = Async.AwaitIAsyncResult(asyncResult) |> Async.Ignore

        // keep working
        printfn "Doing something useful while waiting for write to complete"

        // block on the timer now by waiting for the async to complete
        Async.RunSynchronously async

        // done
        printfn "Async write completed"


    let sleepWorkflow  = async{
        printfn "Starting sleep workflow at %O" DateTime.Now.TimeOfDay
        do! Async.Sleep 2000
        printfn "Finished sleep workflow at %O" DateTime.Now.TimeOfDay
        }

    Async.RunSynchronously sleepWorkflow

    let nestedWorkflow  = async{

        printfn "Starting parent"
        let! childWorkflow = Async.StartChild sleepWorkflow

        // give the child a chance and then keep working
        do! Async.Sleep 100
        printfn "Doing something useful while waiting "

        // block on the child
        let! result = childWorkflow

        // done
        printfn "Finished parent"
        }

    // run the whole workflow
    Async.RunSynchronously nestedWorkflow

    let testLoop = async {
        for i in [1..100] do
            // do something
            printf "%i before.." i

            // sleep a bit
            do! Async.Sleep 10
            printfn "..after"
        }

//    Async.RunSynchronously testLoop

    open System
    open System.Threading

    // create a cancellation source
    let cancellationSource = new CancellationTokenSource()

    // start the task, but this time pass in a cancellation token
    Async.Start (testLoop,cancellationSource.Token)

    // wait a bit
    Thread.Sleep(200)

    // cancel after 200ms
    cancellationSource.Cancel()


    // create a workflow to sleep for a time
    let sleepWorkflowMs ms = async {
        printfn "%i ms workflow started" ms
        do! Async.Sleep ms
        printfn "%i ms workflow finished" ms
        }

    let workflowInSeries = async {
        let! sleep1 = sleepWorkflowMs 1000
        printfn "Finished one"
        let! sleep2 = sleepWorkflowMs 2000
        printfn "Finished two"
        }

    //Synchronous run
    //    #time
    Async.RunSynchronously workflowInSeries
    //    #time

    //Parallel run
    // Create them
    let sleep1 = sleepWorkflowMs 1000
    let sleep2 = sleepWorkflowMs 2000

//    // run them in parallel
//    #time
    [sleep1; sleep2]
        |> Async.Parallel
        |> Async.RunSynchronously
//    #time


    open System.Net
    open System
    open System.IO

    let fetchUrl url =
        let req = WebRequest.Create(Uri(url))
        use resp = req.GetResponse()
        use stream = resp.GetResponseStream()
        use reader = new IO.StreamReader(stream)
        let html = reader.ReadToEnd()
        printfn "finished downloading %s" url

        // a list of sites to fetch
    let sites = ["http://www.bing.com";
                 "http://www.google.com";
                 "http://www.microsoft.com";
                 "http://www.amazon.com";
                 "http://www.yahoo.com"]

//    #time                     // turn interactive timer on
    sites                     // start with the list of sites
    |> List.map fetchUrl      // loop through each site and download
//    #time                     // turn timer off


    open Microsoft.FSharp.Control.CommonExtensions
                                                // adds AsyncGetResponse

    // Fetch the contents of a web page asynchronously
    let fetchUrlAsync url =
        async {
            let req = WebRequest.Create(Uri(url))
            use! resp = req.AsyncGetResponse()  // new keyword "use!"
            use stream = resp.GetResponseStream()
            use reader = new IO.StreamReader(stream)
            let html = reader.ReadToEnd()
            printfn "finished downloading %s" url
            }

//    #time                      // turn interactive timer on
    sites
    |> List.map fetchUrlAsync  // make a list of async tasks
    |> Async.Parallel          // set up the tasks to run in parallel
    |> Async.RunSynchronously  // start them off
//    #time                      // turn timer off


    let childTask() =
        // chew up some CPU.
        for i in [1..1800] do
            for i in [1..1000] do
                do "Hello".Contains("H") |> ignore
                // we don't care about the answer!

    // Test the child task on its own.
    // Adjust the upper bounds as needed
    // to make this run in about 0.2 sec
//    #time
    childTask()
//    #time

    let parentTask =
        childTask
        |> List.replicate 20
        |> List.reduce (>>)

    //test
//    #time
    parentTask()
//    #time

    let asyncChildTask = async { return childTask() }

    let asyncParentTask =
        asyncChildTask
        |> List.replicate 20
        |> Async.Parallel

    //test
//    #time
    asyncParentTask
    |> Async.RunSynchronously
//    #time

//---------------------
// Section 'Why use F#?' Series no. 25
module WhyUseFSharpSeries25 =
//    #nowarn "40"
    let printerAgent = MailboxProcessor.Start(fun inbox->

        // the message processing function
        let rec messageLoop = async{

            // read a message
            let! msg = inbox.Receive()

            // process a message
            printfn "message is: %s" msg

            // loop to top
            return! messageLoop
            }

        // start the loop
        messageLoop
        )

    // test it
    printerAgent.Post "wazuuup"
    printerAgent.Post "wazuuup again"
    printerAgent.Post "wazuuup a third time"


    open System
    open System.Threading
    open System.Diagnostics

    // a utility function
    type Utility() =
        static let rand = new Random()

        static member RandomSleep() =
            let ms = rand.Next(1,10)
            Thread.Sleep ms

    // an implementation of a shared counter using locks
    type LockedCounter () =

        static let _lock = new Object()

        static let mutable count = 0
        static let mutable sum = 0

        static let updateState i =
            // increment the counters and...
            sum <- sum + i
            count <- count + 1
            printfn "Count is: %i. Sum is: %i" count sum

            // ...emulate a short delay
            Utility.RandomSleep()


        // public interface to hide the state
        static member Add i =
            // see how long a client has to wait
            let stopwatch = new Stopwatch()
            stopwatch.Start()

            // start lock. Same as C# lock{...}
            lock _lock (fun () ->

                // see how long the wait was
                stopwatch.Stop()
                printfn "Client waited %i" stopwatch.ElapsedMilliseconds

                // do the core logic
                updateState i
                )
            // release lock

    // test in isolation
    LockedCounter.Add 4
    LockedCounter.Add 5

    let makeCountingTask addFunction taskId  = async {
        let name = sprintf "Task%i" taskId
        for i in [1..3] do
            addFunction i
        }

    // test in isolation
    let task = makeCountingTask LockedCounter.Add 1
    Async.RunSynchronously task
//    #time
    let lockedExample5 =
        [1..100]
            |> List.map (fun i -> makeCountingTask LockedCounter.Add i)
            |> Async.Parallel
            |> Async.RunSynchronously
            |> ignore
//    #time

    type MessageBasedCounter () =

        static let updateState (count,sum) msg =

            // increment the counters and...
            let newSum = sum + msg
            let newCount = count + 1
            printfn "Count is: %i. Sum is: %i" newCount newSum

            // ...emulate a short delay
            Utility.RandomSleep()

            // return the new state
            (newCount,newSum)

        // create the agent
        static let agent = MailboxProcessor.Start(fun inbox ->

            // the message processing function
            let rec messageLoop oldState = async{

                // read a message
                let! msg = inbox.Receive()

                // do the core logic
                let newState = updateState oldState msg

                // loop to top
                return! messageLoop newState
                }

            // start the loop
            messageLoop (0,0)
            )

        // public interface to hide the implementation
        static member Add i = agent.Post i

    // test in isolation
    MessageBasedCounter.Add 4
    MessageBasedCounter.Add 5

    let task2 = makeCountingTask MessageBasedCounter.Add 1
    Async.RunSynchronously task2
    let messageExample5 =
        [1..100]
            |> List.map (fun i -> makeCountingTask MessageBasedCounter.Add i)
            |> Async.Parallel
            |> Async.RunSynchronously
            |> ignore

    let slowConsoleWrite msg =
        msg |> String.iter (fun ch->
            System.Threading.Thread.Sleep(1)
            System.Console.Write ch
            )

    // test in isolation
    slowConsoleWrite "abc"

    let makeTask logger taskId = async {
        let name = sprintf "Task%i" taskId
        for i in [1..3] do
            let msg = sprintf "-%s:Loop%i-" name i
            logger msg
        }

    // test in isolation
    let task3 = makeTask slowConsoleWrite 1
    Async.RunSynchronously task3

    type UnserializedLogger() =
        // interface
        member this.Log msg = slowConsoleWrite msg

    // test in isolation
    let unserializedLogger = UnserializedLogger()
    unserializedLogger.Log "hello"

    let unserializedExample =
        let logger = new UnserializedLogger()
        [1..5]
            |> List.map (fun i -> makeTask logger.Log i)
            |> Async.Parallel
            |> Async.RunSynchronously
            |> ignore

    type SerializedLogger() =

        // create the mailbox processor
        let agent = MailboxProcessor.Start(fun inbox ->

            // the message processing function
            let rec messageLoop () = async{

                // read a message
                let! msg = inbox.Receive()

                // write it to the log
                slowConsoleWrite msg

                // loop to top
                return! messageLoop ()
                }

            // start the loop
            messageLoop ()
            )

        // public interface
        member this.Log msg = agent.Post msg

    // test in isolation
    let serializedLogger = SerializedLogger()
    serializedLogger.Log "hello"

    let serializedExample =
        let logger = new SerializedLogger()
        [1..5]
            |> List.map (fun i -> makeTask logger.Log i)
            |> Async.Parallel
            |> Async.RunSynchronously
            |> ignore

//---------------------
// Section 'Why use F#?' Series no. 26
// http://fsharpforfunandprofit.com/posts/concurrency-reactive/

module WhyUseFSharpSeries26 =
    open System
    open System.Threading

    /// create a timer and register an event handler,
    /// then run the timer for five seconds
    let createTimer timerInterval eventHandler =
        // setup a timer
        let timer = new System.Timers.Timer(float timerInterval)
        timer.AutoReset <- true

        // add an event handler
        timer.Elapsed.Add eventHandler

        // return an async task
        async {
            // start timer...
            timer.Start()
            // ...run for five seconds...
            do! Async.Sleep 5000
            // ... and stop
            timer.Stop()
        }

    // create a handler. The event args are ignored
    let basicHandler _ = printfn "tick %A" DateTime.Now

    // register the handler
    let basicTimer1 = createTimer 1000 basicHandler

    // run the task now
    Async.RunSynchronously basicTimer1

    let createTimerAndObservable timerInterval =
        // setup a timer
        let timer = new System.Timers.Timer(float timerInterval)
        timer.AutoReset <- true

        // events are automatically IObservable
        let observable = timer.Elapsed

        // return an async task
        let task = async {
            timer.Start()
            do! Async.Sleep 5000
            timer.Stop()
            }

        // return a async task and the observable
        (task,observable)

    // create the timer and the corresponding observable
    let basicTimer2 , timerEventStream = createTimerAndObservable 1000

    // register that everytime something happens on the
    // event stream, print the time.
    timerEventStream
    |> Observable.subscribe (fun _ -> printfn "tick %A" DateTime.Now)

    // run the task now
    Async.RunSynchronously basicTimer2

    type ImperativeTimerCount() =

        let mutable count = 0

        // the event handler. The event args are ignored
        member this.handleEvent _ =
          count <- count + 1
          printfn "timer ticked with count %i" count

    // create a handler class
    let handler = new ImperativeTimerCount()

    // register the handler method
    let timerCount1 = createTimer 500 handler.handleEvent

    // run the task now
    Async.RunSynchronously timerCount1

    // create the timer and the corresponding observable
    let timerCount2, timerEventStream2 = createTimerAndObservable 500

    // set up the transformations on the event stream
    timerEventStream2
    |> Observable.scan (fun count _ -> count + 1) 0
    |> Observable.subscribe (fun count -> printfn "timer ticked with count %i" count)

    // run the task now
    Async.RunSynchronously timerCount2


    type FizzBuzzEvent = {label:int; time: DateTime}

    let areSimultaneous (earlierEvent,laterEvent) =
        let {label=_;time=t1} = earlierEvent
        let {label=_;time=t2} = laterEvent
        t2.Subtract(t1).Milliseconds < 50

    type ImperativeFizzBuzzHandler() =

        let mutable previousEvent: FizzBuzzEvent option = None

        let printEvent thisEvent  =
          let {label=id; time=t} = thisEvent
          printf "[%i] %i.%03i " id t.Second t.Millisecond
          let simultaneous = previousEvent.IsSome && areSimultaneous (previousEvent.Value,thisEvent)
          if simultaneous then printfn "FizzBuzz"
          elif id = 3 then printfn "Fizz"
          elif id = 5 then printfn "Buzz"

        member this.handleEvent3 eventArgs =
          let event = {label=3; time=DateTime.Now}
          printEvent event
          previousEvent <- Some event

        member this.handleEvent5 eventArgs =
          let event = {label=5; time=DateTime.Now}
          printEvent event
          previousEvent <- Some event

    // create the class
    let handler2 = new ImperativeFizzBuzzHandler()

    // create the two timers and register the two handlers
    let timer3 = createTimer 300 handler2.handleEvent3
    let timer5 = createTimer 500 handler2.handleEvent5

    // run the two timers at the same time
    [timer3;timer5]
    |> Async.Parallel
    |> Async.RunSynchronously

    let timer33, timerEventStream3 = createTimerAndObservable 300
    let timer55, timerEventStream5 = createTimerAndObservable 500

    // convert the time events into FizzBuzz events with the appropriate id
    let eventStream3  =
       timerEventStream3
       |> Observable.map (fun _ -> {label=3; time=DateTime.Now})

    let eventStream5  =
       timerEventStream5
       |> Observable.map (fun _ -> {label=5; time=DateTime.Now})

    // combine the two streams
    let combinedStream =
        Observable.merge eventStream3 eventStream5

    // make pairs of events
    let pairwiseStream =
       combinedStream |> Observable.pairwise

    // split the stream based on whether the pairs are simultaneous
    let simultaneousStream, nonSimultaneousStream =
        pairwiseStream |> Observable.partition areSimultaneous

    // split the non-simultaneous stream based on the id
    let fizzStream, buzzStream  =
        nonSimultaneousStream
        // convert pair of events to the first event
        |> Observable.map (fun (ev1,_) -> ev1)
        // split on whether the event id is three
        |> Observable.partition (fun {label=id} -> id=3)

    //print events from the combinedStream
    combinedStream
    |> Observable.subscribe (fun {label=id;time=t} ->
                                  printf "[%i] %i.%03i " id t.Second t.Millisecond)

    //print events from the simultaneous stream
    simultaneousStream
    |> Observable.subscribe (fun _ -> printfn "FizzBuzz")

    //print events from the nonSimultaneous streams
    fizzStream
    |> Observable.subscribe (fun _ -> printfn "Fizz")

    buzzStream
    |> Observable.subscribe (fun _ -> printfn "Buzz")

    // debugging code
    simultaneousStream |> Observable.subscribe (fun e -> printfn "sim %A" e)
    nonSimultaneousStream |> Observable.subscribe (fun e -> printfn "non-sim %A" e)

    // run the two timers at the same time
    [timer33;timer55]
    |> Async.Parallel
    |> Async.RunSynchronously
//---------------------
// Section 'Why use F#?' Series no. 27
// http://fsharpforfunandprofit.com/posts/completeness-intro/
// No Code

//---------------------
// Section 'Why use F#?' Series no. 28
// http://fsharpforfunandprofit.com/posts/completeness-seamless-dotnet-interop/
module WhyUseFSharpSeries28 =
    //using an Int32
    let (i1success,i1) = System.Int32.TryParse("123");
    if i1success then printfn "parsed as %i" i1 else printfn "parse failed"

    let (i2success,i2) = System.Int32.TryParse("hello");
    if i2success then printfn "parsed as %i" i2 else printfn "parse failed"

    //using a DateTime
    let (d1success,d1) = System.DateTime.TryParse("1/1/1980");
    let (d2success,d2) = System.DateTime.TryParse("hello");

    //using a dictionary
    let dict = new System.Collections.Generic.Dictionary<string,string>();
    dict.Add("a","hello")
    let (e1success,e1) = dict.TryGetValue("a");
    let (e2success,e2) = dict.TryGetValue("b");

//    let createReader fileName = new System.IO.StreamReader(fileName)
    // error FS0041: A unique overload for method 'StreamReader'
    //               could not be determined
    let createReader2 fileName = new System.IO.StreamReader(path=fileName)

    let (|Digit|Letter|Whitespace|Other|) ch =
       if System.Char.IsDigit(ch) then Digit
       else if System.Char.IsLetter(ch) then Letter
       else if System.Char.IsWhiteSpace(ch) then Whitespace
       else Other

    let printChar ch =
      match ch with
      | Digit -> printfn "%c is a Digit" ch
      | Letter -> printfn "%c is a Letter" ch
      | Whitespace -> printfn "%c is a Whitespace" ch
      | _ -> printfn "%c is something else" ch

    // print a list
    ['a';'b';'1';' ';'-';'c'] |> List.iter printChar

    open System.Data.SqlClient

    let (|ConstraintException|ForeignKeyException|Other|) (ex:SqlException) =
       if ex.Number = 2601 then ConstraintException
       else if ex.Number = 2627 then ConstraintException
       else if ex.Number = 547 then ForeignKeyException
       else Other

    let executeNonQuery (sqlCommmand:SqlCommand) =
        try
            let result = sqlCommmand.ExecuteNonQuery()
            // handle success
            printfn "%i" result
        with
        | :?SqlException as sqlException -> // if a SqlException
            match sqlException with         // nice pattern matching
            | ConstraintException  -> printfn "ConstraintException" // handle constraint error
            | ForeignKeyException  -> printfn "ForeignKeyException" // handle FK error
            | _ -> reraise()          // don't handle any other cases
        // all non SqlExceptions are thrown normally


    // create a new object that implements IDisposable
    let makeResource name =
       { new System.IDisposable
         with member this.Dispose() = printfn "%s disposed" name }

    let useAndDisposeResources =
        use r1 = makeResource "first resource"
        printfn "using first resource"
        for i in [1..3] do
            let resourceName = sprintf "\tinner resource %d" i
            use temp = makeResource resourceName
            printfn "\tdo something with %s" resourceName
        use r2 = makeResource "second resource"
        printfn "using second resource"
        printfn "done."

//---------------------
// Section 'Why use F#?' Series no. 29
module WhyUseFSharpSeries29 =
    let x = 0


//---------------------
// Section 'Why use F#?' Series no. 30
module WhyUseFSharpSeries30 =
    let x = 0


//---------------------